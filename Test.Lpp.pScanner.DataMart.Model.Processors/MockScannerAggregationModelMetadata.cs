using System;
using System.Collections.Generic;
using Lpp.Dns.DataMart.Model;
using Lpp.Dns.DataMart.Model.Settings;

namespace Test.Lpp.pScanner.DataMart.Model.Processors {

    internal class MockScannerAggregationModelMetadata : IModelMetadata {
        public IDictionary<string, bool> Capabilities => throw new NotImplementedException();

        public Guid ModelId => throw new NotImplementedException();

        public string ModelName => throw new NotImplementedException();

        public IDictionary<string, string> Properties => throw new NotImplementedException();

        public ICollection<ProcessorSetting> Settings => throw new NotImplementedException();

        public IEnumerable<SQLProvider> SQlProviders => throw new NotImplementedException();

        public string Version => throw new NotImplementedException();

        public MockScannerAggregationModelMetadata() {
        }
    }
}