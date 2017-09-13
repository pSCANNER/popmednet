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
using Lpp.Scanner.DataMart.Model.Processors.Common.Base;


#endregion

namespace Lpp.Scanner.DataMart.Model.Processors.Common.Parameters {

    public class WebServiceRequestParameter : BaseAdapterRequestParameter {

        private const string item = "item";
        private static readonly Regex _re = new Regex(string.Format("(?:(?<{0}>{1}|{2}|{3}|{4})\\?)", item, RequestForEnum.GetResponse, RequestForEnum.GetStatus, RequestForEnum.PostRequest, RequestForEnum.StopRequest), RegexOptions.Compiled | RegexOptions.IgnoreCase);

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="WebServiceRequestParameter" /> class.
        /// </summary>
        /// <param name="requestUrl">The request URL.</param>
        /// <param name="requestId">The request identifier.</param>
        public WebServiceRequestParameter(string requestUrl, string requestId) : base(requestId, ConvertRequestTypeToEnum(_re.Match(requestUrl).Groups[item].Value)) {
            RequestUrl = requestUrl;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the request URL.
        /// </summary>
        /// <value>
        ///     The request URL.
        /// </value>
        public string RequestUrl { get; private set; }

        #endregion

        /// <summary>
        ///     Converts the request type to enum.
        /// </summary>
        /// <param name="danType">Type of the dan.</param>
        /// <returns></returns>
        public static RequestForEnum ConvertRequestTypeToEnum(string danType) {
            var retVal = RequestForEnum.Undefined;
            danType = danType.ToUpper();
            switch (danType) {
                case "POSTREQUEST": {
                    retVal = RequestForEnum.PostRequest;
                    break;
                }
                case "REQUESTID": {
                    retVal = RequestForEnum.RequestId;
                    break;
                }
                case "REQUESTXML": {
                    retVal = RequestForEnum.RequestXml;
                    break;
                }
                case "GETRESPONSE": {
                    retVal = RequestForEnum.GetResponse;
                    break;
                }
                case "GETSTATUS": {
                    retVal = RequestForEnum.GetStatus;
                    break;
                }
                case "STOPREQUEST": {
                    retVal = RequestForEnum.StopRequest;
                    break;
                }

            }
            return retVal;
        }

    }

}