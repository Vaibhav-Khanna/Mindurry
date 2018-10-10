using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mindurry.Models.DataObjects
{
    public class SalesTeam : BaseDataObject
    {
        [JsonProperty("name")]
        public string Name { get; set; }

    }
}
