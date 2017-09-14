using Lpp.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Lpp.Dns.DTO {

    /// <summary>
    /// </summary>
    [DataContract]
    public class CodeSelectorValueDTO {

        /// <summary>
        ///     Gets or sets the code.
        /// </summary>
        /// <value>The code.</value>
        [DataMember]
        public string Code { get; set; }

        /// <summary>
        ///     Gets or sets the expire date.
        /// </summary>
        /// <value>The expire date.</value>
        [DataMember]
        public DateTime? ExpireDate { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [DataMember]
        public string Name { get; set; }
    }
}