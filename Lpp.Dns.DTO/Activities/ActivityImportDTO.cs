using Newtonsoft.Json;
using System.Collections.Generic;

namespace Lpp.Dns.DTO {

    /// <summary>
    /// </summary>
    public class ActivityImportDTO {

        /// <summary>
        ///     Gets or sets the acronym.
        /// </summary>
        /// <value>The acronym.</value>
        [JsonProperty("activity_acronym")]
        public string Acronym { get; set; }

        /// <summary>
        ///     Gets or sets the activities.
        /// </summary>
        /// <value>The activities.</value>
        [JsonProperty("subactivities")]
        public ICollection<SubActivityImportDTO> Activities { get; set; }

        /// <summary>
        ///     Gets or sets the key.
        /// </summary>
        /// <value>The key.</value>
        [JsonProperty("activity_id")]
        public int? Key { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [JsonProperty("activity_name")]
        public string Name { get; set; }
    }

    /// <summary>
    /// </summary>
    public class SubActivityImportDTO {

        /// <summary>
        ///     Gets or sets the acronym.
        /// </summary>
        /// <value>The acronym.</value>
        [JsonProperty("subactivity_acronym")]
        public string Acronym { get; set; }

        /// <summary>
        ///     Gets or sets the key.
        /// </summary>
        /// <value>The key.</value>
        [JsonProperty("subactivity_id")]
        public int? Key { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [JsonProperty("subactivity_name")]
        public string Name { get; set; }
    }

    /// <summary>
    /// </summary>
    public class TaskOrderImportDTO {

        /// <summary>
        ///     Gets or sets the activities.
        /// </summary>
        /// <value>The activities.</value>
        public ICollection<ActivityImportDTO> Activities { get; set; }

        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public string ID { get; set; }
    }
}