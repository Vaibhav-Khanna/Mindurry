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
        public ObservableCollection<CheckBoxItem> ResidencesChecks { get; set; }
        public ObservableCollection<CheckBoxItem> CommercialChecks { get; set; }

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
        public ICommand CloseFilterCommand { get; set; }
        public ICommand ArrowOneCommand { get; set; }
        public ICommand ArrowTwoCommand { get; set; }
        public ICommand AddCommand { get; set; }



        public async override void Init(object initData)
        {
            base.Init(initData);
            
            await LoadData();

            var check1 = new CheckBoxItem { Content = "Herrian" };
            var check2 = new CheckBoxItem { Content = "Herri Ondo", IsChecked = true };
            var check3 = new CheckBoxItem { Content = "Villa Aguiléra" };

            ResidencesChecks = new ObservableCollection<CheckBoxItem> { check1, check2, check3 };

            var check4 = new CheckBoxItem { Content = "Jean Noosa", IsChecked = true };
            var check5 = new CheckBoxItem { Content = "Marie Shine" };
            var check6 = new CheckBoxItem { Content = "Arold Martino" };

            CommercialChecks = new ObservableCollection<CheckBoxItem> { check4, check5, check6 };

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

            }
            else
            {
                await CoreMethods.DisplayAlert("Erreur", "Impossibilité de charger les données", "OK");
            }
        }
        
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
        public void Dispose()
        {
            MessagingCenter.Unsubscribe<NewClientPageModel>(this, "ReloadCollection");
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
