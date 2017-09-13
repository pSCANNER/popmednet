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
    public class ScannerStudySiteProtocolsDTO
    {
        /// <summary>
        /// DataMart ID
        /// </summary>
        [DataMember]
        public Guid? ID { get; set; }
        /// <summary>
        /// DataMart Name
        /// </summary>
        [DataMember]
        public string DataMartName { get; set; }
        /// <summary>
        /// DataMart Study Protocols
        /// </summary>
        [DataMember]
        public IEnumerable<ScannerStudyProtocolDTO> Protocols { get; set; }
    }
}
