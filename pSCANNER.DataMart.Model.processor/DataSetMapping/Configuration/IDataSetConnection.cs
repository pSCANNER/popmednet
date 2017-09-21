namespace pSCANNER.DataMart.Model.processor.DataSetMapping.Configuration {

    /// <summary>
    /// </summary>
    public interface IDataSetConnection {

        /// <summary>
        ///     Gets or sets the type of the connection.
        /// </summary>
        /// <value>The type of the connection.</value>
        string ConnectionType { get; set; }

        /// <summary>
        ///     Gets or sets the type of the data.
        /// </summary>
        /// <value>The type of the data.</value>
        string DataType { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; set; }

        /// <summary>
        ///     Gets or sets the URI.
        /// </summary>
        /// <value>The URI.</value>
        string Uri { get; set; }

        /// <summary>
        ///     Gets the version.
        /// </summary>
        /// <value>The version.</value>
        string Version { get; }
    }
}