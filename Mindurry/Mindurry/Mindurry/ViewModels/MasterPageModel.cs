using Mindurry.Models.DataObjects;
using Mindurry.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace Mindurry.ViewModels
{
    public class MasterPageModel : BasePageModel
    {

        public MasterPageModel()
        {

        }

        public delegate void EventHandler(MasterMenuEventArgs args);
 
        public event EventHandler MenuItemSelected;

        private MasterMenuItem lastSelectedItem;
        [PropertyChanged.DoNotNotify]
        public MasterMenuItem SelectedItem
        {
            get => null;
            set
            {
                if (value != null && lastSelectedItem != null)
                    lastSelectedItem.IsSelected = false;

                if (value != null)
                {
                    lastSelectedItem = value;
                    value.IsSelected = true;
                    MenuItemSelected?.Invoke(new MasterMenuEventArgs(value));
                }
                RaisePropertyChanged(nameof(SelectedItem));
            }
        }

        ObservableCollection<MasterMenuItem> items;
        [PropertyChanged.DoNotNotify]
        public ObservableCollection<MasterMenuItem> Items
        {
            get { return items; }

            set
            {
                items = value;
                RaisePropertyChanged();
            }
        }

        public override void Init(object initData)
        {
            base.Init(initData);

            #region Static_data_Init

            Items = new ObservableCollection<MasterMenuItem>();

            Items.Add(new MasterMenuItem() { Title = "Tableau de board", TagetType = typeof(Pages.DashboardPage), Icon = "\uF0E2" });
            Items.Add(new MasterMenuItem() { Title = "Leads", TagetType = typeof(Pages.LeadsPage), Icon = "" });
            Items.Add(new MasterMenuItem() { Title = "Contacts", TagetType = typeof(Pages.ContactsPage), Icon = "" });
            Items.Add(new MasterMenuItem() { Title = "Résidences", TagetType = typeof(Pages.ResidencesPage), Icon = "" });
            Items.Add(new MasterMenuItem() { Title = "Clients", TagetType = typeof(Pages.ClientsPage), Icon = "" });
            Items.Add(new MasterMenuItem() { Title = "Rappels", TagetType = typeof(Pages.RemindersPage), Icon = "\uEA8F" });
            SelectedItem = items[0];
            #endregion
        }
    }
}
