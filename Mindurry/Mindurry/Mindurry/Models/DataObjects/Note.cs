using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mindurry.Models.DataObjects
{
    public class Note : BaseDataObject
    {
        [JsonProperty("doneAt")]
        public DateTimeOffset DoneAt { get; set; }

        [JsonProperty("databaseInsertAt", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset DatabaseInsertAt { get; set; }

        [JsonProperty("reminderAt", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset ReminderAt { get; set; }

        [JsonProperty("activityStreamDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset ActivityStreamDate { get; set; }

        [JsonProperty("contactId", NullValueHandling = NullValueHandling.Ignore)]
        public string ContactId { get; set; }

        [JsonProperty("userId", NullValueHandling = NullValueHandling.Ignore)]
        public string UserId { get; set; }

        [JsonProperty("userLastname", NullValueHandling = NullValueHandling.Ignore)]
        public string UserLastname { get; set; }

        [JsonProperty("userFirstname", NullValueHandling = NullValueHandling.Ignore)]
        public string UserFirstname { get; set; }

        [JsonProperty("contactFirstname", NullValueHandling = NullValueHandling.Ignore)]
        public string ContactFirstname { get; set; }

        [JsonProperty("contactLastname", NullValueHandling = NullValueHandling.Ignore)]
        public string ContactLastname { get; set; }

        [JsonProperty("contactCompanyName", NullValueHandling = NullValueHandling.Ignore)]
        public string ContactCompanyName { get; set; }

        [JsonProperty("kind", NullValueHandling = NullValueHandling.Ignore)]
        public string Kind { get; set; }

        [JsonProperty("referenceId", NullValueHandling = NullValueHandling.Ignore)]
        public string ReferenceId { get; set; }

        [JsonProperty("subject", NullValueHandling = NullValueHandling.Ignore)]
        public string Subject { get; set; }

        [JsonProperty("content", NullValueHandling = NullValueHandling.Ignore)]
        public string Content { get; set; }

        [JsonProperty("extra1", NullValueHandling = NullValueHandling.Ignore)]
        public string Extra1 { get; set; }

        [JsonProperty("contentHasRGPDData", NullValueHandling = NullValueHandling.Ignore)]
        public bool contentHasRGPDData { get; set; }

    }
}
