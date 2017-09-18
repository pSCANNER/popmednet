using Lpp.Scanner.DataMart.Model.Processors.Analysis;
using Lpp.Scanner.DataMart.Model.Processors.Common.Processor.Task;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using pSCANNER.DataMart.Model.processor.Analysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test.Lpp.pScanner.DataMart.Model.Processors {

    [TestClass]
    internal class TestScannerAnalysisModelProcessor {

        [TestInitialize]
        public void InitializeTest() {
            _processor = new ScannerAnalysisModelProcessor(new MockScannerAnalysisModelProcessor<AsyncTask>(), new ScannerAnalysisModelMetadata());
        }

        [TestMethod]
        public void Test() {
        }

        private ScannerAnalysisModelProcessor _processor;
    }
}