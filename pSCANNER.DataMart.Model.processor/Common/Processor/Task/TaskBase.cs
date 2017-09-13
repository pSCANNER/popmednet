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

using Lpp.Scanner.DataMart.Model.Processors.Common.Base;
using Lpp.Scanner.DataMart.Model.Processors.Common.Processor.Adapers.Base;


#endregion

namespace Lpp.Scanner.DataMart.Model.Processors.Common.Processor.Task {

    public abstract class TaskBase {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="TaskBase" /> class.
        /// </summary>
        /// <param name="requestRequestParameter">The aggregator request parameter.</param>
        /// <param name="adapter">The adapter.</param>
        protected TaskBase(BaseRequestParameter requestRequestParameter, BaseAdapter adapter) {
            RequestParameter = requestRequestParameter;
            Adapter = adapter;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TaskBase" /> class.
        /// </summary>
        protected TaskBase() {}

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the adapter.
        /// </summary>
        /// <value>
        ///     The adapter.
        /// </value>
        public BaseAdapter Adapter { get; private set; }

        /// <summary>
        ///     Gets the request count.
        /// </summary>
        /// <value>
        ///     The request count.
        /// </value>
        public int RequestCount { get; private set; }

        /// <summary>
        ///     Gets the parameter.
        /// </summary>
        /// <value>
        ///     The parameter.
        /// </value>
        public BaseRequestParameter RequestParameter { get; private set; }

        /// <summary>
        ///     Gets the result.
        /// </summary>
        /// <value>
        ///     The result.
        /// </value>
        public ResponseBase Result { get; protected set; }

        #endregion

        /// <summary>
        ///     Sets the requested again.
        /// </summary>
        public void WasRequestedAgain() {
            RequestCount++;
        }

        /// <summary>
        ///     Starts this instance.
        /// </summary>
        public abstract void Start();

    }

}