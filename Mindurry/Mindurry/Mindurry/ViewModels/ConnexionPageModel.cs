using Microsoft.Identity.Client;
using Mindurry.ViewModels.Base;
using System;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;

namespace Mindurry.ViewModels
{
    public class ConnexionPageModel : BasePageModel
    {
        
        public Command LogCommand => new Command( async () =>
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
                    await CoreMethods.DisplayAlert("Authentication", "Authentication", "OK");
                }
            }
            catch (MsalException ex)
            {
                if (ex.ErrorCode == "authentication_canceled")
                {
                    await CoreMethods.DisplayAlert("Authentication", "Authentication was cancelled by the user.", "OK");
                }
                else
                {
                    await CoreMethods.DisplayAlert("An error has occurred", "Exception message: " + ex.Message, "OK");
                }
            }
            catch (Exception ex)
            {
                await CoreMethods.DisplayAlert("Authentication", "Authentication failed. Exception: " + ex.Message, "OK");
            }

        }); 


       /* protected override async void ViewIsAppearing(object sender, EventArgs e)
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
            base.ViewIsAppearing(sender, e);
       
        }  */
    }
}
