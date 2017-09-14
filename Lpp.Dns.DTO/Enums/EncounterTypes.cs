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
    public enum PatientEncounterTypes {

        /// <summary>
        ///     The unknown
        /// </summary>
        [EnumMember, Description("Unknown")]
        Unknown = 0,

        /// <summary>
        ///     The ambulatory
        /// </summary>
        [EnumMember, Description("Ambulatory Visit")]
        Ambulatory = 1,

        /// <summary>
        ///     The emergency
        /// </summary>
        [EnumMember, Description("Emergency Department")]
        Emergency = 2,

        /// <summary>
        ///     The emergency admit
        /// </summary>
        [EnumMember, Description("Emergency Department Admit to Inpatient Hospital Stay")]
        EmergencyAdmit = 8,

        /// <summary>
        ///     The inpatient
        /// </summary>
        [EnumMember, Description("Inpatient Hospital Stay")]
        Inpatient = 3,

        /// <summary>
        ///     The institutional
        /// </summary>
        [EnumMember, Description("Non-Acute Institutional Stay")]
        Institutional = 4,

        /// <summary>
        ///     The other ambulatory
        /// </summary>
        [EnumMember, Description("Other Ambulatory Visit")]
        OtherAmbulatory = 5,

        /// <summary>
        ///     The other
        /// </summary>
        [EnumMember, Description("Other")]
        Other = 6,

        /// <summary>
        ///     The no information
        /// </summary>
        [EnumMember, Description("No Information")]
        NoInformation = 7
    }
}