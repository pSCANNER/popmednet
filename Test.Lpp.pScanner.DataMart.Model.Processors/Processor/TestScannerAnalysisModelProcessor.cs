//using Lpp.Scanner.DataMart.Model.Processors.Analysis;
//using Lpp.Scanner.DataMart.Model.Processors.Common.Processor.Task;
//using NUnit.Framework;
//using pSCANNER.DataMart.Model.processor.Analysis;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Test.Lpp.pScanner.DataMart.Model.Processors.Processor {
//    [TestFixture]
//    internal class TestScannerAnalysisModelProcessor {
//        /// <summary>
//        ///     Initializes the test.
//        /// </summary>
//        [OneTimeSetUp]
//        public void InitializeTest() {
//            _processor = new ScannerAnalysisModelProcessor(new MockScannerAnalysisModelProcessor<AsyncTask>(), new ScannerAnalysisModelMetadata());
//        }

// [OneTimeTearDown] public void TearDown() { _processor = null; }

// ///
// <summary>
//     /// Tests this instance. ///
// </summary>
// [Test] public void TestRequestDocument() { string requestId = null; string documentId = null; Stream contentStream = null;
// _processor.RequestDocument(requestId, documentId, contentStream); }

// ///
// <summary>
//     /// Tests the response document. ///
// </summary>
// [Test] public void TestResponseDocument() { string requestId = null; string documentId = null; int maxSize = 0; _processor.ResponseDocument(requestId,
// documentId, out Stream contentStream, maxSize); }

//        /// <summary>
//        ///     The processor
//        /// </summary>
//        private ScannerAnalysisModelProcessor _processor;
//    }
//}