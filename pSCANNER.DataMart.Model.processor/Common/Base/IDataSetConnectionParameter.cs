using pSCANNER.DataMart.Model.processor.DataSetMapping.Configuration;

namespace Lpp.Scanner.DataMart.Model.Processors.Common.Base {

    public interface IDataSetConnectionParameter {
        IDataSetConnection DataSetConnection { get; set; }
    }
}