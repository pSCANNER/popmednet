using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace Lpp.Dns.DTO.Scanner
{
    /// <summary>
    /// Scanner Study Protocol specification
    /// </summary>
    [DataContract]
    public class ScannerStudyProtocolDTO
    {
        /// <summary>
        /// The Identifier of each StudyProtocol entry within a list of protocols (not a GUID)
        /// </summary>
        [DataMember]
        public string Identifier { get; set; }
        /// <summary>
        /// Library Name
        /// </summary>
        [DataMember]
        public string LibraryName { get; set; }
        /// <summary>
        /// Method Name
        /// </summary>
        [DataMember]
        public string MethodName { get; set; }
        /// <summary>
        /// Library Method URL
        /// </summary>
        [DataMember]
        public string LibraryMethodURL { get; set; }
        /// <summary>
        /// DataSet Name
        /// </summary>
        [DataMember]
        public string DataSetName { get; set; }
        /// <summary>
        /// DataSet URL
        /// </summary>
        [DataMember]
        public string DataSetURL { get; set; }
        /// <summary>
        /// Result Release Name
        /// </summary>
        [DataMember]
        public string ResultReleaseName { get; set; }
    }
}
