using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Lpp.Dns.DTO.Enums {

    /// <summary>
    /// </summary>
    [DataContract]
    public enum QueryComposerCriteriaTypes {

        /// <summary>
        ///     The paragraph
        /// </summary>
        [EnumMember]
        Paragraph = 0,

        /// <summary>
        ///     The event
        /// </summary>
        [EnumMember]
        Event = 1,

        /// <summary>
        ///     The index event
        /// </summary>
        [EnumMember, Description("Index Event")]
        IndexEvent = 2
    }
}