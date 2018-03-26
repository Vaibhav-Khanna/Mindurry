using FreshMvvm;
using Mindurry.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Mindurry
{
	public partial class App : Application
	{
        public static event EventHandler<DataModels.Residence> TabbedPageRequested;

		public App ()
		{
			InitializeComponent();

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

        public static void RequestTabbedPage(Residence data)
        {
            TabbedPageRequested?.Invoke(null, data);
        }
	}
}
