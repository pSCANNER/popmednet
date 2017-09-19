using Lpp.Dns.DataMart.Model;
using Moq;
using NUnit.Framework;
using NUnit.Lpp.pScanner.DataMart.Model.Processors.Processor;
using System;
using System.Collections.Generic;

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
            Assert.That(() => _processor.Request("1", _network, _requestMetadata, null, out IDictionary<string, string> requestProperties, out Document[] desiredDocuments), Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("Document[]"));
            Assert.That(() => _processor.Request("1", _network, _requestMetadata, new Document[] { }, out IDictionary<string, string> requestProperties, out Document[] desiredDocuments), Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("Document[]"));
        }

        /// <summary>
        ///     Requests the network connection metadata empty test.
        /// </summary>
        //[Test]
        public void Request_NetworkConnectionMetadata_Empty_Test() {
            Assert.That(() => _processor.Request("1", null, _requestMetadata, _requestDocuments, out IDictionary<string, string> requestProperties, out Document[] desiredDocuments), Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("NetworkConnectionMetadata"));
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
            Assert.That(() => _processor.Request("1", _network, null, _requestDocuments, out IDictionary<string, string> requestProperties, out Document[] desiredDocuments), Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("RequestMetadata"));
        }

        /// <summary>
        ///     Requests the test.
        /// </summary>
        //[Test]
        public void Request_Test() {
            string requestId = It.IsAny<String>();
            _processor.Request("1", _network, _requestMetadata, _requestDocuments, out IDictionary<string, string> requestProperties, out Document[] desiredDocuments);
        }
    }
}