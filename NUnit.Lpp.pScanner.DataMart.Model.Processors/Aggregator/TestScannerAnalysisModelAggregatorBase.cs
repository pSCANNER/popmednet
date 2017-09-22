using Lpp.Dns.DataMart.Model;
using Lpp.Scanner.DataMart.Model.Processors.Aggregation;
using Lpp.Scanner.DataMart.Model.Processors.Aggregation.Common;
using Lpp.Scanner.DataMart.Model.Processors.Analysis;
using Lpp.Scanner.DataMart.Model.Processors.Common.Models;
using Lpp.Scanner.DataMart.Model.Processors.Common.Processor.Task;
using NUnit.Framework;
using pSCANNER.DataMart.Model.processor.Aggregation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace NUnit.Lpp.pScanner.DataMart.Model.Processors.Aggregator {

    public class TestScannerAnalysisModelAggregatorBase {

        /// <summary>
        ///     Initializes the test.
        /// </summary>
        [OneTimeSetUp]
        public void InitializeTest() {
            _aggregator = new ScannerAnalysisModelAggregator(new PScannerAggregatorProxy<AsyncTask>(), new ScannerAggregationModelMetadata());
            _aggregator.Settings = new Dictionary<string, object> { { Constants.Aggregator.Input.SettingsEnum.rLocation.ToString(), @"C:\Program Files\R\R-3.3.2\bin\x64\R.exe" } };
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

            _pmmResourceName = assembly_name + "." + TestFileFolder + "." + "test_pmml.json";
            _parameterResourceName = assembly_name + "." + TestFileFolder + "." + "test_parameters.json";

            _pmmlStream = assembly.GetManifestResourceStream(_pmmResourceName);
            _parametersSteam = assembly.GetManifestResourceStream(_parameterResourceName);
        }

        [OneTimeTearDown]
        public void TearDown() {
            _aggregator = null;
        }

        /// <summary>
        ///     Tests the request document.
        /// </summary>
        //[Test]
        public void TestRequestDocument() {
            string requestId = null;
            string documentId = null;
            Stream contentStream = null;
            _aggregator.RequestDocument(requestId, documentId, contentStream);
        }

        /// <summary>
        ///     Tests the response document.
        /// </summary>
       // [Test]
        public void TestResponseDocument() {
            string requestId = null;
            string documentId = null;
            int maxSize = 0;
            _aggregator.ResponseDocument(requestId, documentId, out Stream contentStream, maxSize);
        }

        /// <summary>
        ///     The data set identifier
        /// </summary>
        protected const string DataSetId = "1";

        /// <summary>
        ///     The parameter document identifier
        /// </summary>
        protected const string ParameterDocId = "2";

        /// <summary>
        ///     The PMML document identifier
        /// </summary>
        protected const string PmmlDocumentId = "3";

        /// <summary>
        ///     The request identifier
        /// </summary>
        protected const string RequestId = "1";

        /// <summary>
        ///     The aggregator
        /// </summary>
        protected ScannerAnalysisModelAggregator _aggregator;

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

        /// <summary>
        ///     The test file folder
        /// </summary>
        private const string TestFileFolder = "Common.scanner.aggregation.test_files";
    }
}