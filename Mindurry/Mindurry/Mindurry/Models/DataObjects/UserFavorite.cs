using System;
using Newtonsoft.Json;

namespace Mindurry.Models.DataObjects
{
    public class UserFavorite : BaseDataObject
    {
        [JsonProperty("residenceId")]
        public string ResidenceId { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }

    }
}
