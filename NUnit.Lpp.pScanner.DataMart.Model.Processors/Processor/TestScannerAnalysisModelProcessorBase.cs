using Lpp.Dns.DataMart.Model;
using Lpp.Scanner.DataMart.Model.Processors.Analysis;
using Lpp.Scanner.DataMart.Model.Processors.Common.Processor.Task;
using Moq;
using NUnit.Framework;
using pSCANNER.DataMart.Model.processor.Analysis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Test.Lpp.pScanner.DataMart.Model.Processors;

namespace NUnit.Lpp.pScanner.DataMart.Model.Processors.Processor {

    /// <summary>
    /// </summary>
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
            _requestAnalyticDocuments = new Document[] {
                new Document(DataSetId, "json", "dataset.json"),
                new Document(ParameterDocId, "json", "parameters.json"),
                new Document(PmmlDocumentId, "json", "pmml.json")
            };

            var assembly = Assembly.GetExecutingAssembly();
            string assembly_name = assembly.GetName().Name;
            _dataSetResourceName = assembly_name + "." + TestFileFolder + "." + "test_dataset.json";
            _pmmResourceName = assembly_name + "." + TestFileFolder + "." + "test_pmml.json";
            _parameterResourceName = assembly_name + "." + TestFileFolder + "." + "test_parameters.json";

            _dataSetStream = assembly.GetManifestResourceStream(_dataSetResourceName);
            _pmmlStream = assembly.GetManifestResourceStream(_pmmResourceName);
            _parametersSteam = assembly.GetManifestResourceStream(_parameterResourceName);
        }

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
            Assert.That(() => _processor.Request(RequestId, null, _requestMetadata, _requestAnalyticDocuments, out IDictionary<string, string> requestProperties, out Document[] desiredDocuments), Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("NetworkConnectionMetadata"));
        }

        /// <summary>
        ///     Requests the request identifier empty test.
        /// </summary>
        //[Test]
        public void Request_RequestId_Empty_Test() {
            Assert.That(() => _processor.Request(null, _network, _requestMetadata, _requestAnalyticDocuments, out IDictionary<string, string> requestProperties, out Document[] desiredDocuments), Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("RequestId"));
            Assert.That(() => _processor.Request(string.Empty, _network, _requestMetadata, _requestAnalyticDocuments, out IDictionary<string, string> requestProperties, out Document[] desiredDocuments), Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("RequestId"));
            Assert.That(() => _processor.Request("", _network, _requestMetadata, _requestAnalyticDocuments, out IDictionary<string, string> requestProperties, out Document[] desiredDocuments), Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("RequestId"));
        }

        /// <summary>
        ///     Requests the request metadata empty test.
        /// </summary>
        //[Test]
        public void Request_RequestMetadata_Empty_Test() {
            Assert.That(() => _processor.Request(RequestId, _network, null, _requestAnalyticDocuments, out IDictionary<string, string> requestProperties, out Document[] desiredDocuments), Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("RequestMetadata"));
        }

        /// <summary>
        ///     Requests the test.
        /// </summary>
        //[Test]
        public void Request_Test() {
            _processor.Request(RequestId, _network, _requestMetadata, _requestAnalyticDocuments, out IDictionary<string, string> requestProperties, out Document[] desiredDocuments);
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

        protected const string DataSetId = "1";
        protected const string ParameterDocId = "2";
        protected const string PmmlDocumentId = "3";

        /// <summary>
        ///     The request identifier
        /// </summary>
        protected const string RequestId = "1";

        /// <summary>
        ///     The data set resource name
        /// </summary>
        protected string _dataSetResourceName;

        /// <summary>
        ///     The data set stream
        /// </summary>
        protected Stream _dataSetStream;

        /// <summary>
        ///     The network
        /// </summary>
        protected NetworkConnectionMetadata _network;

        /// <summary>
        ///     The parameter resource name
        /// </summary>
        protected string _parameterResourceName;

        /// <summary>
        ///     The parameters steam
        /// </summary>
        protected Stream _parametersSteam;

        /// <summary>
        ///     The PMML stream
        /// </summary>
        protected Stream _pmmlStream;

        /// <summary>
        ///     The PMM resource name
        /// </summary>
        protected string _pmmResourceName;

        /// <summary>
        ///     The processor
        /// </summary>
        protected ScannerAnalysisModelProcessor _processor;

        /// <summary>
        ///     The request documents
        /// </summary>
        protected Document[] _requestAnalyticDocuments;

        /// <summary>
        ///     The request metadata
        /// </summary>
        protected RequestMetadata _requestMetadata;

        private const string TestFileFolder = "Common.scanner.test_files";
    }
}