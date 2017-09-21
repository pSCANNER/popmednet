using System;
using System.Collections.Generic;

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
        ///     Gets the specified data set name.
        /// </summary>
        /// <param name="dataSetName">Name of the data set.</param>
        /// <returns></returns>
        internal static IDataSetConnection Get(string dataSetName) {
            if (_dataSourceLookup.ContainsKey(dataSetName) == false) {
                throw new DataSetConnectionDoesNotExist(dataSetName);
            }
            return _dataSourceLookup[dataSetName];
        }

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

        /// <summary>
        ///     Initializes the <see cref="BaseDataSetConnection"/> class.
        /// </summary>
        static DataSetConnection() {
            _dataSourceLookup = new Dictionary<String, IDataSetConnection> {
                {
                    "UCLA_LogReg_CSV",
                    new DataSetConnection() {
                        Name = "Example",
                        ConnectionType = "HTTP",
                        DataType = "CSV",
                        Uri = "https://stats.idre.ucla.edu/stat/data/binary.csv",
                        Version = "1.0"
                    }
                },
                {
                    "Test",
                   new DataSetConnection() {
                        Name = "Test",
                        ConnectionType = "Database",
                        DataType = "MSSQL",
                        Uri = "Server=DAX-PC;Database=pScannerDataSources;User Id=myUsername;Password=myPassword;",
                        Version = "1.0"
                    }
                },
                {
                    "MTE",
                   new DataSetConnection() {
                        Name = "MTE",
                        ConnectionType = "Database",
                        DataType = "MSSQL",
                        Uri = "Server=NLDMIWUA51323FX\\DEVELOPMENT;Database=pScannerDataSources;Trusted_Connection=True;",
                        Version = "1.0"
                    }
                }
            };
        }

        /// <summary>
        ///     The data source lookup
        /// </summary>
        private static IDictionary<String, IDataSetConnection> _dataSourceLookup;

        /// <summary>
        /// </summary>
        /// <seealso cref="System.ArgumentException"/>
        public class DataSetConnectionDoesNotExist : ArgumentException {

            /// <summary>
            ///     Initializes a new instance of the <see cref="DataSetConnectionDoesNotExist"/> class.
            /// </summary>
            /// <param name="message">The error message that explains the reason for the exception.</param>
            public DataSetConnectionDoesNotExist(string message) : base(message) {
            }
        }
    }
}