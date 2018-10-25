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
                bool authenticated;
                using (UserDialogs.Instance.Loading("Chargement", null, null, true))
                {
                     authenticated = await storeManager.LoginAsync();
                }
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
            catch (MsalException ex)
            {
                if (ex.ErrorCode == "authentication_canceled")
                {
                    await DisplayAlert("Authentification", "L'authentification a été fermé par l'utilisateur.", "OK");
                }
                else
                {
                    await DisplayAlert("Une erreur est survenur", "Message d'exception: " + ex.Message, "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Authentification", "L'authentification a échoué. Message d'exception: " + ex.Message, "OK");
            }
        }

    }
}
