using Lpp.Scanner.DataMart.Model.Processors.DataSetMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pSCANNER.DataMart.Model.processor.DataSetMapping.Configuration;

namespace pSCANNER.DataMart.Model.processor.DataSetMapping {

    internal class PSCANNERRegistry : IRegistry {

        public Dictionary<string, IDataSetConnection> GetDataSetConnectionElementMap() => throw new NotImplementedException();

        public string getRSection() => throw new NotImplementedException();
    }
}