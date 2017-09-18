using Lpp.Scanner.DataMart.Model.Processors.Analysis;
using Lpp.Scanner.DataMart.Model.Processors.Common.Processor.Task;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using pSCANNER.DataMart.Model.processor.Analysis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Test.Lpp.pScanner.DataMart.Model.Processors {

    /// <summary>
    /// </summary>
    [TestClass]
    internal class TestScannerAnalysisModelProcessor {

        /// <summary>
        ///     Initializes the test.
        /// </summary>
        [TestInitialize]
        public void InitializeTest() {
            _processor = new ScannerAnalysisModelProcessor(new MockScannerAnalysisModelProcessor<AsyncTask>(), new ScannerAnalysisModelMetadata());
        }

        /// <summary>
        ///     Tests this instance.
        /// </summary>
        [TestMethod]
        public void TestRequestDocument() {
            string requestId = null;
            string documentId = null;
            Stream contentStream = null;
            _processor.RequestDocument(requestId, documentId, contentStream);
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
            _processor.ResponseDocument(requestId, documentId, out contentStream, maxSize);
        }

        /// <summary>
        ///     The processor
        /// </summary>
        private ScannerAnalysisModelProcessor _processor;
    }
}