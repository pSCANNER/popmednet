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
    public class ScannerAnalysisDTO
    {
        /// <summary>
        /// Is this analysis iterative
        /// </summary>
        [DataMember]
        public Boolean? IsIterative { get; set; }
        /// <summary>
        /// Protocol to be used for Analysis
        /// </summary>
        [DataMember]
        public ScannerStudyProtocolDTO Protocol { get; set; }
        /// <summary>
        /// DataSet definition to be used for Analysis
        /// </summary>
        [DataMember]
        public ScannerDataSetDTO DataSet { get; set; }
        /// <summary>
        /// Screen parameter set to be used to render the UI for the Analysis and to hold user input for the parameters
        /// </summary>
        [DataMember]
        public ScannerScreenParameterSetDTO ScreenParameterSet { get; set; }
        /// <summary>
        /// The PMML parameters JSON from the ScreenDataSet for the Analysis and stored as a raw JSON string
        /// </summary>
        [DataMember]
        public string PmmlParametersJson { get; set; }
        /// <summary>
        /// The PMML template JSON for the Analysis and stored as a raw JSON string
        /// </summary>
        [DataMember]
        public string PmmlJson { get; set; }
    }
}
