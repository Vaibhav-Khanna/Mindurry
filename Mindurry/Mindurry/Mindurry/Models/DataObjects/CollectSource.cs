using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mindurry.Models.DataObjects
{
    public class CollectSource : BaseDataObject
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("orderDisplay")]
        public string OrderDisplay { get; set; }

    }
}
