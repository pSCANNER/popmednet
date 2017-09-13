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

using System.Net;
using WebResponse = Lpp.Scanner.DataMart.Model.Processors.Common.Parameters.Response.WebResponse;


#endregion

namespace Lpp.Scanner.DataMart.Model.Processors.Common.Processor.Adapers.Base {

    public abstract class WebAdapterBase : BaseAdapter {
        #region Properties

        /// <summary>
        ///     Gets or sets the processor response.
        /// </summary>
        /// <value>
        ///     The processor response.
        /// </value>
        protected WebResponse ProcessorResponse { get; set; }

        /// <summary>
        ///     Gets or sets the request.
        /// </summary>
        /// <value>
        ///     The request.
        /// </value>
        protected HttpWebRequest Request { get; set; }

        #endregion
    }

}