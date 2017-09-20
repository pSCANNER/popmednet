using System;

//<DataSetConnectionSection>
//    <DataSetConnections>
//      <add Name="Example" ConnectionType="HTTP" DataType="CSV" Uri="http://www.ats.ucla.edu/stat/data/binary.csv" />
//      <add Name="Test" ConnectionType="Database" DataType="MSSQL" Uri="Server=DAX-PC;Database=pScannerDataSources;User Id=myUsername;Password=myPassword;" Version="1.0" />
//      <add Name="MTE" ConnectionType="Database" DataType="MSSQL" Uri="Server=NLDMIWUA51323FX\\DEVELOPMENT;Database=pScannerDataSources;Trusted_Connection=True;" Version="1.0"/>
//    </DataSetConnections>
//  </DataSetConnectionSection>

namespace pSCANNER.DataMart.Model.processor.DataSetMapping.Configuration {

    /// <summary>
    /// </summary>
    public class DataSetConnection : IDataSetConnection {

        /// <summary>
        ///     Gets or sets the type of the connection.
        /// </summary>
        /// <value>The type of the connection.</value>
        public String ConnectionType { get; set; }

        /// <summary>
        ///     Gets or sets the type of the data.
        /// </summary>
        /// <value>The type of the data.</value>
        public String DataType { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public String Name { get; set; }

        /// <summary>
        ///     Gets or sets the URI.
        /// </summary>
        /// <value>The URI.</value>
        public String Uri { get; set; }

        /// <summary>
        ///     Gets the version.
        /// </summary>
        /// <value>The version.</value>
        public string Version { get; internal set; }
    }
}