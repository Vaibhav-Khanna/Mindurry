using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mindurry.Models.DataObjects
{
    public class ContactCustomField : BaseDataObject
    {
        [JsonProperty("accountId")]
        public string AccountId { get; set; }

        [JsonProperty("contactId")]
        public string ContactId { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("contactCustomFieldSourceEntryId")]
        public string ContactCustomFieldSourceEntryId { get; set; }

        [JsonProperty("contactCustomFieldSourceId")]
        public string ContactCustomFieldSourceId { get; set; }

        [JsonProperty("contactCustomFieldSourceInternalName")]
        public string ContactCustomFieldSourceInternalName { get; set; }

        [JsonProperty("contactCustomFieldSourceEntryValue")]
        public string ContactCustomFieldSourceEntryValue { get; set; }

        [JsonProperty("insertedDate")]
        public DateTime InsertedDate { get; set; }
       

    }
}
