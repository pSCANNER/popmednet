using Lpp.Dns.DataMart.Model;
using Moq;
using NUnit.Framework;
using NUnit.Lpp.pScanner.DataMart.Model.Processors.Processor;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Test.Lpp.pScanner.DataMart.Model.Processors.Processor {

    /// <summary>
    /// </summary>
    [TestFixture]
    internal class TestScannerAnalysisModelProcessor : TestScannerAnalysisModelProcessorBase {

        [Test, Order(1)]
        public void RequestDocument_Test() {
            _processor.Request(RequestId, _network, _requestMetadata, _requestAnalyticDocuments, out IDictionary<string, string> requestProperties, out Document[] desiredDocuments);

            _processor.RequestDocument(RequestId, DataSetId, _dataSetStream);

            _processor.RequestDocument(RequestId, ParameterDocId, _parametersSteam);

            _processor.RequestDocument(RequestId, PmmlDocumentId, _pmmlStream);
        }

        [Test, Order(2)]
        public void Start_Test() {
            //UCLA_LogReg_CSV
            _processor.Start(RequestId);
        }
    }
}