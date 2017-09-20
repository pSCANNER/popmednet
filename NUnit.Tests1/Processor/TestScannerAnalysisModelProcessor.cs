using Lpp.Dns.DataMart.Model;
using Moq;
using NUnit.Framework;
using NUnit.Lpp.pScanner.DataMart.Model.Processors.Processor;
using System.Collections.Generic;
using System.IO;

namespace Test.Lpp.pScanner.DataMart.Model.Processors.Processor {

    /// <summary>
    /// </summary>
    [TestFixture]
    internal class TestScannerAnalysisModelProcessor : TestScannerAnalysisModelProcessorBase {

        [Test]
        public void RequestDocument_Test() {
            _processor.Request(RequestId, _network, _requestMetadata, _requestAnalyticDocuments, out IDictionary<string, string> requestProperties, out Document[] desiredDocuments);

            string requestId = RequestId;
            string documentId = "1";
            //Common\scanner\test_files
            //File.OpenRead()
            Stream _dataSetStream = new MemoryStream();
            _processor.RequestDocument(requestId, documentId, _dataSetStream);

            Stream _pmmlStream = new MemoryStream();
            _processor.RequestDocument(requestId, documentId, _pmmlStream);

            Stream _parametersSteam = new MemoryStream();
            _processor.RequestDocument(requestId, documentId, _parametersSteam);
        }
    }
}