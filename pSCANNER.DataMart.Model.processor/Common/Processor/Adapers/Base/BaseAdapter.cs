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

using System;
using Lpp.Scanner.DataMart.Model.Processors.Common.Base;
using Lpp.Scanner.DataMart.Model.Processors.Common.Processor.Task;


#endregion

namespace Lpp.Scanner.DataMart.Model.Processors.Common.Processor.Adapers.Base {

    public abstract class BaseAdapter {
        #region Properties

        /// <summary>
        ///     Gets the task.
        /// </summary>
        /// <value>
        ///     The task.
        /// </value>
        protected TaskBase Task { get; private set; }

        #endregion

        /// <summary>
        ///     Requests the specified request URI string.
        /// </summary>
        /// <param name="requestParameter">The request parameter.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentOutOfRangeException"></exception>
        /// <exception cref="System.NotImplementedException"></exception>
        public ResponseBase PerformRequest(BaseRequestParameter requestParameter) {

            ResponseBase retVal = null;

            switch (requestParameter.RequestFor) {

                case BaseRequestParameter.RequestForEnum.PostRequest: {
                    retVal = PostRequest(requestParameter);
                    break;
                }
                case BaseRequestParameter.RequestForEnum.GetResponse: {
                    retVal = GetResponse(requestParameter);
                    break;
                }
                case BaseRequestParameter.RequestForEnum.GetStatus: {
                    retVal = GetStatus(requestParameter);
                    break;
                }
                case BaseRequestParameter.RequestForEnum.StopRequest: {
                    retVal = StopRequest(requestParameter);
                    break;
                }
                case BaseRequestParameter.RequestForEnum.RequestId:
                case BaseRequestParameter.RequestForEnum.RequestXml:
                case BaseRequestParameter.RequestForEnum.Undefined:
                    break;
                default: {
                    throw new ArgumentOutOfRangeException();
                }
            }

            return retVal;
        }

        /// <summary>
        ///     Does the post request.
        /// </summary>
        /// <param name="requestParameter">The request parameter.</param>
        /// <returns></returns>
        public abstract ResponseBase PostRequest(BaseRequestParameter requestParameter);

        /// <summary>
        ///     Does the stop request.
        /// </summary>
        /// <param name="requestParameter">The request parameter.</param>
        /// <returns></returns>
        protected abstract ResponseBase StopRequest(BaseRequestParameter requestParameter);

        /// <summary>
        ///     Does the get status.
        /// </summary>
        /// <param name="requestParameter">The request parameter.</param>
        /// <returns></returns>
        protected abstract ResponseBase GetStatus(BaseRequestParameter requestParameter);

        /// <summary>
        ///     Does the get response.
        /// </summary>
        /// <param name="requestParameter">The request parameter.</param>
        /// <returns></returns>
        protected abstract ResponseBase GetResponse(BaseRequestParameter requestParameter);

    }

}