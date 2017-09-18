using Lpp.Scanner.DataMart.Model.Processors.Aggregation;
using Lpp.Scanner.DataMart.Model.Processors.Common.Processor.Task;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using pSCANNER.DataMart.Model.processor.Aggregation;

namespace Test.Lpp.pScanner.DataMart.Model.Processors {

    [TestClass]
    public class TestScannerAnalysisModelAggregator {

        [TestInitialize]
        public void InitializeTest() {
            _aggregator = new ScannerAnalysisModelAggregator(new MockPScannerAggregatorProxy<AsyncTask>(), new ScannerAggregationModelMetadata());
        }

        [TestMethod]
        public void Test() {
        }

        private ScannerAnalysisModelAggregator _aggregator;
    }
}