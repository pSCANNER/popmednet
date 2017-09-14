using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Lpp.Dns.DTO {

    /// <summary>
    /// </summary>
    [DataContract]
    public class HasGlobalSecurityForTemplateDTO {

        /// <summary>
        ///     Gets or sets a value indicating whether [current user has global permission].
        /// </summary>
        /// <value><c>true</c> if [current user has global permission]; otherwise, <c>false</c>.</value>
        [DataMember]
        public bool CurrentUserHasGlobalPermission { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether [security group exists for global permission].
        /// </summary>
        /// <value><c>true</c> if [security group exists for global permission]; otherwise, <c>false</c>.</value>
        [DataMember]
        public bool SecurityGroupExistsForGlobalPermission { get; set; }
    }
}