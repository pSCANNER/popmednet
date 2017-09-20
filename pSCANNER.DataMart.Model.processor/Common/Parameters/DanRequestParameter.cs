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
using Lpp.Scanner.DataMart.Model.Processors.DataSetMapping.Configuration;
using pSCANNER.DataMart.Model.processor.DataSetMapping.Configuration;

#endregion Using

namespace Lpp.Scanner.DataMart.Model.Processors.Common.Parameters {

    public class DanRequestParameter : BaseProxyRequestParameter {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DanRequestParameter"/> class.
        /// </summary>
        /// <param name="requestId">The request identifier.</param>
        /// <param name="datasetJson">The dataset json.</param>
        /// <param name="parametersJson">The parameters json.</param>
        /// <param name="pmmlJson">The PMML json.</param>
        /// <param name="connection">The connection.</param>
        /// <param name="serviceUrl">The service URL.</param>
        public DanRequestParameter(string requestId, string datasetJson, string parametersJson, string pmmlJson, IDataSetConnection connection, string serviceUrl)
            : base(requestId, datasetJson, parametersJson, pmmlJson, connection, RequestForEnum.Undefined) {
            ServiceUrl = serviceUrl;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        ///     Gets the service URL.
        /// </summary>
        /// <value>The service URL.</value>
        public string ServiceUrl { get; private set; }

        #endregion Properties
    }
}