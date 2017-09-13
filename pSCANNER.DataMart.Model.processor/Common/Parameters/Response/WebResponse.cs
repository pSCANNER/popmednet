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


#endregion

namespace Lpp.Scanner.DataMart.Model.Processors.Common.Parameters.Response {

    public class WebResponse : ResponseBase {
        #region Properties

        /// <summary>
        ///     Gets or sets the response.
        /// </summary>
        /// <value>
        ///     The response.
        /// </value>
        public string Response { get; set; }

        #endregion
    }

}