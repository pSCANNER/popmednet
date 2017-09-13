#region Legal Information

// ====================================================================================
//  
//      Center for Population Health Informatics
//      Solution: Lpp.Adapters
//      Project: Lpp.Scanner.DataMart.Model.Processors
//      Last Updated By: Westerman, Dax Marek
// 
// ====================================================================================

#endregion

#region Using

using System.Collections.Generic;
using Lpp.Scanner.DataMart.Model.Processors.Common.Base;


#endregion

namespace Lpp.Scanner.DataMart.Model.Processors.Common.Parameters {

    public class AggregatorServerRequestParameter : BaseRequestParameter, IUserJsonInputParameter, IRExecution {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="AggregatorServerRequestParameter" /> class.
        /// </summary>
        /// <param name="requestId">The request identifier.</param>
        /// <param name="rLocationPath">The r location path.</param>
        /// <param name="siteResponses">The site responses.</param>
        /// <param name="parameters"></param>
        /// <param name="siteNames"></param>
        public AggregatorServerRequestParameter(string requestId, string rLocationPath, IList<string> siteResponses, string parameters, string[] siteNames) : base(requestId, RequestForEnum.PostRequest) {
            SiteResponses = siteResponses;
            ExecutionPath = rLocationPath;
            ParametersJson = parameters;
            SiteNames = siteNames;
        }

        public string[] SiteNames { get; private set; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AggregatorServerRequestParameter" /> class.
        /// </summary>
        /// <param name="requestId">The request identifier.</param>
        /// <param name="requestFor">The request for.</param>
        public AggregatorServerRequestParameter(string requestId, RequestForEnum requestFor) : base(requestId, requestFor) {}

        #endregion

        #region Properties

        public string DatasetJson { get; private set; }

        /// <summary>
        ///     Gets the execution path.
        /// </summary>
        /// <value>
        ///     The execution path.
        /// </value>
        public string ExecutionPath { get; private set; }

        /// <summary>
        /// Gets the parameters json.
        /// </summary>
        /// <value>
        /// The parameters json.
        /// </value>
        public string ParametersJson { get; set; }

        /// <summary>
        /// Gets the PMML json.
        /// </summary>
        /// <value>
        /// The PMML json.
        /// </value>
        public string PmmlJson { get; private set; }

        /// <summary>
        ///     Gets the PMML input docs.
        /// </summary>
        /// <value>
        ///     The PMML input docs.
        /// </value>
        public IList<string> SiteResponses { get; private set; }

        public int AggregationCount { get; set; }

        public int MaxAggregationCount { get; set; }

        #endregion
    }

}