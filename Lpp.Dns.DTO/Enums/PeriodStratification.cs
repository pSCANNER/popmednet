using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Lpp.Dns.DTO.Enums {

    /// <summary>
    /// </summary>
    [DataContract]
    public enum PeriodStratification {

        /// <summary>
        ///     The monthly
        /// </summary>
        [EnumMember, Description("Monthly")]
        Monthly = 1,

        /// <summary>
        ///     The yearly
        /// </summary>
        [EnumMember, Description("Yearly")]
        Yearly = 2,
    }
}