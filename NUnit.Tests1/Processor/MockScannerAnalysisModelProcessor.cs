using Lpp.Scanner.DataMart.Model.Processors.Common.Base;
using Lpp.Scanner.DataMart.Model.Processors.Common.Processor.Task;

namespace Test.Lpp.pScanner.DataMart.Model.Processors {

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="Lpp.Scanner.DataMart.Model.Processors.Common.Base.ProxyBase"/>
    internal class MockScannerAnalysisModelProcessor<T> : ProxyBase {

        /// <summary>
        ///     Gets the response.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override ResponseBase DoGetResponse(BaseRequestParameter parameter) {
            throw new System.NotImplementedException();
        }

        /// <summary>
        ///     Gets the status.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override ResponseBase DoGetStatus(BaseRequestParameter parameter) {
            throw new System.NotImplementedException();
        }

        /// <summary>
        ///     Posts the request.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override ResponseBase DoPostRequest(BaseRequestParameter parameter) {
            throw new System.NotImplementedException();
        }

        /// <summary>
        ///     Stops the specified service URL.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        protected override ResponseBase DoStop(BaseRequestParameter parameter) {
            throw new System.NotImplementedException();
        }

        /// <summary>
        ///     Gets the task.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        protected override TaskBase GetTask(BaseRequestParameter parameter) {
            throw new System.NotImplementedException();
        }

        /// <summary>
        ///     Removes the task.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        protected override void RemoveTask(BaseRequestParameter parameter) {
            throw new System.NotImplementedException();
        }
    }
}