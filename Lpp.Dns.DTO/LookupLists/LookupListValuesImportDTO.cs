using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Lpp.Dns.DTO {

    /// <summary>
    /// </summary>
    public class LookupListValuesImportDTO {

        /// <summary>
        ///     Gets or sets the codes.
        /// </summary>
        /// <value>The codes.</value>
        [JsonProperty("results")]
        public ICollection<string[]> Codes { get; set; }

        /// <summary>
        ///     Gets or sets the metadata.
        /// </summary>
        /// <value>The metadata.</value>
        [JsonProperty("meta")]
        public MetadataImportDTO Metadata { get; set; }
    }

    /// <summary>
    /// </summary>
    public class MetadataImportDTO {

        /// <summary>
        ///     Gets or sets the last updated date.
        /// </summary>
        /// <value>The last updated date.</value>
        [JsonProperty("last_updated")]
        public string lastUpdatedDate { get; set; }

        /// <summary>
        ///     Gets or sets the results.
        /// </summary>
        /// <value>The results.</value>
        [JsonProperty("results")]
        public ResultsImportDTO results { get; set; }
    }

    /// <summary>
    /// </summary>
    public class ResultsImportDTO {

        /// <summary>
        ///     Gets or sets the count.
        /// </summary>
        /// <value>The count.</value>
        [JsonProperty("count")]
        public int count { get; set; }

        /// <summary>
        ///     Gets or sets the header.
        /// </summary>
        /// <value>The header.</value>
        [JsonProperty("header")]
        public ICollection<string> header { get; set; }

        /// <summary>
        ///     Gets or sets the page.
        /// </summary>
        /// <value>The page.</value>
        [JsonProperty("page")]
        public int page { get; set; }

        /// <summary>
        ///     Gets or sets the skip.
        /// </summary>
        /// <value>The skip.</value>
        [JsonProperty("skip")]
        public int skip { get; set; }

        /// <summary>
        ///     Gets or sets the total.
        /// </summary>
        /// <value>The total.</value>
        [JsonProperty("total")]
        public int total { get; set; }
    }
}