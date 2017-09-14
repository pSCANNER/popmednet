using System;
using System.Runtime.Serialization;

namespace Lpp.Dns.DTO {

    /// <summary>
    ///     Data Models
    /// </summary>
    [DataContract]
    public class DataModelProcessorDTO {

        /// <summary>
        ///     The Data Model ID
        /// </summary>
        [DataMember]
        public Guid ModelID { get; set; }

        /// <summary>
        ///     The name of the processor
        /// </summary>
        [DataMember]
        public string Processor { get; set; }

        /// <summary>
        ///     The ID of the processor
        /// </summary>
        [DataMember]
        public Guid ProcessorID { get; set; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DataModelProcessorDTO"/> class.
        /// </summary>
        public DataModelProcessorDTO() {
        }
    }
}