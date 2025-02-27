﻿using FreshMvvm;
using Microsoft.Identity.Client;
using Mindurry.DataModels;
using Mindurry.DataStore.Abstraction;
using Mindurry.DataStore.Implementation;
using Mindurry.Helpers;
using Mindurry.Models.DataObjects;
using Mindurry.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Mindurry
{
	public partial class App : Application
	{

       // public static IAuthenticate AuthenticationProvider { get; private set; }

        public static UIParent UiParent = null;

        public static IStoreManager storeManager { get; set; }

        public App ()
		{
			InitializeComponent();

            BasePageModel.Init();
            // Init();

            MainPage = new Pages.ConnexionPage();
        }

        public async void Init()
        {
            try
            {
                

              //  bool authenticated = await storeManager.LoginAsync(true);
 
                if (!string.IsNullOrWhiteSpace(Settings.AuthToken))
                {
                    Device.BeginInvokeOnMainThread(()=>
                    {
                        var page = new Pages.MasterDetailNavigationPage();
                        MainPage = page;
                    });
                    
                    storeManager.SyncAllAsync(true);
                }
            }
            catch
            {
                // Do nothing - the user isn't logged in
            }
        }

        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected async override void OnResume ()
		{
            storeManager = FreshIOC.Container.Resolve<IStoreManager>() as StoreManager;
            // Handle when your app resumes
            if (StoreManager.MobileService.CurrentUser != null)
            {
                await storeManager.SyncAllAsync(false);
               
            }
        }
    }
}
