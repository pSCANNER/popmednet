using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace Lpp.Dns.DTO.Scanner
{
    /// <summary>
    /// Scanner Analysis Screen Parameter Set
    /// </summary>
    [DataContract]
    public class ScannerScreenParameterSetDTO
    {
        /// <summary>
        /// Name of the screen parameter set
        /// </summary>
        [DataMember]
        public string name { get; set; }
        /// <summary>
        /// List of ScreenParameters
        /// </summary>
        [DataMember]
        public IEnumerable<ScannerScreenParameterDTO> parameters { get; set; }
    }
}
