using System;
using Newtonsoft.Json;

namespace Mindurry.Models.DataObjects
{
    public class Garage : BaseDataObject
    {
        [JsonProperty("residenceId")]
        public string ResidenceId { get; set; }

        [JsonProperty("lotNumberArchitect")]
        public string LotNumberArchitect { get; set; }

        [JsonProperty("lotNumberCopro", NullValueHandling = NullValueHandling.Ignore)]
        public string LotNumberCopro { get; set; }

        [JsonProperty("area", NullValueHandling = NullValueHandling.Ignore)]
        public double? Area { get; set; }

        [JsonProperty("price", NullValueHandling = NullValueHandling.Ignore)]
        public double? Price { get; set; }

        [JsonProperty("contactId", NullValueHandling = NullValueHandling.Ignore)]
        public string ContactId { get; set; }

        [JsonProperty("commandState", NullValueHandling = NullValueHandling.Ignore)]
        public CommandState? CommandState { get; set; }

    }
}
