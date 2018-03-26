﻿using FreshMvvm;
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

            var masterpage = FreshPageModelResolver.ResolvePageModel<MasterPageModel>();

            var context = masterpage.BindingContext as MasterPageModel;

            if (context != null)
            {
                context.MenuItemSelected += Context_MenuItemSelected;
            }

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

        void Context_MenuItemSelected(MasterMenuEventArgs args)
        {
            var currenPage = Detail_navigation.CurrentPage;

            var current_model = currenPage.BindingContext as FreshBasePageModel;

            if (args.item.TagetType == currenPage.GetType())
            {
                IsPresented = false;
                return;
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
