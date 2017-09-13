using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace Lpp.Dns.DTO.Scanner
{
    /// <summary>
    /// Scanner Data Field
    /// </summary>
    [DataContract]
    public class ScannerDataFieldDTO
    {
        /// <summary>
        /// Name of the data field
        /// </summary>
        [DataMember]
        public string name { get; set; }
        /// <summary>
        /// Option type of this field 
        /// </summary>
        [DataMember]
        public string optype { get; set; }
        /// <summary>
        /// Data type of this field
        /// </summary>
        [DataMember]
        public string dataType { get; set; }
    }
}
