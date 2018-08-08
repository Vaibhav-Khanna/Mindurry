using System;
using Newtonsoft.Json;

namespace Mindurry.Models.DataObjects
{
    public class Cellar : BaseDataObject
    {
        [JsonProperty("residenceId")]
        public string ResidenceId { get; set; }

        [JsonProperty("residenceName")]
        public string ResidenceName { get; set; }

        [JsonProperty("lotNumberArchitect")]
        public string LotNumberArchitect { get; set; }

        [JsonProperty("lotNumberCopro")]
        public string LotNumberCopro { get; set; }

        [JsonProperty("area")]
        public double? Area { get; set; }

        [JsonProperty("price")]
        public double? Price { get; set; }

        [JsonProperty("contactId")]
        public string ContactId { get; set; }

        [JsonProperty("commandState")]
        public string CommandState { get; set; }

        [JsonProperty("stage")]
        public string Stage { get; set; }

    }
}
