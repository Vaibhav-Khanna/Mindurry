using FreshMvvm;
using Mindurry.DataModels;
using Mindurry.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Mindurry.Pages
{
    public class MasterDetailNavigationPage : FreshMasterDetailNavigationContainer
    {
        FreshNavigationContainer Detail_navigation;

        public MasterDetailNavigationPage()
        {
            var detailpage = FreshPageModelResolver.ResolvePageModel<ViewModels.DashboardPageModel>();

            var masterpage = FreshPageModelResolver.ResolvePageModel<ViewModels.MasterPageModel>();

            var context = masterpage.BindingContext as MasterPageModel;

            if (context != null)
            {
                context.MenuItemSelected += Context_MenuItemSelected;
            }

            App.TabbedPageRequested += App_TabbedPageRequested;

            App.TabbedPageApartmentRequested += App_TabbedPageApartmentRequested;
            
            #region Setup

            masterpage.Title = " ";
            //masterpage.Icon = "menu.png";

            // stop master from splitscreen view in landscape 
            MasterBehavior = MasterBehavior.Popover;

            // Disable swipe left to right for opening navigation drawer
            IsGestureEnabled = false;

            #endregion

            Detail_navigation = new FreshNavigationContainer(detailpage) { BarTextColor = Color.Black, BarBackgroundColor = Color.White };

            Master = masterpage;

            Detail = Detail_navigation;
        }

        private void App_TabbedPageRequested(object sender, string e)
        {
            var tabbedNavigation = new FreshTabbedFONavigationContainer(e);
            //this causes weird 
            //tabbedNavigation.Title = e;
            tabbedNavigation.AddTab<ViewModels.ResidenceDetailInfoPageModel>("Informations", null);
            tabbedNavigation.AddTab<ViewModels.ResidenceDetailApartmentPageModel>("Appartements", null);
            tabbedNavigation.AddTab<ViewModels.ResidencesDetailsGaragesPageModel>("Garages", null, new Tuple<bool,string> (true, e));
            tabbedNavigation.AddTab<ViewModels.ResidenceDetailsCellarsPageModel>("Caves", null, new Tuple<bool, string>(false, e));
            tabbedNavigation.AddTab<ViewModels.AcquereursPageModel>("Acquéreurs", null);
            Detail_navigation = null;
            Detail = tabbedNavigation;
        }

        private void App_TabbedPageApartmentRequested(object sender, ResidenceModel e)
        {
            var tabbedNavigation = new FreshTabbedFONavigationContainer(e.NoArchi.ToString());
            //this causes weird 
            //tabbedNavigation.Title = e;
            tabbedNavigation.AddTab<ViewModels.ApartmentDetailInfoPageModel>("Informations", null,e);
            tabbedNavigation.AddTab<ViewModels.ApartmentPlansPageModel>("Plans", null,e);
            
            Detail_navigation = null;
            Detail = tabbedNavigation;
        }

        void Context_MenuItemSelected(MasterMenuEventArgs args)
        {
            if (Detail_navigation != null)
            {
                var currenPage = Detail_navigation.CurrentPage;

                if (args.item.TagetType == currenPage.GetType())
                {
                    IsPresented = false;
                    return;
                }
            }

            switch (args.item.TagetType.ToString().Split('.').Last())
            {
                case "DashboardPage":
                    {
                        Detail_navigation = new FreshNavigationContainer(FreshPageModelResolver.ResolvePageModel < ViewModels.DashboardPageModel>()) { BarTextColor = Color.Black, BarBackgroundColor = Color.White };
                        Detail = Detail_navigation;
                        break;
                    }
                case "LeadsPage":
                    {
                        Detail_navigation = new FreshNavigationContainer(FreshPageModelResolver.ResolvePageModel<ViewModels.LeadsPageModel>()) { BarTextColor = Color.Black, BarBackgroundColor = Color.White };
                        Detail = Detail_navigation;
                        break;
                    }
                case "ContactsPage":
                    {
                        Detail_navigation = new FreshNavigationContainer(FreshPageModelResolver.ResolvePageModel<ViewModels.ContactsPageModel>()) { BarTextColor = Color.Black, BarBackgroundColor = Color.White };
                        Detail = Detail_navigation;

                        break;
                    }
                case "ClientsPage":
                    {
                        Detail_navigation = new FreshNavigationContainer(FreshPageModelResolver.ResolvePageModel<ViewModels.ClientsPageModel>()) { BarTextColor = Color.Black, BarBackgroundColor = Color.White };
                        Detail = Detail_navigation;
                        break;
                    }
                case "ResidencesPage":
                    {
                        Detail_navigation = new FreshNavigationContainer(FreshPageModelResolver.ResolvePageModel<ViewModels.ResidencesPageModel>()) { BarTextColor = Color.Black, BarBackgroundColor = Color.White };
                        Detail = Detail_navigation;
                        break;
                    }
                case "RemindersPage":
                    {
                        Detail_navigation = new FreshNavigationContainer(FreshPageModelResolver.ResolvePageModel<ViewModels.RemindersPageModel>()) { BarTextColor = Color.Black, BarBackgroundColor = Color.White };
                        Detail = Detail_navigation;
                        break;
                    }
                default:
                    {
                        IsPresented = false;
                        return;
                    }
            }
            IsPresented = false;
        }
    }
}
