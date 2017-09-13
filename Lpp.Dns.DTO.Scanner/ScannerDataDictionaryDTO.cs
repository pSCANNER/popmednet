using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace Lpp.Dns.DTO.Scanner
{
    /// <summary>
    /// Scanner Data Dictionary
    /// </summary>
    [DataContract]
    public class ScannerDataDictionaryDTO
    {
        /// <summary>
        /// Number of fields in the Data Dictionary
        /// </summary>
        [DataMember]
        public int numberOfFields { get; set; }
        /// <summary>
        /// Details for each field in the Data Dictionary
        /// </summary>
        [DataMember]
        public IEnumerable<ScannerDataFieldDTO> DataField { get; set; }
    }
}
