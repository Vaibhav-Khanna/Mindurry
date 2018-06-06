using System;
using Newtonsoft.Json;

namespace Mindurry.Models.DataObjects
{
    public class User : BaseDataObject
    {
        [JsonProperty("firstname", NullValueHandling = NullValueHandling.Ignore)]
        public string Firstname { get; set; }

        [JsonProperty("lastname", NullValueHandling = NullValueHandling.Ignore)]
        public string Lastname { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password", NullValueHandling = NullValueHandling.Ignore)]
        public string Password { get; set; }

        [JsonProperty("phoneMobile", NullValueHandling = NullValueHandling.Ignore)]
        public string PhoneMobile { get; set; }

        [JsonProperty("role", NullValueHandling = NullValueHandling.Ignore)]
        public Role? Role { get; set; }

        [JsonProperty("invitationSentAt", NullValueHandling = NullValueHandling.Ignore)]
        public string InvitationSentAt { get; set; }

        [JsonProperty("iosOneSignalID", NullValueHandling = NullValueHandling.Ignore)]
        public string IosOneSignalId { get; set; }

        [JsonProperty("androidOneSignalID", NullValueHandling = NullValueHandling.Ignore)]
        public string AndroidOneSignalId { get; set; }

    }

    public enum Role { Admin, Standard };
}
