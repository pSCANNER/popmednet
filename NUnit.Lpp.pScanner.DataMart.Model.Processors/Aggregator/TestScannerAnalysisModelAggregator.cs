using Lpp.Dns.DataMart.Model;
using NUnit.Framework;
using NUnit.Lpp.pScanner.DataMart.Model.Processors.Aggregator;
using System.Collections.Generic;

namespace Test.Lpp.pScanner.DataMart.Model.Processors.Aggregator {

    [TestFixture]
    public class TestScannerAnalysisModelAggregator : TestScannerAnalysisModelAggregatorBase {

        [Test, Order(1)]
        public void Aggregator_RequestDocument_Test() {
            _aggregator.Request(RequestId, _network, _requestMetadata, _requestAnalyticDocuments, out IDictionary<string, string> requestProperties, out Document[] desiredDocuments);

            _processor.RequestDocument(RequestId, DataSetId, _dataSetStream);

            _processor.RequestDocument(RequestId, ParameterDocId, _parametersSteam);

            _processor.RequestDocument(RequestId, PmmlDocumentId, _pmmlStream);
        }

        [Test, Order(2)]
        public void Aggregator_Start_Test() {
            //UCLA_LogReg_CSV
            _aggregator.Start(RequestId);
        }
    }
}