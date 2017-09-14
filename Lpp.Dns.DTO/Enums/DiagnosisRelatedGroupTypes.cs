using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Lpp.Dns.DTO.Enums {

    /// <summary>
    /// </summary>
    [DataContract]
    public enum DiagnosisRelatedGroupTypes {

        /// <summary>
        ///     The ms DRG
        /// </summary>
        [EnumMember, Description("MS-DRG")]
        MS_DRG = 0,

        /// <summary>
        ///     The CMS DRG
        /// </summary>
        [EnumMember, Description("CMS-DRG")]
        CMS_DRG = 1
    }
}