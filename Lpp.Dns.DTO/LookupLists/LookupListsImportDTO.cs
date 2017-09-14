using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Lpp.Dns.DTO {

    /// <summary>
    /// </summary>
    public class LookupListsImportDTO {

        /// <summary>
        ///     Gets or sets the metadata.
        /// </summary>
        /// <value>The metadata.</value>
        [JsonProperty("meta")]
        public LookupListsMetadataImportDTO Metadata { get; set; }

        /// <summary>
        ///     Gets or sets the results.
        /// </summary>
        /// <value>The results.</value>
        [JsonProperty("results")]
        public LookupListsResultsImport Results { get; set; }
    }

    /// <summary>
    /// </summary>
    public class LookupListsMetadataImportDTO {

        /// <summary>
        ///     Gets or sets the last updated date.
        /// </summary>
        /// <value>The last updated date.</value>
        [JsonProperty("last_updated")]
        public string lastUpdatedDate { get; set; }
    }

    /// <summary>
    /// </summary>
    public class LookupListsResultsImport {

        /// <summary>
        ///     Gets or sets the category.
        /// </summary>
        /// <value>The category.</value>
        [JsonProperty("category")]
        public string Category { get; set; }

        /// <summary>
        ///     Gets or sets the class.
        /// </summary>
        /// <value>The class.</value>
        [JsonProperty("class")]
        public string Class { get; set; }

        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [JsonProperty("id")]
        public string id { get; set; }

        /// <summary>
        ///     Gets or sets the latest version.
        /// </summary>
        /// <value>The latest version.</value>
        [JsonProperty("latest")]
        public LookupListsVersionImport LatestVersion { get; set; }

        /// <summary>
        ///     Gets or sets the source.
        /// </summary>
        /// <value>The source.</value>
        [JsonProperty("source")]
        public string Source { get; set; }

        /// <summary>
        ///     Gets or sets the versions.
        /// </summary>
        /// <value>The versions.</value>
        [JsonProperty("versions")]
        public ICollection<LookupListsVersionImport> Versions { get; set; }
    }

    /// <summary>
    /// </summary>
    public class LookupListsVersionImport {

        /// <summary>
        ///     Gets or sets the URL.
        /// </summary>
        /// <value>The URL.</value>
        [JsonProperty("href")]
        public string url { get; set; }

        /// <summary>
        ///     Gets or sets the version identifier.
        /// </summary>
        /// <value>The version identifier.</value>
        [JsonProperty("id")]
        public string VersionID { get; set; }
    }
}