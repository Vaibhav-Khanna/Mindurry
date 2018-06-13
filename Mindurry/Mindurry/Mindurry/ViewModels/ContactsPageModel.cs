using FreshMvvm;
using Mindurry.DataModels;
using Mindurry.Models.DataObjects;
using Mindurry.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Mindurry.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class ContactsPageModel : BasePageModel
    {
        //public ObservableCollection<Contact> Contacts { get; set; }
        public ObservableCollection<Person> Items { get; set; }
        public ObservableCollection<CheckBoxItem> ResidencesChecks { get; set; }
        public ObservableCollection<CheckBoxItem> CommercialChecks { get; set; }

        private Person selectedItem;
        public Person SelectedItem
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

        public override void Init(object initData)
        {
            base.Init(initData);
            
          //  await LoadData();
            
            var item1 = new Person
            {
                Type = PersonType.Contact,
                Date = new DateTime(2017, 12, 11, 9, 34, 0),
                Name = "Jean Michel Marc",
                Email = "j.doe@gmail.com",
                Telephone = "09 36 73 83 83",
                Commercial = "Arold Martino",
                LastRelaunch = new DateTime(2017, 12, 11, 9, 34, 0),
                NextRelaunch = new DateTime(2017, 12, 11, 9, 34, 0),
                Residence = "Villa Aguilera",
                Index=0
            };

            var item2 = new Person
            {
                Type = PersonType.Contact,
                Date = new DateTime(2017, 12, 11, 9, 34, 0),
                Name = "Sullivan Marc",
                Email = "m.sullivan@immo.com",
                Telephone = "06 87 76 44 56",
                Commercial = "Jean Noosa",
                LastRelaunch = new DateTime(2017, 12, 11, 9, 34, 0),
                NextRelaunch = new DateTime(2017, 12, 11, 9, 34, 0),
                Residence = "Villa Aguilera",
                Index = 1
            };

            var item3 = new Person
            {
                Type = PersonType.Contact,
                Date = new DateTime(2017, 12, 11, 9, 34, 0),
                Name = "Marie Yuji",
                Email = "sean.yuji@yuji.com",
                Telephone = "07 56 65 63 00",
                Commercial = "Jean Noosa",
                LastRelaunch = new DateTime(2017, 12, 11, 9, 34, 0),
                NextRelaunch = new DateTime(201, 12, 11, 9, 34, 0),
                Residence = "Herrian",
                Index = 2
            };

            var item4 = new Person
            {
                Type = PersonType.Contact,
                Date = new DateTime(2017, 12, 11, 9, 34, 0),
                Name = "Albert Louak",
                Email = "m.louak@tera.net",
                Telephone = "06 67 55 87 99",
                Commercial = "Jean Noosa",
                LastRelaunch = new DateTime(2017, 12, 11, 9, 34, 0),
                NextRelaunch = new DateTime(2017, 12, 11, 9, 34, 0),
                Residence = "Villa Aguilera",
                Index = 3
            };

            var item5 = new Person
            {
                Type = PersonType.Contact,
                Date = new DateTime(2017, 9, 12, 11, 59, 0),
                Name = "Louis Aroati",
                Email = "franck.aroati@immo.com",
                Telephone = "07 67 55 22 78",
                Commercial = "Jean Noosa",
                LastRelaunch = new DateTime(2017, 12, 11, 9, 34, 0),
                NextRelaunch = new DateTime(2017, 12, 11, 9, 34, 0),
                Residence = "Villa Aguilera",
                Index = 4
            }; 

             Items = new ObservableCollection<Person> { item1, item2, item3, item4, item5 };

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
        /*
        public async Task LoadData()
        {

            Contacts = new ObservableCollection<Contact>();
            IEnumerable <Contact> contactsList = await StoreManager.ContactStore.GetItemsAsync();
            if ((contactsList != null) || (!contactsList.Any()))
            {
                foreach (var item in contactsList)
                {
                 //   Contacts.Add(item);                
                }

            }
            else
            {
                await CoreMethods.DisplayAlert("Erreur", "Impossibilité de charger les données", "OK");
            }
        }
        */
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
        }
    }
}
