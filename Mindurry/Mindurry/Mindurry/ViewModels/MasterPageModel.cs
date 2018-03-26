using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace Mindurry.ViewModels
{
    public class MasterPageModel : FreshMvvm.FreshBasePageModel
    {

        public MasterPageModel()
        {

        }

        public delegate void EventHandler(MasterMenuEventArgs args);
 
        public event EventHandler MenuItemSelected;

        private MasterMenuItem selectedItem;
        public MasterMenuItem SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                if (value != null)
                    MenuItemSelected?.Invoke(new MasterMenuEventArgs(selectedItem));
                RaisePropertyChanged(nameof(SelectedItem));
            }
        }

        ObservableCollection<MasterMenuItem> items;
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

            Items.Add(new MasterMenuItem() { Title = "Tableau de board", TagetType = typeof(Pages.DashboardPage), Icon="" });
            Items.Add(new MasterMenuItem() { Title = "Leads", TagetType = typeof(Pages.LeadsPage), Icon = ""});
            Items.Add(new MasterMenuItem() { Title = "Contacts", TagetType = typeof(Pages.ContactsPage), Icon= "" });
            Items.Add(new MasterMenuItem() { Title = "Clients", TagetType = typeof(Pages.ClientsPage), Icon= "" });
            Items.Add(new MasterMenuItem() { Title = "Résidences", TagetType = typeof(Pages.ResidencesPage), Icon = "" });
            SelectedItem = items[0];
            #endregion

            App.TabbedPageRequested += App_TabbedPageRequested;
        }

        private void App_TabbedPageRequested(object sender, DataModels.Residence e)
        {
            SelectedItem = null;
        }
    }
}
