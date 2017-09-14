using Lpp.Dns.DTO.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Lpp.Dns.DTO.QueryComposer {

    /// <summary>
    ///     Query composer Term
    /// </summary>
    [DataContract]
    public class QueryComposerTermDTO {

        /// <summary>
        ///     Gets or sets the criteria.
        /// </summary>
        /// <value>The criteria.</value>
        [DataMember]
        public IEnumerable<QueryComposerCriteriaDTO> Criteria { get; set; }

        /// <summary>
        ///     The key design elements for the term
        /// </summary>
        /// <value>The design.</value>
        [DataMember]
        public DesignDTO Design { get; set; }

        /// <summary>
        ///     Querycomposer Operators
        /// </summary>
        /// <value>The operator.</value>
        [DataMember]
        public QueryComposerOperators Operator { get; set; }

        /// <summary>
        ///     The name of the type of the term. This should be a defined type.
        /// </summary>
        /// <value>The type.</value>
        [DataMember]
        public Guid Type { get; set; }

        /// <summary>
        ///     Term values
        /// </summary>
        /// <value>The values.</value>
        [DataMember, JsonExtensionData]
        public Dictionary<string, object> Values { get; set; }
    }
}