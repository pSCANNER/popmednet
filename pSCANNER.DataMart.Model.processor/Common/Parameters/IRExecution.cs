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

namespace Lpp.Scanner.DataMart.Model.Processors.Common.Parameters {

    public interface IRExecution {
        #region Properties

        /// <summary>
        ///     Gets the execution path.
        /// </summary>
        /// <value>
        ///     The execution path.
        /// </value>
        string ExecutionPath { get; }

        #endregion
    }

}