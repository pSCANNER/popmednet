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

    public class AsyncTask : TaskBase {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="AsyncTask" /> class.
        /// </summary>
        /// <param name="requestRequestParameter">The aggregator request parameter.</param>
        /// <param name="adapter">The adapter.</param>
        public AsyncTask(BaseRequestParameter requestRequestParameter, BaseAdapter adapter) : base(requestRequestParameter, adapter) {}

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the task.
        /// </summary>
        /// <value>
        ///     The task.
        /// </value>
        public System.Threading.Tasks.Task Task { get; private set; }

        #endregion

        /// <summary>
        ///     Starts this instance.
        /// </summary>
        public override void Start() {
            Task = new System.Threading.Tasks.Task(() => {
                Result = Adapter.PerformRequest(RequestParameter);
            });
            Task.Start();
        }

    }

}