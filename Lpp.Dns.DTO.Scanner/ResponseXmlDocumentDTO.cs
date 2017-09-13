using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace Lpp.Dns.DTO.Scanner
{
    /// <summary>
    /// Scanner DataSet
    /// </summary>
    [DataContract]
    public class ResponseXmlDocumentDTO
    {
        /// <summary>
        /// DataSet Name
        /// </summary>
        [DataMember]
        public Guid ResponseID { get; set; }
        /// <summary>
        /// DataDictionary for the DataSet
        /// </summary>
        [DataMember]
        public string ResponseContent { get; set; }
    }
}
