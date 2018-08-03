using System;
using Newtonsoft.Json;

namespace Mindurry.Models.DataObjects
{
    public class User : BaseDataObject
    {
        [JsonProperty("firstname")]
        public string Firstname { get; set; }

        [JsonProperty("lastname")]
        public string Lastname { get; set; }

        [JsonIgnore]
        public string Name => $"{Firstname} {Lastname}";

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("phoneMobile")]
        public string PhoneMobile { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }

        [JsonProperty("invitationSentAt")]
        public string InvitationSentAt { get; set; }

        [JsonProperty("iosOneSignalID")]
        public string IosOneSignalId { get; set; }

        [JsonProperty("androidOneSignalID")]
        public string AndroidOneSignalId { get; set; }

    }

    public enum Role { administrator, employee };
}
