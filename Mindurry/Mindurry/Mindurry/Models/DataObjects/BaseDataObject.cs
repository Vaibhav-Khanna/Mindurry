using System;
using Newtonsoft.Json;

namespace Mindurry.Models.DataObjects
{
    public class BaseDataObject : IBaseDataObject
    {
        public BaseDataObject()
        {
            Id = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Id for item
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Azure created at time stamp
        /// </summary>
        [JsonIgnore]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Azure UpdateAt timestamp for online/offline sync
        /// </summary>
        [JsonIgnore]
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// Azure version for online/offline sync
        /// </summary>
        [JsonIgnore]
        public string Version { get; set; }

        [JsonIgnore]
        public bool Deleted { get; set; }

    }

    public interface IBaseDataObject
    {
        string Id { get; set; }

        string Version { get; set; }

        DateTimeOffset CreatedAt { get; set; }

        DateTimeOffset UpdatedAt { get; set; }

        bool Deleted { get; set; }

    }

}


