using Mindurry.DataModels;
using Mindurry.Models;
using Mindurry.Models.DataObjects;
using Mindurry.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Mindurry.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class ContactsPageModel : BasePageModel
    {
        IEnumerable<Contact> _contacts { get; set; }
        public ObservableCollection<ContactsListModel> Contacts { get; set; }
        private ObservableCollection<ContactsListModel> NonFilteredContacts; 


        public string SearchText{ get; set; }


        public ObservableCollection<CheckBoxItem> ResidencesChecks { get; set; }
        public ObservableCollection<CheckBoxItem> CommercialChecks { get; set; }

        private List<CheckBoxItem> filterRes = new List<CheckBoxItem>();

        private ContactsListModel selectedItem;
        public ContactsListModel SelectedItem 
        {
            get => selectedItem;
            set
            {
                if (value != null)
                    CoreMethods.PushPageModel<LeadDetailPageModel>(value);
                selectedItem = null;
            }
        }

        private bool SortBy;

        public int Index { get; set; }


        public bool IsFirstListVisible { get; set; } = true;
        public bool IsSecondListVisible { get; set; } = true;
        public bool IsFilterOn { get; set; }

        public string ArrowOne
        {
            get => IsFirstListVisible ? "" : "";
        }

        public string ArrowTwo
        {
            get => IsSecondListVisible ? "" : "";
        }

        public ICommand ShowFilterCommand { get; set; }
        public ICommand CloseFilterCommand { get; set; }
        public ICommand ArrowOneCommand { get; set; }
        public ICommand ArrowTwoCommand { get; set; }
        public ICommand AddCommand { get; set; }



        public async override void Init(object initData)
        {
            base.Init(initData);
            
            await LoadData();

           // Chargement filtre 
           var residences = await StoreManager.ContactCustomFieldSourceEntryStore.GetItemsByContactCustomFieldSourceName("Résidences");
            residences.OrderBy(x => x.ContactCustomFieldSourceInternalName);

            ResidencesChecks = new ObservableCollection<CheckBoxItem>();
            foreach (var item in residences)
            {
                var resCheck = new CheckBoxItem
                {
                    Content = item.Value,
                    IsChecked = false
                };
                ResidencesChecks.Add(resCheck);
            }

            // Chargement commercial
            var commercials = Contacts.Select(x => x.Commercial).Distinct().OrderBy(x => x).ToList();

            CommercialChecks = new ObservableCollection<CheckBoxItem>();
            foreach (var item in commercials)
            {
                var commCheck = new CheckBoxItem
                {
                    Content = item,
                    IsChecked = false
                };
                CommercialChecks.Add(commCheck);
            }

            ShowFilterCommand = new Command(ShowFilter);
            CloseFilterCommand = new Command(CloseFilter);
            ArrowOneCommand = new Command(ChangeArrowOne);
            ArrowTwoCommand = new Command(ChangeArrowTwo);
            AddCommand = new Command(AddContact);
        }
        
        public async Task LoadData()
        {

            _contacts = await StoreManager.ContactStore.GetItemsAsync();
            if ((_contacts != null) || (!_contacts.Any()))
            {
                _contacts = _contacts.OrderBy(x => x.Name);
                Contacts = new ObservableCollection<ContactsListModel>();
                var indexValue = 0; //to calculate Index to the backgroundColor
                foreach (var item in _contacts)
                {
                    
                    var contactListItem = new ContactsListModel();
                    contactListItem.Index = indexValue; // to alternante background Color
                    contactListItem.ContactId = item.Id;
                    contactListItem.Date = item.ContactCreatedAt;
                    contactListItem.Name = item.Firstname + " " + item.Lastname;
                    contactListItem.Email = item.Email;
                    contactListItem.Telephone = item.Phone;
                    contactListItem.Commercial = item.UserFirstname + " " + item.UserLastname;

                    // Calcul de dernier relance (derniere Note sur le contact)
                    DateTimeOffset? lastNoteDate = await StoreManager.NoteStore.GetLastNoteDateAsync(item.Id);
                    contactListItem.LastRelaunch = lastNoteDate;
                    // Calcul du prochain Reminder (Note avec ReminderAt de set)
                    DateTimeOffset? nextReminderDate = await StoreManager.NoteStore.GetNextNoteReminderDateAsync(item.Id);
                    contactListItem.NextRelaunch = nextReminderDate;

                    // Custom Field Residence
                    var residences = (await StoreManager.ContactCustomFieldStore.GetItemsByContactCustomFieldSourceName("Résidences", item.Id)).ToList();
                   
                    if ((residences != null) && (residences.Any()))
                    {
                        string residenceFormat="";
                        for (var i = 0; i < residences.Count(); i++)
                        {
                            residenceFormat += residences[i].ContactCustomFieldSourceEntryValue; 
                            if ((residences.Count() > 1) && (i < residences.Count() - 1))
                            {
                                residenceFormat += ", ";
                            }
                        }
                        contactListItem.Residence = residenceFormat;                     
                    }
                    indexValue++; //increment to change Backgroung Color
                    Contacts.Add(contactListItem);                  
                }
                NonFilteredContacts = new ObservableCollection<ContactsListModel>(Contacts);


            }
            else
            {
                await CoreMethods.DisplayAlert("Erreur", "Impossibilité de charger les données", "OK");
            }
        }

        public Command SelectResidenceCommand => new Command<CheckBoxItem>((obj) =>
        {
            if (obj.IsChecked)
            {
                filterRes.Add(obj);
            }
            else
            {
                filterRes.Remove(obj);
            }
            Contacts = new ObservableCollection<ContactsListModel>();

        });

        public Command SearchCommand => new Command<string>((searchString) =>
        {

            if (searchString?.Length > 0) {
                searchString = searchString.ToLower();
                Contacts = new ObservableCollection<ContactsListModel>(NonFilteredContacts.Where(x => ((x.Name.ToLower().Contains(searchString))|| (x.Email.ToLower().Contains(searchString))|| (x.Telephone.ToLower().Contains(searchString)))).ToList());
            }
            else
            {
                Contacts = NonFilteredContacts;
            }
        });

        public Command SortByCreationDateCommand => new Command( () =>
        {
            SortBy = !SortBy;
            if (SortBy)
            {
                Contacts = new ObservableCollection<ContactsListModel>(Contacts.OrderBy(x => x.Date).ToList());
            }
            else
            {
                Contacts = new ObservableCollection<ContactsListModel>(Contacts.OrderByDescending(x => x.Date).ToList());
            }
        });

        public Command SortByLastRelaunchCommand => new Command( () =>
        {
            SortBy = !SortBy;
            if (SortBy)
            {
                Contacts = new ObservableCollection<ContactsListModel>(Contacts.OrderBy(x => x.LastRelaunch).ToList());
            }
            else
            {
                Contacts = new ObservableCollection<ContactsListModel>(Contacts.OrderByDescending(x => x.LastRelaunch).ToList());
            }
        });

        public Command SortByNextRelaunchCommand => new Command( () =>
        {
            SortBy = !SortBy;
            if (SortBy)
            {
                Contacts = new ObservableCollection<ContactsListModel>(Contacts.OrderBy(x => x.NextRelaunch).ToList());
            }
            else
            {
                Contacts = new ObservableCollection<ContactsListModel>(Contacts.OrderByDescending(x => x.NextRelaunch).ToList());
            }
        });

        void ShowFilter()
        {
            IsFilterOn = !IsFilterOn;
        }
        void CloseFilter()
        {
            IsFilterOn = false;
        }

        void ChangeArrowOne()
        {
            IsFirstListVisible = !IsFirstListVisible;
        }

        void ChangeArrowTwo()
        {
            IsSecondListVisible = !IsSecondListVisible;
        }

        void AddContact()
        {
            CoreMethods.PushPageModel<NewContactPageModel>();
            SubUnsub();
        }

        void SubUnsub()
        {
            MessagingCenter.Subscribe<NewContactPageModel>(this, "ReloadCollection", async (obj) =>
            {
                    await LoadData();

                MessagingCenter.Unsubscribe<NewContactPageModel>(this, "ReloadCollection");
            });
        }
    }
}
