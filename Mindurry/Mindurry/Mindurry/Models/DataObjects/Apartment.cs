using System;
using Newtonsoft.Json;

namespace Mindurry.Models.DataObjects
{
    public class Apartment : BaseDataObject
    {
        [JsonProperty("residenceId")]
        public string ResidenceId { get; set; }

        [JsonProperty("contactId", NullValueHandling = NullValueHandling.Ignore)]
        public string ContactId { get; set; }

        [JsonProperty("commandState", NullValueHandling = NullValueHandling.Ignore)]
        public CommandState? CommandState { get; set; }

        [JsonProperty("lotNumberArchitect", NullValueHandling = NullValueHandling.Ignore)]
        public string LotNumberArchitect { get; set; }

        [JsonProperty("lotNumberCopro", NullValueHandling = NullValueHandling.Ignore)]
        public string LotNumberCopro { get; set; }

        [JsonProperty("kind", NullValueHandling = NullValueHandling.Ignore)]
        public Kind? Kind { get; set; }

        [JsonProperty("exposure", NullValueHandling = NullValueHandling.Ignore)]
        public Exposure? Exposure { get; set; }

        [JsonProperty("area", NullValueHandling = NullValueHandling.Ignore)]
        public double? Area { get; set; }

        [JsonProperty("floor", NullValueHandling = NullValueHandling.Ignore)]
        public double? Floor { get; set; }

        [JsonProperty("price", NullValueHandling = NullValueHandling.Ignore)]
        public double? Price { get; set; }


    }
    public enum CommandState { Libre, Optionné, Reservé, Vendu };

    public enum Exposure { East, NortWest, North, NorthEast, South, SouthEast, SouthWest, West };

    public enum Kind { T1, T2, T3, T4, T5, T6 };

}
