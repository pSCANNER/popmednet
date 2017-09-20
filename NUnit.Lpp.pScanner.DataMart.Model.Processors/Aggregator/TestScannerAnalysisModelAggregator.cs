using Lpp.Scanner.DataMart.Model.Processors.Aggregation;
using Lpp.Scanner.DataMart.Model.Processors.Aggregation.Common;
using Lpp.Scanner.DataMart.Model.Processors.Common.Processor.Task;
using NUnit.Framework;
using pSCANNER.DataMart.Model.processor.Aggregation;
using System.IO;

namespace Test.Lpp.pScanner.DataMart.Model.Processors.Aggregator {

    //[TestFixture]
    public class TestScannerAnalysisModelAggregator {

        /// <summary>
        ///     Initializes the test.
        /// </summary>
        [OneTimeSetUp]
        public void InitializeTest() {
            _aggregator = new ScannerAnalysisModelAggregator(new PScannerAggregatorProxy<AsyncTask>(), new ScannerAggregationModelMetadata());
        }

        [OneTimeTearDown]
        public void TearDown() {
            _aggregator = null;
        }

        /// <summary>
        ///     Tests the request document.
        /// </summary>
        [Test]
        public void TestRequestDocument() {
            string requestId = null;
            string documentId = null;
            Stream contentStream = null;
            _aggregator.RequestDocument(requestId, documentId, contentStream);
        }

        /// <summary>
        ///     Tests the response document.
        /// </summary>
        [Test]
        public void TestResponseDocument() {
            string requestId = null;
            string documentId = null;
            int maxSize = 0;
            _aggregator.ResponseDocument(requestId, documentId, out Stream contentStream, maxSize);
        }

        /// <summary>
        ///     The aggregator
        /// </summary>
        private ScannerAnalysisModelAggregator _aggregator;
    }
}