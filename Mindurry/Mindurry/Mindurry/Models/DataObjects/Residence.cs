using System;
using Newtonsoft.Json;

namespace Mindurry.Models.DataObjects
{
    public class Residence : BaseDataObject
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("archived", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Archived { get; set; }

        [JsonProperty("address1", NullValueHandling = NullValueHandling.Ignore)]
        public string Address1 { get; set; }

        [JsonProperty("address2", NullValueHandling = NullValueHandling.Ignore)]
        public string Address2 { get; set; }

        [JsonProperty("zipCode", NullValueHandling = NullValueHandling.Ignore)]
        public string ZipCode { get; set; }

        [JsonProperty("locality", NullValueHandling = NullValueHandling.Ignore)]
        public string Locality { get; set; }

        [JsonProperty("country", NullValueHandling = NullValueHandling.Ignore)]
        public string Country { get; set; }

        [JsonProperty("formattedAddress", NullValueHandling = NullValueHandling.Ignore)]
        public string FormattedAddress { get; set; }

        [JsonProperty("sciName")]
        public string SciName { get; set; }

        [JsonProperty("sciAddress1", NullValueHandling = NullValueHandling.Ignore)]
        public string SciAddress1 { get; set; }

        [JsonProperty("sciAddress2", NullValueHandling = NullValueHandling.Ignore)]
        public string SciAddress2 { get; set; }

        [JsonProperty("sciZipCode", NullValueHandling = NullValueHandling.Ignore)]
        public string SciZipCode { get; set; }

        [JsonProperty("sciCountry", NullValueHandling = NullValueHandling.Ignore)]
        public string SciCountry { get; set; }

        [JsonProperty("sciLocality", NullValueHandling = NullValueHandling.Ignore)]
        public string SciLocality { get; set; }

        [JsonProperty("sciFormattedAddress", NullValueHandling = NullValueHandling.Ignore)]
        public string SciFormattedAddress { get; set; }

    }
}
