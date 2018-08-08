using System;
using Newtonsoft.Json;

namespace Mindurry.Models.DataObjects
{
    public class Apartment : BaseDataObject
    {
        [JsonProperty("residenceId")]
        public string ResidenceId { get; set; }

        [JsonProperty("residenceName")]
        public string ResidenceName { get; set; }

        [JsonProperty("contactId")]
        public string ContactId { get; set; }

        [JsonProperty("commandState")]
        public string CommandState { get; set; }

        [JsonProperty("stage")]
        public string Stage { get; set; }

        [JsonProperty("lotNumberArchitect")]
        public string LotNumberArchitect { get; set; }

        [JsonProperty("lotNumberCopro")]
        public string LotNumberCopro { get; set; }

        [JsonProperty("kind")]
        public string Kind { get; set; }

        [JsonProperty("exposure")]
        public string Exposure { get; set; }

        [JsonProperty("area")]
        public double? Area { get; set; }

        [JsonProperty("floor")]
        public double? Floor { get; set; }

        [JsonProperty("price")]
        public double? Price { get; set; }


    }
    public enum CommandState { Libre, Optionné, Reservé, Vendu };

    public enum Exposure { East, NortWest, North, NorthEast, South, SouthEast, SouthWest, West };

    public enum Kind { T1, T2, T3, T4, T5, T6 };

}
