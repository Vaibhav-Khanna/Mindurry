using Acr.UserDialogs;
using FreshMvvm;
using Microsoft.Identity.Client;
using Mindurry.DataStore.Abstraction;
using Mindurry.DataStore.Implementation;
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


        async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            try
            {
                var storeManager = FreshIOC.Container.Resolve<IStoreManager>() as StoreManager;
                using (UserDialogs.Instance.Loading("Chargement", null, null, true))
                {
                    bool authenticated = await storeManager.LoginAsync();
                    if (authenticated)
                    {
                        var page = new Pages.MasterDetailNavigationPage();
                        Application.Current.MainPage = page;
                        storeManager.SyncAllAsync(true);
                    }
                    else
                    {
                        await DisplayAlert("Erreur", "Erreur d'authentification, veuillez recommencer s'il vous plaît", "OK");
                    }
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
