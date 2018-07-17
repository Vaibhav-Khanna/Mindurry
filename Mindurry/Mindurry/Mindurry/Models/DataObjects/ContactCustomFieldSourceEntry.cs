using Newtonsoft.Json;

namespace Mindurry.Models.DataObjects
{
    public class ContactCustomFieldSourceEntry : BaseDataObject
    {
        [JsonProperty("accountId")]
        public string AccountId { get; set; }

        [JsonProperty("contactCustomFieldSourceId")]
        public string ContactCustomFieldSourceId { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("displayOrder")]
        public string DisplayOrder { get; set; }

        [JsonProperty("externalId")]
        public string ExternalId { get; set; }

    }
}

