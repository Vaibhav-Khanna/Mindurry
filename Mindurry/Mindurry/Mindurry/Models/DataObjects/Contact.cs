using System;
using Newtonsoft.Json;

namespace Mindurry.Models.DataObjects
{
    public class Contact : BaseDataObject
    {
        [JsonProperty("contactCreatedAt")]
        public DateTimeOffset ContactCreatedAt { get; set; }

        [JsonProperty("contactLastUpdateAt")]
        public DateTimeOffset ContactLastUpdateAt { get; set; }

        [JsonProperty("firstname")]
        public string Firstname { get; set; }

        [JsonProperty("lastname")]
        public string Lastname { get; set; }

        [JsonProperty("street1")]
        public string Street1 { get; set; }

        [JsonProperty("street2")]
        public string Street2 { get; set; }

        [JsonProperty("zipCode")]
        public string ZipCode { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("longitude")]
        public double? Longitude { get; set; }

        [JsonProperty("latitude")]
        public double? Latitude { get; set; }

        [JsonProperty("placeId")]
        public string PlaceId { get; set; }

        [JsonProperty("placeLocation")]
        public string PlaceLocation { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("jobTitle")]
        public string JobTitle { get; set; }

        [JsonProperty("qualification")]
        public string Qualification { get; set; }

        [JsonProperty("collectSourceId")]
        public string CollectSourceId { get; set; }

        [JsonProperty("collectSourceName")]
        public string CollectSourceName { get; set; }
        
        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("userFirstname")]
        public string UserFirstname { get; set; }

        [JsonProperty("userLastname")]
        public string UserLastname { get; set; }

        [JsonProperty("customFields")]
        public string CustomFields { get; set; }

        [JsonProperty("civility")]
        public string Civility { get; set; }

        [JsonProperty("creatorUserId")]
        public string CreatorUserId { get; set; }

        [JsonProperty("salesTeamId")]
        public string SalesTeamId { get; set; }

        [JsonIgnore]
        public string Name => $"{Firstname} {Lastname}";

    }
    public enum Qualification { Contact, Client, Lead };

    public static class Civility
    {
        public static string M = "M.";

        public static string Mme = "Mme";

        public static string MM = "MM.";

        public static string Mmes = "Mmes";

        public static string Mlle = "Mlle";

        public static string Mlles = "Mlles";
    }

    public enum Profil { Investisseur, RésidencePrincipale, RésidenceSecondaire };
}
