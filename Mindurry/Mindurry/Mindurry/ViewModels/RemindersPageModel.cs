using Mindurry.DataModels;
using Mindurry.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Mindurry.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class RemindersPageModel : BasePageModel
    {

        public ObservableCollection<Reminder> Items { get; set; }

        private Reminder selectedItem;
        public Reminder SelectedItem
        {
            get => selectedItem;
            set
            {
                if (value != null)
                {
                    // CoreMethods.PushPageModel<ClientsDetailsInfoPageModel>(value);
                    selectedItem = null;
                }

            }
        }


        public override void Init(object initData)
        {
            base.Init(initData);

            var item1 = new Reminder
            {              
                ReminderDate = new DateTime(2018, 06, 10, 9, 34, 0),
                ContactName = "Edith Herain",
                Title = "A checkIn",
            };
            var item2 = new Reminder
            {
                ReminderDate = new DateTime(2018, 06, 12, 9, 34, 0),
                ContactName = "Frank Marquez",
                Title = "Envoyer SEPA par DocuSign",
            };
            var item3 = new Reminder
            {
                ReminderDate = new DateTime(2018, 07, 11, 9, 34, 0),
                ContactName = "Marc Franol",
                Title = "Envoyer Contrat",
            };

            Items = new ObservableCollection<Reminder> { item1, item2, item3};

            ViewModels.StaticViewModel.SelectionChanged += StaticViewModel_SelectionChanged;
        }

        private void StaticViewModel_SelectionChanged(object sender, EventArgs e)
        {
            
                foreach (Reminder r in Items)
                {
                    if (r.IsChecked)
                    {
                       
                        return;
                    }
                }
            
          
        }

    }
}