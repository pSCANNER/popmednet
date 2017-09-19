using Lpp.Dns.DataMart.Model;
using Lpp.Scanner.DataMart.Model.Processors.Analysis;
using Lpp.Scanner.DataMart.Model.Processors.Common.Processor.Task;
using Moq;
using NUnit.Framework;
using pSCANNER.DataMart.Model.processor.Analysis;
using System;
using System.IO;
using Test.Lpp.pScanner.DataMart.Model.Processors;

namespace NUnit.Lpp.pScanner.DataMart.Model.Processors.Processor {

    internal class TestScannerAnalysisModelProcessorBase {

        /// <summary>
        ///     Initializes the test.
        /// </summary>
        [OneTimeSetUp]
        public void InitializeTest() {
            _processor = new ScannerAnalysisModelProcessor(new MockScannerAnalysisModelProcessor<AsyncTask>(), new ScannerAnalysisModelMetadata());
            _network = new NetworkConnectionMetadata {
                OrganizationName = "",
                UserEmail = "",
                UserFullName = "",
                UserLogin = ""
            };
            _requestMetadata = new RequestMetadata {
                RequestTypeId = Guid.Empty.ToString()
            };
            _requestDocuments = new Document[] {
                new Document("1", "json", "file1"),
                new Document("2", "json", "file2"),
                new Document("3", "json", "file3")
            };
        }

        //[Test]
        /// <summary>
        ///     Requests the document content stream empty test.
        /// </summary>
        public void RequestDocument_ContentStream_Empty_Test() {
            Assert.That(() => _processor.RequestDocument(It.IsAny<String>(), It.IsAny<String>(), null), Throws.Exception.TypeOf<ArgumentException>().With.Property("ParamName").EqualTo("contentStream"));
        }

        //[Test]
        /// <summary>
        ///     Requests the document document identifier empty test.
        /// </summary>
        public void RequestDocument_DocumentId_Empty_Test() {
            Assert.That(() => _processor.RequestDocument(It.IsAny<String>(), null, It.IsAny<Stream>()), Throws.Exception.TypeOf<ArgumentException>().With.Property("ParamName").EqualTo("documentId"));
            Assert.That(() => _processor.RequestDocument(It.IsAny<String>(), string.Empty, It.IsAny<Stream>()), Throws.Exception.TypeOf<ArgumentException>().With.Property("ParamName").EqualTo("documentId"));
            Assert.That(() => _processor.RequestDocument(It.IsAny<String>(), "", It.IsAny<Stream>()), Throws.Exception.TypeOf<ArgumentException>().With.Property("ParamName").EqualTo("documentId"));
        }

        //[Test]
        /// <summary>
        ///     Requests the document request identifier empty test.
        /// </summary>
        public void RequestDocument_RequestId_Empty_Test() {
            Assert.That(() => _processor.RequestDocument(null, It.IsAny<String>(), It.IsAny<Stream>()), Throws.Exception.TypeOf<ArgumentException>().With.Property("ParamName").EqualTo("requestId"));
            Assert.That(() => _processor.RequestDocument(string.Empty, It.IsAny<String>(), It.IsAny<Stream>()), Throws.Exception.TypeOf<ArgumentException>().With.Property("ParamName").EqualTo("requestId"));
            Assert.That(() => _processor.RequestDocument("", It.IsAny<String>(), It.IsAny<Stream>()), Throws.Exception.TypeOf<ArgumentException>().With.Property("ParamName").EqualTo("requestId"));
        }

        /// <summary>
        ///     Tests this instance.
        /// </summary>
        //[Test]
        public void RequestDocument_Test() {
            string requestId = It.IsAny<String>();
            string documentId = It.IsAny<String>();
            var contentStream = new Mock<Stream>();
            _processor.RequestDocument(requestId, documentId, contentStream.Object);
        }

        /// <summary>
        ///     Tests the response document.
        /// </summary>
        //[Test]
        public void ResponseDocument_Test() {
            string requestId = It.IsAny<String>();
            string documentId = It.IsAny<String>();
            int maxSize = It.IsAny<int>();
            _processor.ResponseDocument(requestId, documentId, out Stream contentStream, maxSize);
        }

        /// <summary>
        ///     Tears down.
        /// </summary>
        [OneTimeTearDown]
        public void TearDown() {
            _processor = null;
        }

        /// <summary>
        ///     The network
        /// </summary>
        protected NetworkConnectionMetadata _network;

        /// <summary>
        ///     The processor
        /// </summary>
        protected ScannerAnalysisModelProcessor _processor;

        /// <summary>
        ///     The request documents
        /// </summary>
        protected Document[] _requestDocuments;

        /// <summary>
        ///     The request metadata
        /// </summary>
        protected RequestMetadata _requestMetadata;
    }
}