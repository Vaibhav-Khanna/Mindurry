using Microsoft.WindowsAzure.MobileServices;
using Mindurry.DataModels;
using Mindurry.Helpers;
using Mindurry.Models;
using Mindurry.Models.DataObjects;
using Mindurry.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;
using Xamarin.Forms;

namespace Mindurry.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class ContactsPageModel : BasePageModel
    {
        private IEnumerable<Contact> _contacts;
        public ObservableCollection<ContactsListModel> Contacts { get; set; }
        private List<ContactsListModel> contactsLoadData;
        private string Filter = null;
        private string SortName = null;
        private bool SortBy = false;

        public string SearchText{ get; set; }

        public ObservableCollection<CheckBoxItem> ResidencesChecks { get; set; }
        public ObservableCollection<CheckBoxItem> CommercialChecks { get; set; }

        private List<CheckBoxItem> filterRes = new List<CheckBoxItem>();
        private List<CheckBoxItem> filterCommercial = new List<CheckBoxItem>();

        private ContactsListModel selectedItem;
        public ContactsListModel SelectedItem 
        {
            get => selectedItem;
            set
            {
                if (value != null)
                    CoreMethods.PushPageModel<LeadDetailPageModel>(value.ContactId);
                    SubUnsubDetail();
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
        //public ICommand CloseFilterCommand { get; set; }
        public ICommand ArrowOneCommand { get; set; }
        public ICommand ArrowTwoCommand { get; set; }
        public ICommand AddCommand { get; set; }

        public async override void Init(object initData)
        {
            base.Init(initData);

            await LoadData();

            await LoadFilter();
           
            ShowFilterCommand = new Command(ShowFilter);
            //CloseFilterCommand = new Command(CloseFilter);
            ArrowOneCommand = new Command(ChangeArrowOne);
            ArrowTwoCommand = new Command(ChangeArrowTwo);
            AddCommand = new Command(AddContact);
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

            // Chargement commercial
            var commercials = (await StoreManager.UserStore.GetItemsAsync()).OrderBy(x => x.Lastname).ToList();
            //  var commercials = Contacts.Select(x => x.Commercial).Distinct().OrderBy(x => x).ToList();

            CommercialChecks = new ObservableCollection<CheckBoxItem>();
            foreach (var item in commercials)
            {
                var commCheck = new CheckBoxItem
                {
                    Content = item.Name,
                    IsChecked = false,
                    Id = item.Id
                };
                CommercialChecks.Add(commCheck);
            }

        }

        public async Task LoadData()
        {
            //if subscription for refreshing list
            MessagingCenter.Unsubscribe<LeadDetailPageModel>(this, "ReloadCollection");

            _contacts = null;
            Contacts = null;
            long totalCount;
            if (!filterCommercial.Any() && !filterRes.Any()) {
                _contacts = await StoreManager.ContactStore.GetItemsByTypeAsync("Contact", Filter);
                 totalCount = (_contacts as IQueryResultEnumerable<Contact>).TotalCount;
            }
            else {
               var  result = await StoreManager.ContactStore.GetItemsByCommercialFilterAsync("Contact",filterCommercial, filterRes);
                _contacts = result.results;
                totalCount = (long)result.count;
            }

                if ((_contacts != null) || (!_contacts.Any()))
            {
                var list = _contacts.ToList();
                list.AddRange(_contacts);

              //  var totalCount = (_contacts as IQueryResultEnumerable<Contact>).TotalCount;
                if (Convert.ToInt32(totalCount) - list.Count() > 0)
                    IsLoadMore = true;
                else
                    IsLoadMore = false;

                Contacts = new ObservableCollection<ContactsListModel>();
                var indexValue = 0; //to calculate Index to the backgroundColor
                // residences Names
                var residences = await StoreManager.ContactCustomFieldSourceEntryStore.GetItemsByContactCustomFieldSourceName("Résidences");

                foreach (var item in _contacts)
                {
                    
                    var contactListItem = new ContactsListModel();
                    contactListItem.Index = indexValue; // to alternante background Color
                    contactListItem.ContactId = item.Id;
                    contactListItem.Date = item.ContactCreatedAt;
                    contactListItem.Name = item.Firstname + " " + item.Lastname;
                    contactListItem.Email = item.Email;
                    contactListItem.Telephone = item.Phone;
                    contactListItem.Address = item.Street1 + " " + item.ZipCode + " " + item.City + " " + item.Country;
                    contactListItem.Commercial = item.UserFirstname + " " + item.UserLastname;

                    // Calcul de dernier relance (derniere Note sur le contact)
                    DateTimeOffset? lastNoteDate = await StoreManager.NoteStore.GetLastNoteDateAsync(item.Id);
                    contactListItem.LastRelaunch = lastNoteDate;
                    // Calcul du prochain Reminder (Note avec ReminderAt de set)
                    DateTimeOffset? nextReminderDate = await StoreManager.NoteStore.GetNextNoteReminderDateAsync(item.Id);
                    contactListItem.NextRelaunch = nextReminderDate;
                    string customFields = item.CustomFields;

                    if (!String.IsNullOrEmpty(customFields))
                    {
                            string residenceFormat = "";
                            if (customFields.Contains("Résidences="))
                            {

                                string[] substrings = customFields.Split(',');
                                for (int i = 0; i < substrings.Length; i++)
                                {
                                    if (substrings[i].Contains("Résidences="))
                                    {
                                        // string[]  = substring.Split('residence=');
                                        string[] stringSeparators = new string[] { "Résidences=" };
                                        string[] result;
                                        result = substrings[i].Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                                       // ContactCustomFieldSourceEntry residence = await StoreManager.ContactCustomFieldSourceEntryStore.GetItemAsync(result[0]);
                                           foreach (var resItem in residences)
                                        {
                                            if (result[0] == resItem.Id)
                                            {
                                                residenceFormat += resItem.Value + ", ";
                                                break;
                                            }
                                        }
                                       
                                    }
                                }

                            }
                        contactListItem.Residence = residenceFormat;
                    } 
                    
                    indexValue++; //increment to change Backgroung Color
                    Contacts.Add(contactListItem);                  
                }
                contactsLoadData = Contacts.ToList();
            }
            else
            {
                await CoreMethods.DisplayAlert("Erreur", "Impossibilité de charger les données", "OK");
            }
        }

        bool Loadingmore = false;
        //Fetch more contacts for infinite scroll
        public Command LoadMore => new Command(async () =>
        {
            if (!IsLoadMore || Loadingmore)
                return;

            Loadingmore = true;
            long totalCount;

            if (!filterCommercial.Any() && !filterRes.Any())
            {
                _contacts = await StoreManager.ContactStore.GetNextItemsByTypeAsync(Contacts.Count,"Contact", Filter);
                totalCount = (_contacts as IQueryResultEnumerable<Contact>).TotalCount;
            }
            else
            {
                var result = await StoreManager.ContactStore.GetNextItemsByCommercialFilterAsync(Contacts.Count,"Contact", filterCommercial, filterRes);
                _contacts = result.results;
                totalCount = (long)result.count;

            }

            if ((_contacts != null) || (!_contacts.Any()))
            {
                

                Contacts = new ObservableCollection<ContactsListModel>();
                var indexValue = 0; //to calculate Index to the backgroundColor

                // residences Names
                var residences = await StoreManager.ContactCustomFieldSourceEntryStore.GetItemsByContactCustomFieldSourceName("Résidences");

                foreach (var item in _contacts)
                {

                    var contactListItem = new ContactsListModel();
                    contactListItem.Index = indexValue; // to alternante background Color
                    contactListItem.ContactId = item.Id;
                    contactListItem.Date = item.ContactCreatedAt;
                    contactListItem.Name = item.Firstname + " " + item.Lastname;
                    contactListItem.Email = item.Email;
                    contactListItem.Telephone = item.Phone;
                    contactListItem.Address = item.Street1 + " " + item.ZipCode + " " + item.City + " " + item.Country;
                    contactListItem.Commercial = item.UserFirstname + " " + item.UserLastname;

                    // Calcul de dernier relance (derniere Note sur le contact)
                    DateTimeOffset? lastNoteDate = await StoreManager.NoteStore.GetLastNoteDateAsync(item.Id);
                    contactListItem.LastRelaunch = lastNoteDate;
                    // Calcul du prochain Reminder (Note avec ReminderAt de set)
                    DateTimeOffset? nextReminderDate = await StoreManager.NoteStore.GetNextNoteReminderDateAsync(item.Id);
                    contactListItem.NextRelaunch = nextReminderDate;
                    string customFields = item.CustomFields;

                    if (!String.IsNullOrEmpty(customFields))
                    {
                        string residenceFormat = "";
                        if (customFields.Contains("Résidences="))
                        {

                            string[] substrings = customFields.Split(',');
                            for (int i = 0; i < substrings.Length; i++)
                            {
                                if (substrings[i].Contains("Résidences="))
                                {
                                    // string[]  = substring.Split('residence=');
                                    string[] stringSeparators = new string[] { "Résidences=" };
                                    string[] result;
                                    result = substrings[i].Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                                    //ContactCustomFieldSourceEntry residence = await StoreManager.ContactCustomFieldSourceEntryStore.GetItemAsync(result[0]);
                                    foreach (var resItem in residences)
                                    {
                                        if (result[0] == resItem.Id)
                                        {
                                            residenceFormat += resItem.Value + ", ";
                                            break;
                                        }
                                    }
                                }
                            }

                        }
                        contactListItem.Residence = residenceFormat;
                    }

                    indexValue++; //increment to change Backgroung Color
                    Contacts.Add(contactListItem);
                }

                var list = contactsLoadData;
                list.AddRange(Contacts);

                 
                if (Convert.ToInt32(totalCount) - list.Count() > 0)
                {
                    IsLoadMore = true;
                }
                else
                {
                    IsLoadMore = false;
                }
            }
            else
            {
                IsLoadMore = false;
                await CoreMethods.DisplayAlert("Erreur", "Impossibilité de charger les données", "OK");
            }
            Loadingmore = false;
        });

        public Command CsvExportCommand => new Command(async () =>
        {

            List<List<string>> dataList = new List<List<string>>();
            // title firstline in csv
            List<string> column = new List<string> { "Date de création", "Nom", "Email", "Téléphone", "Adresse", "Commercial en charge", "Dernière relance", "Prochaine relance", "Intêret résidence" };
            dataList.Add(column);
            //iterate through list items
            foreach (var item in Contacts)
            {
                //get properties and values 
                PropertyInfo[] props = item.GetType().GetProperties();

                List<string> itemValues = new List<string>(); 

                //iterate through properties
                foreach (var prop in props) 
                {
                    if (prop.Name != "ContactId" && prop.Name != "Lastname" && prop.Name != "Index" && prop.Name != "Type")
                    {
                        var valueProp = prop.GetValue(item, null);
                        string val;
                        if (valueProp == null)
                        {
                            val = "";
                        }
                        else
                        {
                            val = valueProp.ToString();
                        }
                        itemValues.Add(val);
                    }
                }
                dataList.Add(itemValues);
            }
            string csv = string.Join(Environment.NewLine, dataList.Select(i => string.Join(",", i)));

            var bytes = System.Text.Encoding.Unicode.GetBytes(csv);
            StorageFolder folder;
            
            var stream = new MemoryStream(bytes);
            var dateNow = DateTime.Now.ToString("ddMMyyyy");
            var name = "export_Contacts_"+ dateNow+".csv";

            folder = await DependencyService.Get<ISave>().Save(stream, name);
            if (folder != null)
            {
                var result = await CoreMethods.DisplayAlert("Téléchargement", "Le téléchargement du document " + name + " est terminé", "Ouvrir le répertoire", "Fermer");
                if (result)
                {
                    await DependencyService.Get<ISave>().LaunchFolder(folder);
                }
            }
        }
);

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
        public Command SelectCommercialCommand => new Command<CheckBoxItem>(async(obj) =>
        {
            if (obj.IsChecked)
            {
                filterCommercial.Add(obj);
            }
            else
            {
                filterCommercial.Remove(obj);
            }

            await LoadData();
        });

        public Command SearchCommand => new Command<string>(async (searchString) =>
        {

            if (searchString?.Length > 0) {
                searchString = searchString.ToLower();
                Filter = searchString;
               
            }
            else
            {
                Filter = null;
            }
            await LoadData();
        });

        public Command SortByCreationDateCommand => new Command( async () =>
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
        public Command CloseFilterCommand => new Command(async() =>
        {
            await LoadFilter();
            filterRes = new List<CheckBoxItem>();
            filterCommercial = new List<CheckBoxItem>();
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

        void AddContact()
        {
            CoreMethods.PushPageModel<NewContactPageModel>(Qualification.Contact);
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
        void SubUnsubDetail()
        {
            MessagingCenter.Subscribe<LeadDetailPageModel>(this, "ReloadCollection", async (obj) =>
            {
                await LoadData();

            //    MessagingCenter.Unsubscribe<LeadDetailPageModel>(this, "ReloadCollection");
            });
        }
    }
}
