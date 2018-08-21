using System;
using Newtonsoft.Json;

namespace Mindurry.Models.DataObjects
{
    public class Residence : BaseDataObject
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("archived")]
        public bool Archived { get; set; }

        [JsonProperty("address1")]
        public string Address1 { get; set; }

        [JsonProperty("address2")]
        public string Address2 { get; set; }

        [JsonProperty("zipCode")]
        public string ZipCode { get; set; }

        [JsonProperty("locality")]
        public string Locality { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("formattedAddress")]
        public string FormattedAddress { get; set; }

        [JsonProperty("sciName")]
        public string SciName { get; set; }

        [JsonProperty("sciAddress1")]
        public string SciAddress1 { get; set; }

        [JsonProperty("sciAddress2")]
        public string SciAddress2 { get; set; }

        [JsonProperty("sciZipCode")]
        public string SciZipCode { get; set; }

        [JsonProperty("sciLocality")]
        public string SciLocality { get; set; }

        [JsonProperty("sciCountry")]
        public string SciCountry { get; set; }

        [JsonProperty("sciFormattedAddress")]
        public string SciFormattedAddress { get; set; }
    }
}
