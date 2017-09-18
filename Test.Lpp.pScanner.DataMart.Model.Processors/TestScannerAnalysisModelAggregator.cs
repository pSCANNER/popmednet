using Lpp.Scanner.DataMart.Model.Processors.Aggregation;
using Lpp.Scanner.DataMart.Model.Processors.Common.Processor.Task;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using pSCANNER.DataMart.Model.processor.Aggregation;

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
        ///     Tests this instance.
        /// </summary>
        [TestMethod]
        public void Test() {
        }

        /// <summary>
        ///     The aggregator
        /// </summary>
        private ScannerAnalysisModelAggregator _aggregator;
    }
}