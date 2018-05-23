using FreshMvvm;
using Microsoft.Identity.Client;
using Microsoft.WindowsAzure.MobileServices;
using Mindurry.DataStore.Abstraction;
using Mindurry.DataStore.Abstraction.Stores;
using Mindurry.DataStore.Implementation.Stores;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Mindurry.DataStore.Implementation
{
    public class StoreManager : IStoreManager
    {
        public static PublicClientApplication ADB2CClient { get; private set; }

        public static MobileServiceUser User { get; private set; }

        public static MobileServiceClient MobileService { get; set; }

        public bool IsInitialized { get; private set; }

        IApartmentStore apartmentStore;
        public IApartmentStore ApartmentStore => apartmentStore ?? (apartmentStore = FreshIOC.Container.Resolve<IApartmentStore>());

        ICellarStore cellarStore;
        public ICellarStore CellarStore => cellarStore ?? (cellarStore = FreshIOC.Container.Resolve<ICellarStore>());

        IContactStore contactStore;
        public IContactStore ContactStore => contactStore ?? (contactStore = FreshIOC.Container.Resolve<IContactStore>());

        IDocumentStore documentStore;
        public IDocumentStore DocumentStore => documentStore ?? (documentStore = FreshIOC.Container.Resolve<IDocumentStore>());

        IGarageStore garageStore;
        public IGarageStore GarageStore => garageStore ?? (garageStore = FreshIOC.Container.Resolve<IGarageStore>());

        IGardenStore gardenStore;
        public IGardenStore GardenStore => gardenStore ?? (gardenStore = FreshIOC.Container.Resolve<IGardenStore>());

        IResidenceStore residenceStore;
        public IResidenceStore ResidenceStore => residenceStore ?? (residenceStore = FreshIOC.Container.Resolve<IResidenceStore>());

        ITerraceStore terraceStore;
        public ITerraceStore TerraceStore => terraceStore ?? (terraceStore = FreshIOC.Container.Resolve<ITerraceStore>());

        IUserStore userStore;
        public IUserStore UserStore => userStore ?? (userStore = FreshIOC.Container.Resolve<IUserStore>());

        IUserFavoriteStore userFavoriteStore;
        public IUserFavoriteStore UserFavoriteStore => userFavoriteStore ?? (userFavoriteStore = FreshIOC.Container.Resolve<IUserFavoriteStore>());




        #region iStoreManager Implementation


        public StoreManager()
        {
            ADB2CClient = new PublicClientApplication(Constants.ClientID, Constants.Authority);
            ADB2CClient.RedirectUri = Constants.RedirectUri;
        }

        object locker = new object();
        public void Initialize()
        {

            lock (locker)
            {

                if (IsInitialized)
                    return;

                IsInitialized = true;
                MobileService = new MobileServiceClient(Constants.ApplicationURL);
            }

         //   LoadCachedTokenAsync();
        }

        #endregion

        public async Task<bool> LoginAsync(bool useSilent = false)
        {
            bool success = false;
            try
            {
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
                    var payload = new JObject();
                    if (authenticationResult != null && !string.IsNullOrWhiteSpace(authenticationResult.IdToken))
                    {
                        payload["access_token"] = authenticationResult.IdToken;
                    }

                    User = await TodoItemManager.DefaultManager.CurrentClient.LoginAsync(
                        MobileServiceAuthenticationProvider.WindowsAzureActiveDirectory,
                        payload);
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

        public async Task<bool> LogoutAsync()
        {
            bool success = false;
            try
            {
                if (User != null)
                {
                    await TodoItemManager.DefaultManager.CurrentClient.LogoutAsync();

                    foreach (var user in ADB2CClient.Users)
                    {
                        ADB2CClient.Remove(user);
                    }
                    User = null;
                    success = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return success;
        }


    }
}
