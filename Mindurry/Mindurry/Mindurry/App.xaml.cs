using FreshMvvm;
using Microsoft.Identity.Client;
using Mindurry.DataModels;
using Mindurry.DataStore.Abstraction;
using Mindurry.DataStore.Implementation;
using Mindurry.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Mindurry
{
	public partial class App : Application
	{
        public static event EventHandler<string> TabbedPageRequested;

        public static IAuthenticate AuthenticationProvider { get; private set; }

        public static UIParent UiParent = null;


        public App ()
		{
			InitializeComponent();
        
            BasePageModel.Init();
         
            // var page = new Pages.ConnexionPage();
            var page = new Pages.MasterDetailNavigationPage();
            MainPage = page;
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
	}
}
