using Lpp.Scanner.DataMart.Model.Processors.Aggregation;
using Lpp.Scanner.DataMart.Model.Processors.Common.Processor.Task;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using pSCANNER.DataMart.Model.processor.Aggregation;
using System.IO;

namespace Test.Lpp.pScanner.DataMart.Model.Processors {

    /// <summary>
    /// </summary>
    [TestClass]
    public class TestScannerAnalysisModelAggregator {

        /// <summary>
        ///     Initializes the test.
        /// </summary>
        [TestInitialize]
        public void InitializeTest() {
            _aggregator = new ScannerAnalysisModelAggregator(new MockPScannerAggregatorProxy<AsyncTask>(), new ScannerAggregationModelMetadata());
        }

        /// <summary>
        ///     Tests the request document.
        /// </summary>
        [TestMethod]
        public void TestRequestDocument() {
            string requestId = null;
            string documentId = null;
            Stream contentStream = null;
            _aggregator.RequestDocument(requestId, documentId, contentStream);
        }

        /// <summary>
        ///     Tests the response document.
        /// </summary>
        [TestMethod]
        public void TestResponseDocument() {
            string requestId = null;
            string documentId = null;
            Stream contentStream = null;
            int maxSize = 0;
            _aggregator.ResponseDocument(requestId, documentId, out contentStream, maxSize);
        }

        /// <summary>
        ///     The aggregator
        /// </summary>
        private ScannerAnalysisModelAggregator _aggregator;
    }
}