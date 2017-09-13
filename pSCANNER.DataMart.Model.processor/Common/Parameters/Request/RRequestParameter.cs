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

#endregion

#region Using

using Lpp.Scanner.DataMart.Model.Processors.Common.Base;
using Lpp.Scanner.DataMart.Model.Processors.DataSetMapping;


#endregion

namespace Lpp.Scanner.DataMart.Model.Processors.Common.Parameters.Request {

    public class RRequestParameter : BaseAdapterRequestParameter, IRExecution {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="RRequestParameter" /> class.
        /// </summary>
        /// <param name="requestFor">The request for.</param>
        /// <param name="requestId">The request identifier.</param>
        /// <param name="dataSetConnection"></param>
        private RRequestParameter(RequestForEnum requestFor, string requestId, BaseDataSetConnection dataSetConnection) : base(requestId, requestFor, dataSetConnection) {
            
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="RRequestParameter" /> class.
        /// </summary>
        /// <param name="rScript">The r script.</param>
        /// <param name="execPath">The execute path.</param>
        /// <param name="requestId">The request identifier.</param>
        /// <param name="requestFor">The request for.</param>
        /// <param name="dataSetConnection"></param>
        public RRequestParameter(string rScript, string execPath, string requestId, RequestForEnum requestFor, BaseDataSetConnection dataSetConnection) : this(requestFor, requestId, dataSetConnection) {
            Script = rScript;
            ExecutionPath = execPath;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the execution path.
        /// </summary>
        /// <value>
        ///     The execution path.
        /// </value>
        public string ExecutionPath { get; private set; }

        /// <summary>
        ///     Gets the script.
        /// </summary>
        /// <value>
        ///     The script.
        /// </value>
        public string Script { get; private set; }

        #endregion
    }

}