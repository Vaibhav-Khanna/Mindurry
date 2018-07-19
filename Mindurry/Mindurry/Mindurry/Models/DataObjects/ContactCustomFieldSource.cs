using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mindurry.Models.DataObjects
{
    public class ContactCustomFieldSource : BaseDataObject
    {
        [JsonProperty("accountId")]
        public string AccountId { get; set; }

        [JsonProperty("internalName")]
        public string InternalName { get; set; }

        [JsonProperty("displayOrder")]
        public string DisplayOrder { get; set; }

        [JsonProperty("displayKind")]
        public string DisplayKind { get; set; }

        [JsonProperty("screenPlacement")]
        public string ScreenPlacement { get; set; }

        [JsonProperty("canEdit")]
        public bool CanEdit { get; set; }
    }
}
