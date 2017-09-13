using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Lpp.Dns.DTO.Scanner
{
    /// <summary>
    /// Scanner Study basic Info
    /// </summary>
    [DataContract]
    public class ScannerStudyInfoDTO
    {
        /// <summary>
        /// IRBNo
        /// </summary>
        [DataMember]
        public string IRBNo { get; set; }
        /// <summary>
        /// AnalysisPlan
        /// </summary>
        [DataMember]
        public string AnalysisPlan { get; set; }
        /// <summary>
        /// Protocol
        /// </summary>
        [DataMember]
        public string Protocol { get; set; }
        /// <summary>
        /// Clinical Trials Id
        /// </summary>
        [DataMember]
        public string ClinicalTrialsId { get; set; }
        /// <summary>
        /// Start date
        /// </summary>
        [DataMember]
        public DateTimeOffset? StartDate { get; set; }
        /// <summary>
        /// End date
        /// </summary>
        [DataMember]
        public DateTimeOffset? EndDate { get; set; }
    }
}
