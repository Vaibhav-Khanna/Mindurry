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
    public class LeadsPageModel : BasePageModel
    {
        private IEnumerable<Contact> _contacts;
        public ObservableCollection<ContactsListModel> Contacts { get; set; }
        private string Filter = null;
        private string SortName = null;
        private bool SortBy = false;

        public string SearchText { get; set; }

        public ObservableCollection<CheckBoxItem> ResidencesChecks { get; set; }
        public ObservableCollection<CheckBoxItem> TypeChecks { get; set; }

        private List<CheckBoxItem> filterRes = new List<CheckBoxItem>();
        private List<CheckBoxItem> filterType = new List<CheckBoxItem>();

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
        public ICommand ArrowOneCommand { get; set; }
        public ICommand ArrowTwoCommand { get; set; }
        public ICommand AddCommand { get; set; }


        public async override void Init(object initData)
        {
            base.Init(initData);
            
            await LoadData();

            await LoadFilter();


            ShowFilterCommand = new Command(ShowFilter);
            ArrowOneCommand = new Command(ChangeArrowOne);
            ArrowTwoCommand = new Command(ChangeArrowTwo);
           
        }
        public async Task LoadFilter()
        {
            // Chargement filtre 
            var residences = await StoreManager.ContactCustomFieldSourceEntryStore.GetItemsByContactCustomFieldSourceName("Résidences");
            residences.OrderBy(x => x.ContactCustomFieldSourceInternalName);

            ResidencesChecks = new ObservableCollection<CheckBoxItem>();
            foreach (var item in residences)
            {
                var resCheck = new CheckBoxItem
                {
                    Content = item.Value,
                    IsChecked = false,
                    Id = item.Id
                };
                ResidencesChecks.Add(resCheck);
            }

            // Chargement type
            var types = await StoreManager.ContactCustomFieldSourceEntryStore.GetItemsByContactCustomFieldSourceName("Type d\'appartement");
            types.OrderBy(x => x.ContactCustomFieldSourceInternalName);

            TypeChecks = new ObservableCollection<CheckBoxItem>();
            foreach (var item in types)
            {
                var typeCheck = new CheckBoxItem
                {
                    Content = item.Value,
                    IsChecked = false,
                    Id = item.Id
                };
                TypeChecks.Add(typeCheck);
            }

        }

        public async Task LoadData()
        {
            Contacts = new ObservableCollection<ContactsListModel>();
            if (!filterType.Any() && !filterRes.Any())
            {
                _contacts = await StoreManager.ContactStore.GetItemsByTypeAsync("Lead", Filter);
            }
            else
            {
                _contacts = await StoreManager.ContactStore.GetItemsByTypeFilterAsync("Lead", filterType, filterRes);
            }

            if ((_contacts != null) || (!_contacts.Any()))
            {

                
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

                    string customFields = item.CustomFields;

                    if (!String.IsNullOrEmpty(customFields))
                    {
                        string residenceFormat = "";
                        string typeFormat = "";

                        if (customFields.Contains("residence="))
                        {

                            string[] substrings = customFields.Split(',');
                            for (int i = 0; i < substrings.Length; i++)
                            {
                                if (substrings[i].Contains("residence="))
                                {
                                    // string[]  = substring.Split('residence=');
                                    string[] stringSeparators = new string[] { "residence=" };
                                    string[] result;
                                    result = substrings[i].Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                                    ContactCustomFieldSourceEntry residence = await StoreManager.ContactCustomFieldSourceEntryStore.GetItemAsync(result[0]);
                                    residenceFormat += residence.Value + "-";

                                }
                            }

                        }
                        contactListItem.Residence = residenceFormat;
                        //Type d'appartement
                        if (customFields.Contains("type="))
                        {

                            string[] substrings = customFields.Split(',');
                            for (int i = 0; i < substrings.Length; i++)
                            {
                                if (substrings[i].Contains("type="))
                                {
                                   
                                    string[] stringSeparators = new string[] { "type=" };
                                    string[] result;
                                    result = substrings[i].Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                                    ContactCustomFieldSourceEntry residence = await StoreManager.ContactCustomFieldSourceEntryStore.GetItemAsync(result[0]);
                                    typeFormat += residence.Value + "-";

                                }
                            }

                        }
                        contactListItem.Type = typeFormat;
                    }

                    /*
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
                    } */
                    indexValue++; //increment to change Backgroung Color
                    Contacts.Add(contactListItem);
                }
            }
            else
            {
                await CoreMethods.DisplayAlert("Erreur", "Impossibilité de charger les données", "OK");
            }
        }

        public Command SelectResidenceCommand => new Command<CheckBoxItem>(async (obj) =>
        {
            if (obj.IsChecked)
            {
                filterRes.Add(obj);
            }
            else
            {
                filterRes.Remove(obj);
            }

            await LoadData();

        });
        public Command SelectTypeCommand => new Command<CheckBoxItem>(async (obj) =>
        {
            if (obj.IsChecked)
            {
                filterType.Add(obj);
            }
            else
            {
                filterType.Remove(obj);
            }

            await LoadData();
        });

        public Command SearchCommand => new Command<string>(async (searchString) =>
        {

            if (searchString?.Length > 0)
            {
                searchString = searchString.ToLower();
                Filter = searchString;

            }
            else
            {
                Filter = null;
            }
            await LoadData();
        });

        public Command SortByCreationDateCommand => new Command(async () =>
        {
            /* SortBy = !SortBy;
             SortName = "CreatedDate";
             await LoadData(); */
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

        public Command SortByLastRelaunchCommand => new Command(() =>
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

        public Command SortByNextRelaunchCommand => new Command(() =>
        {
            SortBy = !SortBy;
            if (!SortBy)
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
        public Command CloseFilterCommand => new Command(async () =>
        {
            await LoadFilter();
            filterRes = new List<CheckBoxItem>();
            filterType = new List<CheckBoxItem>();
            await LoadData();
        });

        void ChangeArrowOne()
        {
            IsFirstListVisible = !IsFirstListVisible;
        }

        void ChangeArrowTwo()
        {
            IsSecondListVisible = !IsSecondListVisible;
        }

       

    }
}
