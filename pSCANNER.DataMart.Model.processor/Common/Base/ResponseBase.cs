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

using Lpp.Scanner.DataMart.Model.Processors.Common.Models;


#endregion

namespace Lpp.Scanner.DataMart.Model.Processors.Common.Base {

    /// <summary>
    /// </summary>
    public abstract class ResponseBase {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ResponseBase" /> class.
        /// </summary>
        protected ResponseBase() {
            Status = Constants.ResponseStatus.Undefined;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ResponseBase" /> class.
        /// </summary>
        /// <param name="responseBase">The response base.</param>
        protected ResponseBase(ResponseBase responseBase) : this() {
            Status = responseBase.Status;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the status.
        /// </summary>
        /// <value>
        ///     The status.
        /// </value>
        public Constants.ResponseStatus Status { get; set; }

        #endregion
    }

}