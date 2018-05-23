using Mindurry.DataStore.Abstraction.Stores;
using Mindurry.Models.DataObjects;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Mindurry.DataStore.Implementation.Stores
{
    public class UserStore : BaseStore<User>, IUserStore
    {
        public override string Identifier => "User";

        public User currentUser;

        public async Task<User> GetCurrentUserAsync()
        {
            var Token = await SecureStorage.GetAsync("AuthToken");
            //verif validity token
            if (!string.IsNullOrEmpty(Token))
            {

                var getExpiration = JwtUtility.GetTokenExpiration(Token);
                var dateNow = DateTimeOffset.Now;
                if (getExpiration < dateNow)
                {
                    await SecureStorage.SetAsync("AuthToken", string.Empty);

                    return null;
                }
            }
            else
            {
                return null;
            }

            string id = StoreManager.MobileService.CurrentUser?.UserId;

            if (id != null)
            {
                if (currentUser != null)
                    return currentUser;

                try
                {
                    InitializeStore();

                    var item = await Table.LookupAsync(id).ConfigureAwait(false);

                    if (item == null)
                        return null;

                    currentUser = item;

                    return currentUser;
                }
                catch (Exception)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }




    }
}
