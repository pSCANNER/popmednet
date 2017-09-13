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
using Lpp.Scanner.DataMart.Model.Processors.Common.Parameters.Response;
using Lpp.Scanner.DataMart.Model.Processors.Common.Processor.Adapers.Base;


#endregion

namespace Lpp.Scanner.DataMart.Model.Processors.Common.Processor.Adapers.Mock {

    public class MockRAdapter : RAdapterBase {

        /// <summary>
        ///     Does the post request.
        /// </summary>
        /// <param name="requestParameter">The request parameter.</param>
        /// <returns></returns>
        public override ResponseBase PostRequest(BaseRequestParameter requestParameter) {
            var response = new RResponse();
            return response;
        }

        /// <summary>
        ///     Does the stop request.
        /// </summary>
        /// <param name="requestParameter">The request parameter.</param>
        /// <returns></returns>
        protected override ResponseBase StopRequest(BaseRequestParameter requestParameter) {
            var response = new RResponse();
            return response;
        }

        /// <summary>
        ///     Does the get status.
        /// </summary>
        /// <param name="requestParameter">The request parameter.</param>
        /// <returns></returns>
        protected override ResponseBase GetStatus(BaseRequestParameter requestParameter) {
            var response = new RResponse();
            return response;
        }

        /// <summary>
        ///     Does the get response.
        /// </summary>
        /// <param name="requestParameter">The request parameter.</param>
        /// <returns></returns>
        protected override ResponseBase GetResponse(BaseRequestParameter requestParameter) {
            var response = new RResponse();
            return response;
        }

    }

}