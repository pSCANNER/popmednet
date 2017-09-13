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

    public class SyncTask : TaskBase {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="SyncTask" /> class.
        /// </summary>
        /// <param name="requestRequestParameter">The aggregator request parameter.</param>
        /// <param name="adapter">The adapter.</param>
        public SyncTask(BaseRequestParameter requestRequestParameter, BaseAdapter adapter) : base(requestRequestParameter, adapter) {}

        #endregion

        /// <summary>
        ///     Starts this instance.
        /// </summary>
        public override void Start() {
            Result = Adapter.PerformRequest(RequestParameter);
        }

    }

}