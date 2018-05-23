using Newtonsoft.Json;

namespace Mindurry.Models.DataObjects
{
    public class Document
    {
        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("internalName")]
        public string InternalName { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("referenceId")]
        public string ReferenceId { get; set; }

        [JsonProperty("referenceKind")]
        public ReferenceKind ReferenceKind { get; set; }

        [JsonProperty("documentType")]
        public DocumentType DocumentType { get; set; }

        [JsonProperty("documentVersion", NullValueHandling = NullValueHandling.Ignore)]
        public string DocumentVersion { get; set; }

    }

    public enum DocumentType { Autre, Choix, Cuisine, Definitif, Electrique, Initial, Masse, Niveau, Notice, Perspective, Plaquette, Plomberie, Signe };

    public enum ReferenceKind { Apartment, Cellar, Customer, Garage, Residence };
}
