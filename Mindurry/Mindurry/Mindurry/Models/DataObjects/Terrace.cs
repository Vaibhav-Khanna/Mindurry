using System;
using Newtonsoft.Json;

namespace Mindurry.Models.DataObjects
{
    public class Terrace : BaseDataObject
    {
        [JsonProperty("residenceId")]
        public string ResidenceId { get; set; }

        [JsonProperty("apartmentId")]
        public string ApartmentId { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("exposure", NullValueHandling = NullValueHandling.Ignore)]
        public Exposure? Exposure { get; set; }

        [JsonProperty("area", NullValueHandling = NullValueHandling.Ignore)]
        public double? Area { get; set; }

    }

}
