using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lpp.Dns.DTO {

    /// <summary>
    /// </summary>
    public class LookupListCategoryImportDTO {

        /// <summary>
        ///     Gets or sets the categories.
        /// </summary>
        /// <value>The categories.</value>
        [JsonProperty("results")]
        public ICollection<string[]> Categories { get; set; }

        /// <summary>
        ///     Gets or sets the metadata.
        /// </summary>
        /// <value>The metadata.</value>
        [JsonProperty("meta")]
        public MetadataImportDTO Metadata { get; set; }
    }
}