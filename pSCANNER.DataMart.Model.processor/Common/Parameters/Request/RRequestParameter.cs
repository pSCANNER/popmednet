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
using Lpp.Scanner.DataMart.Model.Processors.DataSetMapping;
using pSCANNER.DataMart.Model.processor.DataSetMapping.Configuration;

#endregion Using

namespace Lpp.Scanner.DataMart.Model.Processors.Common.Parameters.Request {

    /// <summary>
    /// </summary>
    /// <seealso cref="Lpp.Scanner.DataMart.Model.Processors.Common.Base.BaseAdapterRequestParameter"/>
    /// <seealso cref="Lpp.Scanner.DataMart.Model.Processors.Common.Parameters.IRExecution"/>
    public class RRequestParameter : BaseAdapterRequestParameter, IRExecution {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="RRequestParameter"/> class.
        /// </summary>
        /// <param name="rScript">The r script.</param>
        /// <param name="execPath">The execute path.</param>
        /// <param name="requestId">The request identifier.</param>
        /// <param name="requestFor">The request for.</param>
        /// <param name="dataSetConnection">The data set connection.</param>
        public RRequestParameter(string rScript, string execPath, string requestId, RequestForEnum requestFor, IDataSetConnection dataSetConnection) : this(requestFor, requestId, dataSetConnection) {
            Script = rScript;
            ExecutionPath = execPath;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="RRequestParameter"/> class.
        /// </summary>
        /// <param name="requestFor">The request for.</param>
        /// <param name="requestId">The request identifier.</param>
        /// <param name="dataSetConnection">The data set connection.</param>
        private RRequestParameter(RequestForEnum requestFor, string requestId, IDataSetConnection dataSetConnection) : base(requestId, requestFor, dataSetConnection) {
        }

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
        ///     Gets the execution path.
        /// </summary>
        /// <value>The execution path.</value>
        public string ExecutionPath { get; private set; }

        /// <summary>
        ///     Gets or sets the PMML.
        /// </summary>
        /// <value>The PMML.</value>
        public PmmlDocument Pmml { get; set; }

        /// <summary>
        ///     Gets the script.
        /// </summary>
        /// <value>The script.</value>
        public string Script { get; private set; }

        #endregion Properties
    }
}