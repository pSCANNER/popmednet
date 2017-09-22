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

using System.Collections.Generic;
using Lpp.Scanner.DataMart.Model.Processors.Common.Base;
using Lpp.Scanner.DataMart.Model.Processors.Common.Pmml;

#endregion Using

namespace Lpp.Scanner.DataMart.Model.Processors.Common.Parameters {

    /// <summary>
    /// </summary>
    /// <seealso cref="Lpp.Scanner.DataMart.Model.Processors.Common.Base.BaseRequestParameter"/>
    /// <seealso cref="Lpp.Scanner.DataMart.Model.Processors.Common.Parameters.IUserJsonInputParameter"/>
    /// <seealso cref="Lpp.Scanner.DataMart.Model.Processors.Common.Parameters.IRExecution"/>
    public class AggregatorServerRequestParameter : BaseRequestParameter, IUserJsonInputParameter, IRExecution {

        #region Constructors

        /// <summary>
        ///     Gets the site names.
        /// </summary>
        /// <value>The site names.</value>
        public string[] SiteNames { get; private set; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AggregatorServerRequestParameter"/> class.
        /// </summary>
        /// <param name="requestId">The request identifier.</param>
        /// <param name="rLocationPath">The r location path.</param>
        /// <param name="siteResponses">The site responses.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="siteNames">The site names.</param>
        public AggregatorServerRequestParameter(string requestId, string rLocationPath, IList<string> siteResponses, string parameters, string[] siteNames) : base(requestId, RequestForEnum.PostRequest) {
            SiteResponses = siteResponses;
            ExecutionPath = rLocationPath;
            ParametersJson = parameters;
            SiteNames = siteNames;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AggregatorServerRequestParameter"/> class.
        /// </summary>
        /// <param name="requestId">The request identifier.</param>
        /// <param name="requestFor">The request for.</param>
        public AggregatorServerRequestParameter(string requestId, RequestForEnum requestFor) : base(requestId, requestFor) { }

        #endregion Constructors

        #region Properties

        /// <summary>
        ///     Sets the PMML document.
        /// </summary>
        /// <param name="pmml">The PMML.</param>
        public void SetPmmlDocument(string pmml) {
            Pmml = PmmlParser.GetFromXmlString(pmml);
        }

        /// <summary>
        ///     Gets or sets the aggregation count.
        /// </summary>
        /// <value>The aggregation count.</value>
        public int AggregationCount { get; set; }

        /// <summary>
        ///     Gets the dataset json.
        /// </summary>
        /// <value>The dataset json.</value>
        public string DatasetJson { get; private set; }

        /// <summary>
        ///     Gets the execution path.
        /// </summary>
        /// <value>The execution path.</value>
        public string ExecutionPath { get; private set; }

        /// <summary>
        ///     Gets or sets the maximum aggregation count.
        /// </summary>
        /// <value>The maximum aggregation count.</value>
        public int MaxAggregationCount { get; set; }

        /// <summary>
        ///     Gets the parameters json.
        /// </summary>
        /// <value>The parameters json.</value>
        public string ParametersJson { get; set; }

        /// <summary>
        ///     Gets or sets the PMML.
        /// </summary>
        /// <value>The PMML.</value>
        public PmmlDocument Pmml { get; set; }

        /// <summary>
        ///     Gets the PMML json.
        /// </summary>
        /// <value>The PMML json.</value>
        public string PmmlJson { get; private set; }

        /// <summary>
        ///     Gets the PMML input docs.
        /// </summary>
        /// <value>The PMML input docs.</value>
        public IList<string> SiteResponses { get; private set; }

        #endregion Properties
    }
}