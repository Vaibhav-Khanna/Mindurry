using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mindurry.Models.DataObjects
{
    public class UserSalesTeam : BaseDataObject
    {
        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("salesTeamId")]
        public string SalesTeamId { get; set; }
    }
}
