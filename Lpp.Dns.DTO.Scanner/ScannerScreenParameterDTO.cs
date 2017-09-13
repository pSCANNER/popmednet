using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace Lpp.Dns.DTO.Scanner
{
    /// <summary>
    /// Scanner Analysis Screen Parameter 
    /// </summary>
    [DataContract]
    public class ScannerScreenParameterDTO
    {
        /// <summary>
        /// id of the parameter field
        /// </summary>
        [DataMember]
        public string id { get; set; }
        /// <summary>
        /// Display label of the field
        /// </summary>
        [DataMember]
        public string label { get; set; }
        /// <summary>
        /// Data type of this field
        /// </summary>
        [DataMember]
        public string type { get; set; }
        /// <summary>
        /// Control to user to capure user input
        /// </summary>
        [DataMember]
        public string control { get; set; }
        /// <summary>
        /// Indicator if field is required 
        /// </summary>
        [DataMember]
        public Boolean required { get; set; }
        /// <summary>
        /// List of availableValues if applicable
        /// </summary>
        [DataMember]
        public IEnumerable<string> availableValues { get; set; }
        /// <summary>
        /// User entry for single value input. This value will be mirrored to and from the 'values' property when the data is loaded and saved.
        /// </summary>
        [DataMember]
        public string value { get; set; }
        /// <summary>
        /// List of user entries. May contain more than one value depending of the type and control
        /// </summary>
        [DataMember]
        public IEnumerable<string> values { get; set; }
        /// <summary>
        /// REGEX expression to validate the input 
        /// </summary>
        [DataMember]
        public string validationRegex { get; set; }
        /// <summary>
        /// Message to display if validation fails
        /// </summary>
        [DataMember]
        public string validationMessage { get; set; }
    }
}
