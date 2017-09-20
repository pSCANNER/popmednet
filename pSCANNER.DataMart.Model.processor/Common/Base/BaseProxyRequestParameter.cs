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

using Lpp.Scanner.DataMart.Model.Processors.Common.Parameters;
using Lpp.Scanner.DataMart.Model.Processors.DataSetMapping;
using Lpp.Scanner.DataMart.Model.Processors.DataSetMapping.Configuration;
using pSCANNER.DataMart.Model.processor.DataSetMapping.Configuration;

#endregion Using

namespace Lpp.Scanner.DataMart.Model.Processors.Common.Base {

    public abstract class BaseProxyRequestParameter : BaseRequestParameter, IUserJsonInputParameter {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseProxyRequestParameter"/> class.
        /// </summary>
        /// <param name="requestParameter">The request parameter.</param>
        protected BaseProxyRequestParameter(BaseRequestParameter requestParameter) : base(requestParameter) { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseProxyRequestParameter"/> class.
        /// </summary>
        /// <param name="requestId">The request identifier.</param>
        /// <param name="dataSetJson">The data set json.</param>
        /// <param name="parametersJson">The parameters json.</param>
        /// <param name="pmmlJson">The PMML json.</param>
        /// <param name="dataSetConnection">The data set connection.</param>
        /// <param name="requestFor">The request for.</param>
        protected BaseProxyRequestParameter(string requestId, string dataSetJson, string parametersJson, string pmmlJson, IDataSetConnection dataSetConnection, RequestForEnum requestFor)
            : base(requestId, requestFor, dataSetConnection) {
            DatasetJson = dataSetJson;
            ParametersJson = parametersJson;
            PmmlJson = pmmlJson;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseProxyRequestParameter"/> class.
        /// </summary>
        /// <param name="requestId">The request identifier.</param>
        /// <param name="requestFor">The request for.</param>
        protected BaseProxyRequestParameter(string requestId, RequestForEnum requestFor) : base(requestId, requestFor) { }

        #endregion Constructors

        #region Properties

        /// <summary>
        ///     Gets the dataset json.
        /// </summary>
        /// <value>The dataset json.</value>
        public string DatasetJson { get; protected set; }

        /// <summary>
        ///     Gets the parameters json.
        /// </summary>
        /// <value>The parameters json.</value>
        public string ParametersJson { get; protected set; }

        /// <summary>
        ///     Gets the PMML json.
        /// </summary>
        /// <value>The PMML json.</value>
        public string PmmlJson { get; protected set; }

        #endregion Properties
    }
}