using FreshMvvm;
using Mindurry.DataModels;
using Mindurry.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Mindurry.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class LeadsPageModel : BasePageModel
    {
        public ObservableCollection<Person> Items { get; set; }
        private Person selectedItem;
        public Person SelectedItem
        {
            get => selectedItem;
            set
            {
                if (value != null)
                {
                    NavigationToDetail(value);
                    selectedItem = null;
                }
                
            }
        }
            private async void NavigationToDetail(Person value)
        {
            await CoreMethods.PushPageModel<LeadDetailPageModel>(value);

        }

        public override void Init(object initData)
        {
            base.Init(initData);

            var item1 = new Person
            {
                Type = PersonType.Lead,
                Date = new DateTime(2018, 1, 1, 15, 45, 0),
                Name = "Hasley Duran",
                Email = "h.duran@gmail.com",
                Telephone = "09 36 73 83 83",
                ApartmentType= "T2",
                Residence = "Villa Aguilera"
            };

            var item2 = new Person
            {
                Type = PersonType.Lead,
                Date = new DateTime(2017, 12, 24, 13, 44, 0),
                Name = "Jeff Frazier",
                Email = "j.frazier@gmail.com",
                Telephone = "06 87 76 44 56",
                ApartmentType = "T2",
                Residence = "Villa Aguilera"
            };

            var item3 = new Person
            {
                Type = PersonType.Lead,
                Date = new DateTime(2017, 12, 18, 16, 12, 0),
                Name = "Mike Kilopi",
                Email = "mike.kilopi@yuji.com",
                Telephone = "07 56 65 63 00",
                ApartmentType = "T2",
                Residence = "Villa Aguilera"
            };

            var item4 = new Person
            {
                Type = PersonType.Lead,
                Date = new DateTime(2017, 12, 11, 9, 34, 0),
                Name = "Manu Surto",
                Email = "m.surto@tera.net",
                Telephone = "06 67 55 87 99",
                ApartmentType = "T2",
                Residence = "Villa Aguilera"
            };

            var item5 = new Person
            {
                Type = PersonType.Lead,
                Date = new DateTime(2017, 9, 12, 11, 59, 0),
                Name = "Arold Marketi",
                Email = "a.marketi@immo.com",
                Telephone = "07 67 55 22 78",
                ApartmentType = "T4",
                Residence = "Villa Aguilera"
            };

            Items = new ObservableCollection<Person> { item1, item2, item3, item4, item5 };
        }
    }
}
