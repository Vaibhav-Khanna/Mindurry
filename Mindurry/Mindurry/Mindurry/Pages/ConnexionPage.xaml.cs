﻿using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mindurry.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ConnexionPage : ContentPage
	{
		public ConnexionPage ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
		}

        protected override async void OnAppearing()
        {
            try
            {
                bool authenticated = await App.AuthenticationProvider.LoginAsync(true);
                if (authenticated)
                {
                    var page = new Pages.MasterDetailNavigationPage();
                    Application.Current.MainPage = page;
                }
            }
            catch
            {
                // Do nothing - the user isn't logged in
            }

            base.OnAppearing();
        }

        async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            try
            {
                bool authenticated = await App.AuthenticationProvider.LoginAsync();
                if (authenticated)
                {
                    var page = new Pages.MasterDetailNavigationPage();
                    Application.Current.MainPage = page;
                }
                else
                {
                    await DisplayAlert("Erreur", "Erreur D'authentification, veuillez recommencer", "OK");
                }
            }
            catch (MsalException ex)
            {
                if (ex.ErrorCode == "authentication_canceled")
                {
                    await DisplayAlert("Authentication", "Authentication was cancelled by the user.", "OK");
                }
                else
                {
                    await DisplayAlert("An error has occurred", "Exception message: " + ex.Message, "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Authentication", "Authentication failed. Exception: " + ex.Message, "OK");
            }
        }

    }
}
