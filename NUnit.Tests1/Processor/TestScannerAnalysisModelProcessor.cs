using Lpp.Dns.DataMart.Model;
using Moq;
using NUnit.Framework;
using NUnit.Lpp.pScanner.DataMart.Model.Processors.Processor;
using System;
using System.Collections.Generic;
using System.IO;

namespace Test.Lpp.pScanner.DataMart.Model.Processors.Processor {

    /// <summary>
    /// </summary>
    [TestFixture]
    internal class TestScannerAnalysisModelProcessor : TestScannerAnalysisModelProcessorBase {

        /// <summary>
        ///     Requests the documents empty test.
        /// </summary>
        //[Test]
        public void Request_Documents_Empty_Test() {
            Assert.That(() => _processor.Request(RequestId, _network, _requestMetadata, null, out IDictionary<string, string> requestProperties, out Document[] desiredDocuments), Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("Document[]"));
            Assert.That(() => _processor.Request(RequestId, _network, _requestMetadata, new Document[] { }, out IDictionary<string, string> requestProperties, out Document[] desiredDocuments), Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("Document[]"));
        }

        /// <summary>
        ///     Requests the network connection metadata empty test.
        /// </summary>
        //[Test]
        public void Request_NetworkConnectionMetadata_Empty_Test() {
            Assert.That(() => _processor.Request(RequestId, null, _requestMetadata, _requestDocuments, out IDictionary<string, string> requestProperties, out Document[] desiredDocuments), Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("NetworkConnectionMetadata"));
        }

        /// <summary>
        ///     Requests the request identifier empty test.
        /// </summary>
        //[Test]
        public void Request_RequestId_Empty_Test() {
            Assert.That(() => _processor.Request(null, _network, _requestMetadata, _requestDocuments, out IDictionary<string, string> requestProperties, out Document[] desiredDocuments), Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("RequestId"));
            Assert.That(() => _processor.Request(string.Empty, _network, _requestMetadata, _requestDocuments, out IDictionary<string, string> requestProperties, out Document[] desiredDocuments), Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("RequestId"));
            Assert.That(() => _processor.Request("", _network, _requestMetadata, _requestDocuments, out IDictionary<string, string> requestProperties, out Document[] desiredDocuments), Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("RequestId"));
        }

        /// <summary>
        ///     Requests the request metadata empty test.
        /// </summary>
        //[Test]
        public void Request_RequestMetadata_Empty_Test() {
            Assert.That(() => _processor.Request(RequestId, _network, null, _requestDocuments, out IDictionary<string, string> requestProperties, out Document[] desiredDocuments), Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("RequestMetadata"));
        }

        /// <summary>
        ///     Requests the test.
        /// </summary>
        //[Test]
        public void Request_Test() {
            _processor.Request(RequestId, _network, _requestMetadata, _requestDocuments, out IDictionary<string, string> requestProperties, out Document[] desiredDocuments);
        }

        [Test]
        public void RequestDocument_Test() {
            _processor.Request(RequestId, _network, _requestMetadata, _requestDocuments, out IDictionary<string, string> requestProperties, out Document[] desiredDocuments);

            string requestId = RequestId;
            string documentId = "1";
            var contentStream = new Mock<Stream>();
            _processor.RequestDocument(requestId, documentId, contentStream.Object);
        }
    }
}