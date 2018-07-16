using FreshMvvm;
using Microsoft.Identity.Client;
using Microsoft.WindowsAzure.MobileServices;
using Mindurry.DataStore.Abstraction;
using Mindurry.Helpers;
using Mindurry.Models.DataObjects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mindurry.DataStore.Implementation
{
    class AuthenticationProvider : IAuthenticate
    {
        public static PublicClientApplication ADB2CClient { get; private set; }

        public static MobileServiceUser User { get; private set; }

        public static MobileServiceClient MobileService { get; set; }

        public static StoreManager StoreManager { get; set; }

        public AuthenticationProvider()
        {
            ADB2CClient = new PublicClientApplication(Constants.ClientID, Constants.Authority);
            ADB2CClient.RedirectUri = Constants.RedirectUri;

            MobileService = new MobileServiceClient(Constants.ApplicationURL);
        }


        public async Task<bool> LoginAsync(bool useSilent = false)
        {
            bool success = false;
            try
            {
                StoreManager = FreshIOC.Container.Resolve<IStoreManager>() as StoreManager;

                AuthenticationResult authenticationResult;

                if (useSilent)
                {
                    authenticationResult = await ADB2CClient.AcquireTokenSilentAsync(
                        Constants.Scopes,
                        GetUserByPolicy(ADB2CClient.Users, Constants.PolicySignUpSignIn),
                        Constants.Authority,
                        false);
                }
                else
                {
                    authenticationResult = await ADB2CClient.AcquireTokenAsync(
                        Constants.Scopes,
                        GetUserByPolicy(ADB2CClient.Users, Constants.PolicySignUpSignIn),
                        App.UiParent);
                }

                if (User == null)
                {
                   
                    if (authenticationResult != null && !string.IsNullOrWhiteSpace(authenticationResult.IdToken))
                    {

                        //Read token

                        var tokenClaims = JwtUtility.GetClaims(authenticationResult.IdToken);

                        var sub = tokenClaims["sub"];

                        MobileServiceUser user = new MobileServiceUser(sub.ToString()) { MobileServiceAuthenticationToken = authenticationResult.IdToken };

                        MobileService.CurrentUser = user;

                        User = user;

                       var profileUser = await StoreManager.UserStore.GetProfileAsync(authenticationResult.IdToken);

                        // CacheToken
                         CacheToken(user, profileUser.Role.ToString());

                        success = true;

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
                Debug.WriteLine(ex);

            }
            return success;
        }

        void CacheToken(MobileServiceUser user, string Role)
        {

            Settings.AuthToken = user.MobileServiceAuthenticationToken;
            Settings.UserId = user.UserId;
            Settings.Role = Role;
        }

        public async Task<bool> LogoutAsync()
        {
            bool success = false;
            try
            {
                if (User != null)
                {
                    await MobileService.LogoutAsync();

                    foreach (var user in ADB2CClient.Users)
                    {
                        ADB2CClient.Remove(user);
                    }
                    User = null;

                    Settings.AuthToken = string.Empty;
                    Settings.UserId = string.Empty;
                    Settings.Role = string.Empty;

                    success = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return success;
        }

        IUser GetUserByPolicy(IEnumerable<IUser> users, string policy)
        {
            foreach (var user in users)
            {
                string userId = Base64UrlDecode(user.Identifier.Split('.')[0]);
                if (userId.EndsWith(policy.ToLower(), StringComparison.Ordinal))
                    return user;
            }
            return null;
        }

        string Base64UrlDecode(string str)
        {
            str = str.Replace('-', '+').Replace('_', '/');
            str = str.PadRight(str.Length + (4 - str.Length % 4) % 4, '=');
            var byteArray = Convert.FromBase64String(str);
            var decoded = Encoding.UTF8.GetString(byteArray, 0, byteArray.Count());
            return decoded;
        }
    }
}