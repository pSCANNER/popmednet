#region Legal Information

// ====================================================================================
//
// Center for Population Health Informatics
// Solution: Lpp.Adapters
// Project: Lpp.Scanner.DataMart.Model.Processors Last Updated By: Westerman, Dax Marek
//
// ====================================================================================

#endregion Legal Information

#region Using

using Lpp.Scanner.DataMart.Model.Processors.Common.Base;
using Lpp.Scanner.DataMart.Model.Processors.Common.Pmml;
using pSCANNER.DataMart.Model.processor.DataSetMapping.Configuration;

#endregion Using

namespace Lpp.Scanner.DataMart.Model.Processors.Common.Parameters {

    public class AggregatorClientRequestParameter : BaseProxyRequestParameter, IRExecution {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="AggregatorClientRequestParameter"/> class.
        /// </summary>
        /// <param name="requestId">The request identifier.</param>
        /// <param name="requestFor">The request for.</param>
        public AggregatorClientRequestParameter(string requestId, RequestForEnum requestFor) : base(requestId, requestFor) { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AggregatorClientRequestParameter"/> class.
        /// </summary>
        /// <param name="requestId">The request identifier.</param>
        /// <param name="dataSetJson">The data set json.</param>
        /// <param name="parametersJson">The parameters json.</param>
        /// <param name="pmmlJson">The PMML json.</param>
        /// <param name="dataSetConnection">The data set connection.</param>
        /// <param name="executionPath">The r location.</param>
        public AggregatorClientRequestParameter(string requestId, string dataSetJson, string parametersJson, string pmmlJson, IDataSetConnection dataSetConnection, string executionPath)
            : base(requestId, dataSetJson, parametersJson, pmmlJson, dataSetConnection, RequestForEnum.PostRequest) {
            ExecutionPath = executionPath;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        ///     Gets the execution path.
        /// </summary>
        /// <value>The execution path.</value>
        public string ExecutionPath { get; set; }

        /// <summary>
        ///     Gets the coefficient PMML.
        /// </summary>
        /// <value>The coefficient PMML.</value>
        public PmmlDocument Pmml { get; set; }

        #endregion Properties

        /// <summary>
        ///     Sets the PMML document.
        /// </summary>
        /// <param name="pmml">The PMML.</param>
        public void SetPmmlDocument(string pmml) {
            Pmml = PmmlParser.GetFromXmlString(pmml);
        }
    }
}