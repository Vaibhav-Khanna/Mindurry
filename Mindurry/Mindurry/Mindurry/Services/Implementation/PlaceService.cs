using Mindurry.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mindurry.Services.Implementation
{
    public class PlaceService : Abstraction.IPlaceService
    {
        private string _apiKey;

        public PlaceService()
        {
            _apiKey = Constants.GoogleApiKey;
        }

        public async Task<WebServiceResults.Result> GetResult(string input)
        {
            var url = $"https://maps.googleapis.com/maps/api/place/autocomplete/json?input={input}&components=country:fr&language=fr&key={_apiKey}";

            HttpClient client = new HttpClient();
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<WebServiceResults.Result>(json);
            }

            return null;
        }

        public async Task<WebServiceDetailResults> GetResultDetail(string input)
        {

            var url = $"https://maps.googleapis.com/maps/api/place/details/json?placeid={input}&key={_apiKey}";
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<WebServiceDetailResults>(json);
            }

            return null;
        }


    }
}

