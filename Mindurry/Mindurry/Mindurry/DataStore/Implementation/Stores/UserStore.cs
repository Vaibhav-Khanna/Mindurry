using Mindurry.DataStore.Abstraction.Stores;
using Mindurry.Models.DataObjects;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mindurry.DataStore.Implementation.Stores
{
    public class UserStore : BaseStore<User>, IUserStore
    {
        public override string Identifier => "User";

        public async Task<User> GetProfileAsync(string token)
        {
            var uri = new Uri(Constants.ProfileURL);

            try
            {
                var _client = new HttpClient();
               
                _client.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");
                _client.DefaultRequestHeaders.Add("X-ZUMO-AUTH", token);

                var response = await _client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var user = JsonConvert.DeserializeObject<User>(content);

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

    }
}