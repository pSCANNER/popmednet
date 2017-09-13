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

using System.Threading;
using Lpp.Scanner.DataMart.Model.Processors.Common.Base;
using Lpp.Scanner.DataMart.Model.Processors.Common.Models;
using Lpp.Scanner.DataMart.Model.Processors.Common.Parameters.Response;
using Lpp.Scanner.DataMart.Model.Processors.Common.Processor.Adapers.Base;


#endregion

namespace Lpp.Scanner.DataMart.Model.Processors.Common.Processor.Adapers.Mock {

    public class MockAdapter : BaseAdapter {
        #region Fields

        private readonly int _delayInSeconds;
        private readonly int _millisecondsTimeout = 1000;
        private readonly string _outputResourceLocation;
        private bool _continue = true;
        private RResponse _postRequest;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="MockAdapter" /> class.
        /// </summary>
        public MockAdapter(string outputResourceLocation) : this(5, outputResourceLocation) {}

        /// <summary>
        ///     Initializes a new instance of the <see cref="MockAdapter" /> class.
        /// </summary>
        /// <param name="delayInSeconds">The delay in seconds.</param>
        /// <param name="outputResourceLocation">The output resource location.</param>
        public MockAdapter(int delayInSeconds, string outputResourceLocation) {
            _outputResourceLocation = outputResourceLocation;
            _delayInSeconds = delayInSeconds;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MockAdapter" /> class.
        /// </summary>
        /// <param name="outputResourceLocation"></param>
        /// <param name="delayInSeconds">The delay in seconds.</param>
        /// <param name="multiplier">The multiplier.</param>
        public MockAdapter(string outputResourceLocation, int delayInSeconds, int multiplier) : this(delayInSeconds, outputResourceLocation) {
            _millisecondsTimeout = multiplier;
        }

        #endregion

        /// <summary>
        ///     Does the post request.
        /// </summary>
        /// <param name="requestParameter">The request parameter.</param>
        /// <returns></returns>
        public override ResponseBase PostRequest(BaseRequestParameter requestParameter) {
            _postRequest = new RResponse {Status = Constants.ResponseStatus.InProgress};

            var timeout = (_delayInSeconds*_millisecondsTimeout);
            for (var index = 0; index < _delayInSeconds; index++) {
                if (_continue == false) {
                    break;
                }
                Thread.Sleep(timeout);
            }
            _postRequest.Status = Constants.ResponseStatus.Complete;
            _postRequest.Result = Helper.ReadInText(_outputResourceLocation);
            return _postRequest;
        }

        /// <summary>
        ///     Does the stop request.
        /// </summary>
        /// <param name="requestParameter">The request parameter.</param>
        /// <returns></returns>
        protected override ResponseBase StopRequest(BaseRequestParameter requestParameter) {
            _continue = false;
            return new RResponse {Status = Constants.ResponseStatus.Canceled};
        }

        /// <summary>
        ///     Does the get status.
        /// </summary>
        /// <param name="requestParameter">The request parameter.</param>
        /// <returns></returns>
        protected override ResponseBase GetStatus(BaseRequestParameter requestParameter) {
            return new RResponse {Status = _continue ? Constants.ResponseStatus.InProgress : Constants.ResponseStatus.Complete};
        }

        /// <summary>
        ///     Does the get response.
        /// </summary>
        /// <param name="requestParameter">The request parameter.</param>
        /// <returns></returns>
        protected override ResponseBase GetResponse(BaseRequestParameter requestParameter) {
            return new RResponse {Status = Constants.ResponseStatus.Complete, Result = Helper.ReadInText(Constants.Aggregator.Output.FinalResponse)};
        }

    }

}