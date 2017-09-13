using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace Lpp.Dns.DTO.Scanner
{
    /// <summary>
    /// Scanner Study Protocol DataSet
    /// </summary>
    [DataContract]
    public class ScannerStudyProtocolDataSetDTO
    {
        /// <summary>
        /// Name
        /// </summary>
        [DataMember]
        public string Name { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        [DataMember]
        public string Description { get; set; }
        /// <summary>
        /// DataSet URL
        /// </summary>
        [DataMember]
        public string DataSetURL { get; set; }
    }
}
