using System;
using Newtonsoft.Json;

namespace Mindurry.Models.DataObjects
{
    public class Terrace : BaseDataObject
    {
        [JsonProperty("residenceId")]
        public string ResidenceId { get; set; }

        [JsonProperty("residenceName")]
        public string ResidenceName { get; set; }

        [JsonProperty("apartmentId")]
        public string ApartmentId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("exposure")]
        public string Exposure { get; set; }

        [JsonProperty("area")]
        public long Area { get; set; }

    }

}
