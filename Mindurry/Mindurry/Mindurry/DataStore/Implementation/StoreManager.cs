using FreshMvvm;
using Microsoft.Identity.Client;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Mindurry.DataStore.Abstraction;
using Mindurry.DataStore.Abstraction.Stores;
using Mindurry.Helpers;
using Mindurry.Models.DataObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Mindurry.DataStore.Implementation
{
    public class StoreManager : IStoreManager
    {
        public bool IsInitialized { get; private set; }

        public static PublicClientApplication ADB2CClient { get; private set; }

        public static MobileServiceUser User { get; private set; }

        public static MobileServiceClient MobileService { get; set; }

        IApartmentStore apartmentStore;
        public IApartmentStore ApartmentStore => apartmentStore ?? (apartmentStore = FreshIOC.Container.Resolve<IApartmentStore>());

        ICellarStore cellarStore;
        public ICellarStore CellarStore => cellarStore ?? (cellarStore = FreshIOC.Container.Resolve<ICellarStore>());

        ICollectSourceStore collectSourceStore;
        public ICollectSourceStore CollectSourceStore => collectSourceStore ?? (collectSourceStore = FreshIOC.Container.Resolve<ICollectSourceStore>());

        IContactCustomFieldSourceEntryStore contactCustomFieldSourceEntryStore;
        public IContactCustomFieldSourceEntryStore ContactCustomFieldSourceEntryStore => contactCustomFieldSourceEntryStore ?? (contactCustomFieldSourceEntryStore = FreshIOC.Container.Resolve<IContactCustomFieldSourceEntryStore>());

        IContactCustomFieldSourceStore contactCustomFieldSourceStore;
        public IContactCustomFieldSourceStore ContactCustomFieldSourceStore => contactCustomFieldSourceStore ?? (contactCustomFieldSourceStore = FreshIOC.Container.Resolve<IContactCustomFieldSourceStore>());

        IContactCustomFieldStore contactCustomFieldStore;
        public IContactCustomFieldStore ContactCustomFieldStore => contactCustomFieldStore ?? (contactCustomFieldStore = FreshIOC.Container.Resolve<IContactCustomFieldStore>());

        IContactStore contactStore;
        public IContactStore ContactStore => contactStore ?? (contactStore = FreshIOC.Container.Resolve<IContactStore>());

        IDocumentMindurryStore documentMindurryStore;
        public IDocumentMindurryStore DocumentMindurryStore => documentMindurryStore ?? (documentMindurryStore = FreshIOC.Container.Resolve<IDocumentMindurryStore>());

        IGarageStore garageStore;
        public IGarageStore GarageStore => garageStore ?? (garageStore = FreshIOC.Container.Resolve<IGarageStore>());

        IGardenStore gardenStore;
        public IGardenStore GardenStore => gardenStore ?? (gardenStore = FreshIOC.Container.Resolve<IGardenStore>());

        INoteStore noteStore;
        public INoteStore NoteStore => noteStore ?? (noteStore = FreshIOC.Container.Resolve<INoteStore>());


        IResidenceStore residenceStore;
        public IResidenceStore ResidenceStore => residenceStore ?? (residenceStore = FreshIOC.Container.Resolve<IResidenceStore>());

        ITerraceStore terraceStore;
        public ITerraceStore TerraceStore => terraceStore ?? (terraceStore = FreshIOC.Container.Resolve<ITerraceStore>());

        IUserStore userStore;
        public IUserStore UserStore => userStore ?? (userStore = FreshIOC.Container.Resolve<IUserStore>());

        IUserFavoriteStore userFavoriteStore;
        public IUserFavoriteStore UserFavoriteStore => userFavoriteStore ?? (userFavoriteStore = FreshIOC.Container.Resolve<IUserFavoriteStore>());

        public StoreManager()
        {
            ADB2CClient = new PublicClientApplication(Constants.ClientID, Constants.Authority);
            ADB2CClient.RedirectUri = Constants.RedirectUri;

        }

        public async Task<bool> LoginAsync(bool useSilent = false)
        {
            if (!IsInitialized)
            {
                await InitializeAsync();
            }

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

                    if (authenticationResult != null && !string.IsNullOrWhiteSpace(authenticationResult.IdToken))
                    {

                        //Read token

                        var tokenClaims = JwtUtility.GetClaims(authenticationResult.IdToken);

                        var sub = tokenClaims["sub"];

                        MobileServiceUser user = new MobileServiceUser(sub.ToString()) { MobileServiceAuthenticationToken = authenticationResult.IdToken };

                        MobileService.CurrentUser = user;

                        User = user;

                        var profileUser = await UserStore.GetProfileAsync(authenticationResult.IdToken);

                        // CacheToken
                        CacheToken(user, profileUser.Role.ToString());

                        success = true;

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;

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
            if (!IsInitialized)
            {
                await InitializeAsync();
            }

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

        public async Task DropEverythingAsync()
        {
            //Settings.UpdateDatabaseId();
            //TODO Do the update id for settings and add rest of the tables

            await ApartmentStore.DropTable();
            await CellarStore.DropTable();
            await CollectSourceStore.DropTable();
            await ContactCustomFieldSourceEntryStore.DropTable();
            await ContactCustomFieldSourceStore.DropTable();
            await ContactCustomFieldStore.DropTable();
            await ContactStore.DropTable();
            await DocumentMindurryStore.DropTable();
            await GarageStore.DropTable();
            await GardenStore.DropTable();
            await NoteStore.DropTable();
            await ResidenceStore.DropTable();
            await TerraceStore.DropTable();
            await UserStore.DropTable();
            await UserFavoriteStore.DropTable();

            IsInitialized = false;

            Settings.UpdateDatabaseId();
        }


        object locker = new object();

        public async Task InitializeAsync()
        {
            MobileServiceSQLiteStore store;

            lock (locker)
            {

                if (IsInitialized)
                    return;

                IsInitialized = true;


                var dbId = Settings.DatabaseId;

                string path = "";

                if (dbId == 0)
                    path = $"syncstore.db";
                else
                    path = $"syncstore{dbId}.db";

                MobileService = new MobileServiceClient(Constants.ApplicationURL);
                store = new MobileServiceSQLiteStore(path);

                store.DefineTable<Apartment>();
                store.DefineTable<Cellar>();
                store.DefineTable<CollectSource>();
                store.DefineTable<ContactCustomField>();
                store.DefineTable<ContactCustomFieldSource>();
                store.DefineTable<ContactCustomFieldSourceEntry>();
                store.DefineTable<Contact>();
                store.DefineTable<DocumentMindurry>();
                store.DefineTable<Garage>();
                store.DefineTable<Garden>();
                store.DefineTable<Note>();
                store.DefineTable<Residence>();
                store.DefineTable<Terrace>();
                store.DefineTable<UserFavorite>();
                store.DefineTable<User>();

                //TODO Add rest of the tables
            }

            await MobileService.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler()).ConfigureAwait(false);

           // await LoadCachedTokenAsync().ConfigureAwait(false);
        }


        public async Task<bool> SyncAllAsync(bool syncUserSpecific)
        {
            if (!IsInitialized)
                await InitializeAsync();





            var taskList = new List<Task<bool>>();

            taskList.Add(ApartmentStore.SyncAsync());
            taskList.Add(CellarStore.SyncAsync());
            taskList.Add(CollectSourceStore.SyncAsync());
            taskList.Add(ContactCustomFieldSourceEntryStore.SyncAsync());
            taskList.Add(ContactCustomFieldSourceStore.SyncAsync());
            taskList.Add(ContactCustomFieldStore.SyncAsync());
            taskList.Add(ContactStore.SyncAsync());
            taskList.Add(DocumentMindurryStore.SyncAsync());
            taskList.Add(GarageStore.SyncAsync());
            taskList.Add(GardenStore.SyncAsync());
            taskList.Add(NoteStore.SyncAsync());
            taskList.Add(ResidenceStore.SyncAsync());
            taskList.Add(TerraceStore.SyncAsync());
            taskList.Add(UserStore.SyncAsync());
            taskList.Add(UserFavoriteStore.SyncAsync());


            Device.BeginInvokeOnMainThread(async () =>
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast(new Acr.UserDialogs.ToastConfig("The app is currently syncing... This might take a few minutes")
                {
                    BackgroundColor = System.Drawing.Color.Maroon,
                    MessageTextColor = System.Drawing.Color.White,
                    Position = Acr.UserDialogs.ToastPosition.Top,
                    Duration = new TimeSpan(0, 0, 10)
                });               
            });

            var successes = await Task.WhenAll(taskList).ConfigureAwait(false);

            if (syncUserSpecific)
            {
                // add stores that are user specific data                       
            } 

            return successes.Any(x => !x); //if any were a failure.

        }

        public static byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }

    }
}