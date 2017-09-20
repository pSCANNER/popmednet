namespace pSCANNER.DataMart.Model.processor.DataSetMapping.Configuration {
    public interface IDataSetConnection {
        string ConnectionType { get; set; }
        string DataType { get; set; }
        string Name { get; set; }
        string Uri { get; set; }
        string Version { get; }
    }
}