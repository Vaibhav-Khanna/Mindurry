using System;
using Newtonsoft.Json;

namespace Mindurry.Models.DataObjects
{
    public class Contact : BaseDataObject
    {
        [JsonProperty("firstname")]
        public string Firstname { get; set; }

        [JsonProperty("lastname")]
        public string Lastname { get; set; }

        [JsonProperty("street1", NullValueHandling = NullValueHandling.Ignore)]
        public string Street1 { get; set; }

        [JsonProperty("street2", NullValueHandling = NullValueHandling.Ignore)]
        public string Street2 { get; set; }

        [JsonProperty("zipCode", NullValueHandling = NullValueHandling.Ignore)]
        public string ZipCode { get; set; }

        [JsonProperty("state", NullValueHandling = NullValueHandling.Ignore)]
        public string State { get; set; }

        [JsonProperty("city", NullValueHandling = NullValueHandling.Ignore)]
        public string City { get; set; }

        [JsonProperty("country", NullValueHandling = NullValueHandling.Ignore)]
        public string Country { get; set; }

        [JsonProperty("longitude", NullValueHandling = NullValueHandling.Ignore)]
        public double? Longitude { get; set; }

        [JsonProperty("latitude", NullValueHandling = NullValueHandling.Ignore)]
        public double? Latitude { get; set; }

        [JsonProperty("email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }

        [JsonProperty("phone", NullValueHandling = NullValueHandling.Ignore)]
        public string Phone { get; set; }

        [JsonProperty("jobTitle", NullValueHandling = NullValueHandling.Ignore)]
        public string JobTitle { get; set; }

        [JsonProperty("qualification", NullValueHandling = NullValueHandling.Ignore)]
        public string Qualification { get; set; }

        [JsonProperty("collectSourceId", NullValueHandling = NullValueHandling.Ignore)]
        public string CollectSourceId { get; set; }
        
        [JsonProperty("userId", NullValueHandling = NullValueHandling.Ignore)]
        public string UserId { get; set; }

        [JsonIgnore]
        public string Name => $"{Firstname} {Lastname}";


    }
    public enum Qualification { Contact, Customer, Lead };

    public enum Profil { Investisseur, RésidencePrincipale, RésidenceSecondaire };
}
