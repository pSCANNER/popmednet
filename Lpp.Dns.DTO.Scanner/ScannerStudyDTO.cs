using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace Lpp.Dns.DTO.Scanner
{
    /// <summary>
    /// Scanner Study
    /// </summary>
    [DataContract]
    public class ScannerStudyDTO
    {
        /// <summary>
        /// Study Info
        /// </summary>
        [DataMember]
        public ScannerStudyInfoDTO StudyInfo { get; set; }
        /// <summary>
        /// Study Protocols
        /// </summary>
        [DataMember]
        public IEnumerable<ScannerStudyProtocolDTO> StudyProtocols { get; set; }
    }
}
