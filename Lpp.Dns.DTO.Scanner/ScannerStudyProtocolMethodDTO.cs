using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace Lpp.Dns.DTO.Scanner
{
    /// <summary>
    /// Scanner Study Protocol Method
    /// </summary>
    [DataContract]
    public class ScannerStudyProtocolMethodDTO
    {
        /// <summary>
        /// Name
        /// </summary>
        [DataMember]
        public string LibraryName { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        [DataMember]
        public string MethodName { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        [DataMember]
        public string Description { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        [DataMember]
        public string LibraryMethodURL { get; set; }
    }
}
