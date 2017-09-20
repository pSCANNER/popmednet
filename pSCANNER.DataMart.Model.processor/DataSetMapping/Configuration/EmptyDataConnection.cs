using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pSCANNER.DataMart.Model.processor.DataSetMapping.Configuration {

    public class EmptyDataConnection : IDataSetConnection {
        public string ConnectionType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string DataType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Uri { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string Version => throw new NotImplementedException();
    }
}