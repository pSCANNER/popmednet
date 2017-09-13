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

using System.IO;
using System.Net;
using Lpp.Scanner.DataMart.Model.Processors.Common.Base;
using Lpp.Scanner.DataMart.Model.Processors.Common.Models;
using Lpp.Scanner.DataMart.Model.Processors.Common.Parameters;
using Lpp.Scanner.DataMart.Model.Processors.Common.Processor.Adapers.Base;
using WebResponse = Lpp.Scanner.DataMart.Model.Processors.Common.Parameters.Response.WebResponse;


#endregion

namespace Lpp.Scanner.DataMart.Model.Processors.Common.Processor.Adapers {

    public class WebAdapter : WebAdapterBase {

        /// <summary>
        ///     Does the post request.
        /// </summary>
        /// <param name="requestParameter">The request parameter.</param>
        /// <returns></returns>
        public override ResponseBase PostRequest(BaseRequestParameter requestParameter) {
            ProcessorResponse = new WebResponse {Status = Constants.ResponseStatus.InProgress};

            var url = ((WebServiceRequestParameter) requestParameter).RequestUrl;
            Request = (HttpWebRequest) WebRequest.Create(url);
            Request.KeepAlive = false;
            Request.ProtocolVersion = HttpVersion.Version10;
            Request.ContentType = "application/xml";
            Request.Method = "GET";

            var responseText = Request.GetResponse();

            using (var responseStream = responseText.GetResponseStream()) {
                if (responseStream == null) {
                    ProcessorResponse.Status = Constants.ResponseStatus.Error;
                } else {
                    var reader = new StreamReader(responseStream);
                    ProcessorResponse.Response = reader.ReadToEnd();
                    ProcessorResponse.Status = Constants.ResponseStatus.Complete;
                }
            }

            return ProcessorResponse;
        }

        /// <summary>
        ///     Does the stop request.
        /// </summary>
        /// <param name="requestParameter">The request parameter.</param>
        /// <returns></returns>
        protected override ResponseBase StopRequest(BaseRequestParameter requestParameter) {
            Request.Abort();
            return new WebResponse {Status = Constants.ResponseStatus.Canceled};
        }

        /// <summary>
        ///     Does the get status.
        /// </summary>
        /// <param name="requestParameter">The request parameter.</param>
        /// <returns></returns>
        protected override ResponseBase GetStatus(BaseRequestParameter requestParameter) {
            return ProcessorResponse;
        }

        /// <summary>
        ///     Does the get response.
        /// </summary>
        /// <param name="requestParameter">The request parameter.</param>
        /// <returns></returns>
        protected override ResponseBase GetResponse(BaseRequestParameter requestParameter) {
            return new WebResponse {Status = Constants.ResponseStatus.InProgress};
        }

    }

}