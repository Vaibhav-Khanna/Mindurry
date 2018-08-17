using FreshMvvm;
using Microsoft.Identity.Client;
using Mindurry.DataModels;
using Mindurry.DataStore.Abstraction;
using Mindurry.DataStore.Implementation;
using Mindurry.Helpers;
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
        public static event EventHandler<string> TabbedPageRequested;

        public static event EventHandler<ResidenceModel> TabbedPageApartmentRequested;

       // public static IAuthenticate AuthenticationProvider { get; private set; }

        public static UIParent UiParent = null;

        public static IStoreManager storeManager { get; set; }

        public App ()
		{
			InitializeComponent();

            BasePageModel.Init();

            Init();

            MainPage = new Pages.ConnexionPage();
        }

        public async void Init()
        {
            try
            {
                storeManager = FreshIOC.Container.Resolve<IStoreManager>() as StoreManager;

                //bool authenticated = await storeManager.LoginAsync(true);
 
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

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}

        public static void RequestTabbedPage(string data)
        {
            TabbedPageRequested?.Invoke(null, data);
        }
        public static void RequestApartmentTabbedPage(ResidenceModel data)
        {
            TabbedPageApartmentRequested?.Invoke(null, data);
        }
    }
}
