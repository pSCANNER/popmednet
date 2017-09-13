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

using System.Text.RegularExpressions;
using Lpp.Scanner.DataMart.Model.Processors.Common.Models;
using Lpp.Scanner.DataMart.Model.Processors.Common.Parameters.Response;
using Lpp.Scanner.DataMart.Model.Processors.Common.Processor.Task;


#endregion

namespace Lpp.Scanner.DataMart.Model.Processors.Common.Base {

    public abstract class ProxyBase {
        #region Fields

        /// <summary>
        ///     The status regex
        /// </summary>
        protected readonly Regex StatusRegex = new Regex(StatusExpression, RegexOptions.Compiled | RegexOptions.IgnoreCase);

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the status expression.
        /// </summary>
        /// <value>
        ///     The status expression.
        /// </value>
        protected static string StatusExpression {
            get {
                return string.Format("{0}=(?<{0}>{1}|{2}|{3}|{4})", Constants.Processor.Output.RegExTokens.Status, Constants.ResponseStatus.Complete, Constants.ResponseStatus.Error, Constants.ResponseStatus.InProgress, Constants.ResponseStatus.Canceled);
            }
        }

        #endregion

        /// <summary>
        ///     Posts the request.
        /// </summary>
        /// <param name="requestParameter"></param>
        public ResponseBase PostRequest(BaseRequestParameter requestParameter) {
            requestParameter.SetRequestFor(BaseRequestParameter.RequestForEnum.PostRequest);
            return DoPostRequest(requestParameter);
        }

        /// <summary>
        ///     Gets the response.
        /// </summary>
        /// <param name="requestParameter"></param>
        /// <returns></returns>
        public ResponseBase GetResponse(BaseRequestParameter requestParameter) {
            requestParameter.SetRequestFor(BaseRequestParameter.RequestForEnum.GetResponse);
            return DoGetResponse(requestParameter);
        }

        /// <summary>
        ///     Stops the specified service URL.
        /// </summary>
        /// <param name="requestParameter">The request parameter.</param>
        /// <returns></returns>
        public ResponseBase Stop(BaseRequestParameter requestParameter) {
            requestParameter.SetRequestFor(BaseRequestParameter.RequestForEnum.StopRequest);

            ResponseBase responseFromServer = new StatusResponse();

            if (GetTask(requestParameter) != null) {
                responseFromServer = DoStop(requestParameter);
                RemoveTask(requestParameter);
            }

            responseFromServer.Status = Constants.ResponseStatus.Canceled;

            return responseFromServer;
        }

        /// <summary>
        ///     Gets the status.
        /// </summary>
        /// <param name="requestParameter"></param>
        /// <returns></returns>
        public ResponseBase GetStatus(BaseRequestParameter requestParameter) {
            requestParameter.SetRequestFor(BaseRequestParameter.RequestForEnum.GetStatus);
            return DoGetStatus(requestParameter);
        }

        /// <summary>
        ///     Posts the request.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        public abstract ResponseBase DoPostRequest(BaseRequestParameter parameter);

        /// <summary>
        ///     Gets the response.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        public abstract ResponseBase DoGetResponse(BaseRequestParameter parameter);

        /// <summary>
        ///     Gets the status.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        public abstract ResponseBase DoGetStatus(BaseRequestParameter parameter);

        /// <summary>
        ///     Removes the task.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        protected abstract void RemoveTask(BaseRequestParameter parameter);

        /// <summary>
        ///     Gets the task.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        protected abstract TaskBase GetTask(BaseRequestParameter parameter);

        /// <summary>
        ///     Stops the specified service URL.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        protected abstract ResponseBase DoStop(BaseRequestParameter parameter);

    }

}