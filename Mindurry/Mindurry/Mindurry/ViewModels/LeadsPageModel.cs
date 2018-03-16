using FreshMvvm;
using Mindurry.DataModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Mindurry.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class LeadsPageModel: FreshBasePageModel
    {
        public ObservableCollection<Lead> Items { get; set; }
        private Lead selectedItem;
        public Lead SelectedItem
        {
            get => selectedItem;
            set
            {
                if (value != null)
                    CoreMethods.PushPageModel<LeadDetailPageModel>(value);
                selectedItem = null;
            }
        }

        public override void Init(object initData)
        {
            base.Init(initData);

            var item1 = new Lead
            {
                Date = new DateTime(2018, 1, 1, 15, 45, 0),
                Name = "Hasley Duran",
                Email = "h.duran@gmail.com",
                Telephone = "09 36 73 83 83"
            };

            var item2 = new Lead
            {
                Date = new DateTime(2017, 12, 24, 13, 44, 0),
                Name = "Jeff Frazier",
                Email = "j.frazier@gmail.com",
                Telephone = "06 87 76 44 56"
            };

            var item3 = new Lead
            {
                Date = new DateTime(2017, 12, 18, 16, 12, 0),
                Name = "Mike Kilopi",
                Email = "mike.kilopi@yuji.com",
                Telephone = "07 56 65 63 00"
            };

            var item4 = new Lead
            {
                Date = new DateTime(2017, 12, 11, 9, 34, 0),
                Name = "Manu Surto",
                Email = "m.surto@tera.net",
                Telephone = "06 67 55 87 99"
            };

            var item5 = new Lead
            {
                Date = new DateTime(2017, 9, 12, 11, 59, 0),
                Name = "Arold Marketi",
                Email = "a.marketi@immo.com",
                Telephone = "07 67 55 22 78"
            };

            Items = new ObservableCollection<Lead> { item1, item2, item3, item4, item5 };
        }
    }
}
