using FreshMvvm;
using Mindurry.Models;
using Mindurry.Models.DataObjects;
using Mindurry.Services.Abstraction;
using Mindurry.Services.Implementation;
using Mindurry.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Mindurry.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class NewContactPageModel : BasePageModel
    {

        private double? _latitude;
        private double? _longitude;
        private string _placeId;
        private string _placeLocation;
        private string _streetNumber;
        private string _street;
        private string _locality;
        private string _postalCode;
        private string _country;

        private string ContactId;

        public string Title { get; set; }

        public List<CollectSource> CollectSources { get; set; }

        public CollectSource CollectSourcesSelected { get; set; }

        public List<ContactCustomFieldSourceEntry> CustomFields { get; set; }

        public ContactCustomFieldSourceEntry CustomFieldsSelected { get; set; }

        private ContactCustomField _customF;

        public Contact Contact { get; set; } = new Contact();

        private readonly IPlaceService _placeService;
        private CancellationTokenSource _cancel;

        private ObservableCollection<PlaceLocation> _locations = new ObservableCollection<PlaceLocation>();
        public ObservableCollection<PlaceLocation> Locations
        {
            get { return _locations; }
            set
            {
                _locations = value;
                RaisePropertyChanged();
            }
        }

        private bool _isVisibleListView;
        public bool IsVisibleListView
        {

            get { return _isVisibleListView; }
            set
            {
                _isVisibleListView = value;
                RaisePropertyChanged();
            }
        }
        private string searchText;
        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value;
                RaisePropertyChanged();
            }
        }

        public NewContactPageModel()
        {
            _placeService = new PlaceService();
        }

        public ICommand FindPlaceCommand => new Command<string>(ExecuteFindPlaceCommandAsync);

        private void ExecuteFindPlaceCommandAsync(string searchString)
        {
            if (searchString?.Length >= 3)
            {
                IsVisibleListView = true;
                PerformSearch();
            }
            else
            {
                IsVisibleListView = false;
                Locations.Clear();
            }
        }

        private void PerformSearch()
        {
            try
            {
                //cancel existing task if applicable
                if (_cancel != null && !_cancel.IsCancellationRequested)
                {
                    _cancel.Cancel();
                }

                //create new search task
                _cancel = new CancellationTokenSource(TimeSpan.FromSeconds(30));
                FirePlacesSearch(_placeService.GetResult(SearchText), _cancel);
            }
            catch (TaskCanceledException tce)
            {
            }
        }

        private async void FirePlacesSearch(Task<WebServiceResults.Result> getResult, CancellationTokenSource onCancel)
        {
            try
            {
                WebServiceResults.Result result = await Task.Run(() => getResult, onCancel.Token);
                if (result != null)
                {
                    Locations.Clear();

                    foreach (var prediction in result.predictions.Where(newItem => Locations.All(oldItem => oldItem.Id != newItem.id)))
                    {

                        Locations.Add(new PlaceLocation()
                        {
                            Id = prediction.place_id,
                            Location = prediction.description
                        });

                    }
                }

            }

            catch (TaskCanceledException ex)
            {

            }

        }

        public ICommand PickPlaceCommand => new Command<PlaceLocation>((place) => { var mypage = ExecutePickPlaceCommandAsync(place); });


        private async Task ExecutePickPlaceCommandAsync(PlaceLocation place)
        {
            SearchText = place.Location;
            var position = await _placeService.GetResultDetail(place.Id);
             _latitude = position.Result.Geometry.Location.Lat;
             _longitude = position.Result.Geometry.Location.Lng;
            _placeId = place.Id;
            _placeLocation = place.Location;
            var  _addressComponents = position.Result.AddressComponents;
            IsVisibleListView = false;

            for (var i = 0; i < _addressComponents.Count; i++)
            {

                for (var j = 0; j < _addressComponents[i].Types.Count; j++)
                {
                    if (_addressComponents[i].Types[j] == "street_number") { _streetNumber = _addressComponents[i].LongName; }
                    if (_addressComponents[i].Types[j] == "route") { _street = _addressComponents[i].LongName; }
                    if (_addressComponents[i].Types[j] == "locality") { _locality = _addressComponents[i].LongName; }
                    if (_addressComponents[i].Types[j] == "country") { _country = _addressComponents[i].LongName; }
                    if (_addressComponents[i].Types[j] == "postal_code") { _postalCode = _addressComponents[i].LongName; }

                }

            }
        }


        public async override void Init(object initData)
        {
            base.Init(initData);
            if (initData != null)
            {
                Title = "Modification Contact";
                ContactId = (string)initData;
                Contact = await StoreManager.ContactStore.GetItemAsync(ContactId);

                if (!String.IsNullOrEmpty(Contact.PlaceId) && !String.IsNullOrEmpty(Contact.PlaceLocation)) { 
                    //affichage adresse  (si adresse saisie)
                    var itemPl = new PlaceLocation
                    {
                        Id = Contact.PlaceId,
                        Location = Contact.PlaceLocation
                    };
                    await ExecutePickPlaceCommandAsync(itemPl);
                }

            }

            Title = "Nouveau Contact";

            IsVisibleListView = false;

            await FetchCollectSources();
            await FetchType();
        }

        public Command CloseCommand => new Command(async () =>
        {
            await CoreMethods.PopPageModel(false, false);
        });

        public Command SaveCommand => new Command(async () =>
        {
        if ( !string.IsNullOrWhiteSpace(Contact.Firstname) && !string.IsNullOrWhiteSpace(Contact.Lastname) && !string.IsNullOrWhiteSpace(_street)) //Address != null &&
            {
                Contact ContactToSave;
                if (String.IsNullOrEmpty(ContactId))
                {
                    ContactToSave = new Contact();
                }
                else
                {
                    ContactToSave = Contact;
                }

                ContactToSave.Firstname = Contact.Firstname;
                ContactToSave.Lastname = Contact.Lastname;
                ContactToSave.Street1 = _streetNumber + " " + _street;
                ContactToSave.ZipCode = _postalCode;
                ContactToSave.City = _locality;
                ContactToSave.Country = _country;
                ContactToSave.Latitude = _latitude;
                ContactToSave.Longitude = _longitude;
                ContactToSave.PlaceId = _placeId;
                ContactToSave.PlaceLocation = _placeLocation;
                ContactToSave.JobTitle = Contact.JobTitle;
                ContactToSave.Email = Contact.Email;
                ContactToSave.Phone = Contact.Phone;
                ContactToSave.Qualification = Qualification.Contact.ToString();
                ContactToSave.CollectSourceId = CollectSourcesSelected.Id;
                

                //Save contact 
                //Insert if new Contact
                if (String.IsNullOrEmpty(ContactId)) {

                    //save custom Field
                    ContactCustomField contactCustomFieldToSave = new ContactCustomField();
                    contactCustomFieldToSave.ContactId = ContactToSave.Id;
                    contactCustomFieldToSave.ContactCustomFieldSourceEntryId = CustomFieldsSelected.Id;
                    contactCustomFieldToSave.ContactCustomFieldSourceId = CustomFieldsSelected.ContactCustomFieldSourceId;
                    await StoreManager.ContactCustomFieldStore.InsertAsync(contactCustomFieldToSave);

                    //Calcul contact customField field
                    var customs = await StoreManager.ContactStore.RewriteCustomFields(ContactToSave.Id);
                    ContactToSave.CustomFields = customs;

                    //Update contact
                    bool isInsertedContact =  await StoreManager.ContactStore.InsertAsync(ContactToSave);

                        MessagingCenter.Send(this, "ReloadCollection");
                        await CoreMethods.PopPageModel(false, false);
                    
                }
                else // Update if existed contact
                {
                    //update ContactCustom if necessary
                    if (_customF.ContactCustomFieldSourceEntryId != CustomFieldsSelected.Id)
                    {
                        _customF.ContactCustomFieldSourceEntryId = CustomFieldsSelected.Id;
                        await StoreManager.ContactCustomFieldStore.UpdateAsync(_customF);

                        //Recalculate Contact CustomFields field to update it
                       var customs = await StoreManager.ContactStore.RewriteCustomFields(ContactToSave.Id);
                        ContactToSave.CustomFields = customs;
                    }
                    //  update contact
                    await StoreManager.ContactStore.UpdateAsync(ContactToSave);
                    MessagingCenter.Send(this, "ReloadDetailContact");
                    await CoreMethods.PopPageModel(false, false);
                }
                  
            } else
            {
                await CoreMethods.DisplayAlert("Erreur", "Des champs ne sont pas remplis", "Ok");
            }


        });

        async Task FetchCollectSources()
        {
            var collectSource = await StoreManager.CollectSourceStore.GetItemsAsync();
            CollectSources = new List<CollectSource>();

            if (collectSource.Any())
            {
                CollectSources = collectSource.ToList();
                CollectSourcesSelected = CollectSources[0];
            }

        }

        async Task FetchType()
        {
            var customField = await StoreManager.ContactCustomFieldSourceEntryStore.GetItemsByContactCustomFieldSourceName("Type");
                CustomFields = new List<ContactCustomFieldSourceEntry>();

            if (customField.Any())
            {
                CustomFields = customField.ToList();
                
                //selected if update of contact
                if (!String.IsNullOrEmpty(ContactId))
                {
                    _customF = await StoreManager.ContactCustomFieldStore.GetItemByContactIdAndSourceIdAsync(Contact.Id, CustomFields[0].ContactCustomFieldSourceId);
                    foreach (var item in CustomFields)
                    {
                        if (item.Id == _customF.ContactCustomFieldSourceEntryId)
                        {
                            CustomFieldsSelected = item;
                            break;
                        }
                    }
                }
                else
                {
                    CustomFieldsSelected = CustomFields[0];
                }
                
            }
        }

    }
}
