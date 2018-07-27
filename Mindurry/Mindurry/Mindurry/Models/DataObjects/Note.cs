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

        [JsonProperty("databaseInsertAt")]
        public DateTimeOffset DatabaseInsertAt { get; set; }

        [JsonProperty("reminderAt")]
        public DateTimeOffset ReminderAt { get; set; }

        [JsonProperty("activityStreamDate")]
        public DateTimeOffset ActivityStreamDate { get; set; }

        [JsonProperty("contactId")]
        public string ContactId { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("userLastname")]
        public string UserLastname { get; set; }

        [JsonProperty("userFirstname")]
        public string UserFirstname { get; set; }

        [JsonProperty("contactFirstname")]
        public string ContactFirstname { get; set; }

        [JsonProperty("contactLastname")]
        public string ContactLastname { get; set; }

        [JsonProperty("contactCompanyName")]
        public string ContactCompanyName { get; set; }

        [JsonProperty("kind")]
        public string Kind { get; set; }

        [JsonProperty("referenceId")]
        public string ReferenceId { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("extra1")]
        public string Extra1 { get; set; }

        [JsonProperty("contentHasRGPDData")]
        public bool contentHasRGPDData { get; set; }

    }
}
