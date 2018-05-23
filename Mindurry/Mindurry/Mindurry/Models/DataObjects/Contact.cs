using System;
using Newtonsoft.Json;

namespace Mindurry.Models.DataObjects
{
    public class Contact
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

        [JsonProperty("qualification", NullValueHandling = NullValueHandling.Ignore)]
        public Qualification? Qualification { get; set; }

        [JsonProperty("profil", NullValueHandling = NullValueHandling.Ignore)]
        public Profil? Profil { get; set; }

        [JsonProperty("creationDate", NullValueHandling = NullValueHandling.Ignore)]
        public string CreationDate { get; set; }

        [JsonProperty("dateLead", NullValueHandling = NullValueHandling.Ignore)]
        public string DateLead { get; set; }

        [JsonProperty("dateContact", NullValueHandling = NullValueHandling.Ignore)]
        public string DateContact { get; set; }

        [JsonProperty("dateCustomer", NullValueHandling = NullValueHandling.Ignore)]
        public string DateCustomer { get; set; }

        [JsonProperty("originLead", NullValueHandling = NullValueHandling.Ignore)]
        public string OriginLead { get; set; }

        [JsonProperty("userId", NullValueHandling = NullValueHandling.Ignore)]
        public string UserId { get; set; }

    }
    public enum Qualification { Contact, Customer, Lead };

    public enum Profil { Investisseur, RésidencePrincipale, RésidenceSecondaire };
}
