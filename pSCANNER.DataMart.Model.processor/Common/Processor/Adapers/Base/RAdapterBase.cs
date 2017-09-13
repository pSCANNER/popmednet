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

using Lpp.Scanner.DataMart.Model.Processors.Common.Models;
using Lpp.Scanner.DataMart.Model.Processors.Common.Parameters.Response;


#endregion

namespace Lpp.Scanner.DataMart.Model.Processors.Common.Processor.Adapers.Base {

    /// <summary>
    /// </summary>
    /// <seealso cref="BaseAdapter" />
    public abstract class RAdapterBase : BaseAdapter {

        private RResponse _response;

        #region Properties

        /// <summary>
        ///     Gets or sets the response.
        /// </summary>
        /// <value>
        ///     The response.
        /// </value>
        protected RResponse Response {
            get {
                return (_response ?? (_response = new RResponse(){Status = Constants.ResponseStatus.Undefined}));
            }
            set {
                _response = value;
            }
        }

        #endregion
    }

}