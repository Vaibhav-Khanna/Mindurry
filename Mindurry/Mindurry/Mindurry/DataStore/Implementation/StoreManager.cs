using Microsoft.WindowsAzure.MobileServices;
using Mindurry.DataStore.Abstraction;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Mindurry.DataStore.Implementation
{
    public class StoreManager : IStoreManager
    {
        public static MobileServiceClient MobileService { get; set; }

        public bool IsInitialized { get; private set; }

        ISiteStore siteStore;
        public ISiteStore SiteStore => siteStore ?? (siteStore = FreshIOC.Container.Resolve<ISiteStore>());

        IDocumentStore documentStore;
        public IDocumentStore DocumentStore => documentStore ?? (documentStore = FreshIOC.Container.Resolve<IDocumentStore>());

        IJobStore jobStore;
        public IJobStore JobStore => jobStore ?? (jobStore = FreshIOC.Container.Resolve<IJobStore>());

        ILexiconStore lexiconStore;
        public ILexiconStore LexiconStore => lexiconStore ?? (lexiconStore = FreshIOC.Container.Resolve<ILexiconStore>());

        IPostStore postStore;
        public IPostStore PostStore => postStore ?? (postStore = FreshIOC.Container.Resolve<IPostStore>());

        IPostCategoryStore postCategoryStore;
        public IPostCategoryStore PostCategoryStore => postCategoryStore ?? (postCategoryStore = FreshIOC.Container.Resolve<IPostCategoryStore>());

        ITagUserPostStore tagUserPostStore;
        public ITagUserPostStore TagUserPostStore => tagUserPostStore ?? (tagUserPostStore = FreshIOC.Container.Resolve<ITagUserPostStore>());

        IUserStore userStore;
        public IUserStore UserStore => userStore ?? (userStore = FreshIOC.Container.Resolve<IUserStore>());

        IUserSiteStore userSiteStore;
        public IUserSiteStore UserSiteStore => userSiteStore ?? (userSiteStore = FreshIOC.Container.Resolve<IUserSiteStore>());

        IUserSiteJobStore userSiteJobStore;
        public IUserSiteJobStore UserSiteJobStore => userSiteJobStore ?? (userSiteJobStore = FreshIOC.Container.Resolve<IUserSiteJobStore>());


        #region iStoreManager Implementation

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

            LoadCachedTokenAsync();
        }

        #endregion


        public async Task<MobileServiceUser> LoginAsync(string username, string password)
        {
            if (!IsInitialized)
            {
                Initialize();
            }

            var credentials = new JObject();
            credentials["email"] = username;
            credentials["password"] = password;
            credentials["mobile"] = true;

            try
            {
                var _client = new HttpClient();

                _client.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");

                var json_cred = credentials.ToString();
                var content = new StringContent(json_cred, Encoding.UTF8, "application/json");
                var uriService = new Uri(Constants.LoginURL);

                var response = await _client.PostAsync(uriService, content);

                if (response.IsSuccessStatusCode)
                {
                    var content2 = await response.Content.ReadAsStringAsync();

                    var User = JsonConvert.DeserializeObject<Authentification>(content2);

                    MobileServiceUser user = new MobileServiceUser(User.UserId) { MobileServiceAuthenticationToken = User.Token };

                    MobileService.CurrentUser = user;

                    CacheToken(user, User.Role);

                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }

        }

        public async Task<bool> ForgotPasswordAsync(string email)
        {
            if (!IsInitialized)
            {
                Initialize();
            }

            var mailObject = new JObject();
            mailObject["email"] = email;

            try
            {
                var _client = new HttpClient();
                _client.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");

                var json_cred = mailObject.ToString();
                var content = new StringContent(json_cred, Encoding.UTF8, "application/json");
                var uriService = new Uri(Constants.ForgotURL);

                var response = await _client.PostAsync(uriService, content);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }


        public async Task LogoutAsync()
        {
            if (!IsInitialized)
            {
                Initialize();
            }

            await MobileService.LogoutAsync();

            Settings.AuthToken = string.Empty;
            Settings.UserId = string.Empty;
            Settings.Role = string.Empty;

            (UserStore as UserStore).currentUser = null;
        }


        void CacheToken(MobileServiceUser user, string Role)
        {
            Settings.AuthToken = user.MobileServiceAuthenticationToken;
            Settings.UserId = user.UserId;
            Settings.Role = Role;
        }


        void LoadCachedTokenAsync()
        {

            if ((!string.IsNullOrEmpty(Settings.AuthToken)) && (!string.IsNullOrEmpty(Settings.UserId)))
            {
                try
                {
                    var getExpiration = JwtUtility.GetTokenExpiration(Settings.AuthToken);
                    var dateNow = DateTimeOffset.Now;

                    if (!string.IsNullOrEmpty(Settings.AuthToken) && JwtUtility.GetTokenExpiration(Settings.AuthToken) > DateTimeOffset.Now)
                    {
                        MobileService.CurrentUser = new MobileServiceUser(Settings.UserId);
                        MobileService.CurrentUser.MobileServiceAuthenticationToken = Settings.AuthToken;
                    }

                }
                catch (InvalidTokenException)
                {
                    Settings.AuthToken = string.Empty;
                    Settings.UserId = string.Empty;

                }
            }
        }

        public async Task VerifyTokenAsync()
        {

            if ((!string.IsNullOrEmpty(Settings.AuthToken)) && (!string.IsNullOrEmpty(Settings.UserId)))
            {
                try
                {
                    var date = JwtUtility.GetTokenExpiration(Settings.AuthToken).Value.AddMinutes(-30);

                    if (!string.IsNullOrEmpty(Settings.AuthToken) && date < DateTime.UtcNow)
                    {
                        var result = await RegenerateTokenAsync();

                        if (!result)
                        {
                            //no token regenerated
                            await LogoutAsync();
                        }
                    }
                }
                catch (InvalidTokenException)
                {
                    //Token exception error
                    await LogoutAsync();
                }
            }

        }

        private async Task<bool> RegenerateTokenAsync()
        {
            var uri = new Uri(Constants.RegenerateURL);

            var actualToken = Settings.AuthToken;

            try
            {
                var _client = new HttpClient();
                _client.DefaultRequestHeaders.Add("token", actualToken);
                _client.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");

                var response = await _client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var authentification = JsonConvert.DeserializeObject<Authentification>(content);
                    MobileServiceUser user = new MobileServiceUser(authentification.UserId) { MobileServiceAuthenticationToken = authentification.Token };
                    MobileService.CurrentUser = user;

                    CacheToken(user, authentification.Role);

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        //Register
        public async Task<MobileServiceUser> RegisterAsync(UserRegistrationInfo userRegistrationInfo)
        {
            try
            {
                var uriService = new Uri(Constants.RegisterURL);
                var _client = new HttpClient();
                _client.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");

                var json_customerRegistration = JsonConvert.SerializeObject(userRegistrationInfo);
                var content = new StringContent(json_customerRegistration, Encoding.UTF8, "application/json");

                var response = await _client.PostAsync(uriService, content);

                if (response.IsSuccessStatusCode)
                {
                    var content2 = await response.Content.ReadAsStringAsync();
                    var Authentification = JsonConvert.DeserializeObject<Authentification>(content2);

                    MobileServiceUser mobileServiceUser = new MobileServiceUser(Authentification.UserId) { MobileServiceAuthenticationToken = Authentification.Token };

                    MobileService.CurrentUser = mobileServiceUser;

                    CacheToken(mobileServiceUser, Authentification.Role);

                    return mobileServiceUser;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
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
