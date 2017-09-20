using pSCANNER.DataMart.Model.processor.DataSetMapping.Configuration;
using System;
using System.Collections.Generic;

namespace Lpp.Scanner.DataMart.Model.Processors.DataSetMapping.Configuration {

    //<DataSetConnectionSection>
    //    <DataSetConnections>
    //      <add Name="Example" ConnectionType="HTTP" DataType="CSV" Uri="http://www.ats.ucla.edu/stat/data/binary.csv" />
    //      <add Name="Test" ConnectionType="Database" DataType="MSSQL" Uri="Server=DAX-PC;Database=pScannerDataSources;User Id=myUsername;Password=myPassword;" Version="1.0" />
    //      <add Name="MTE" ConnectionType="Database" DataType="MSSQL" Uri="Server=NLDMIWUA51323FX\\DEVELOPMENT;Database=pScannerDataSources;Trusted_Connection=True;" Version="1.0"/>
    //    </DataSetConnections>
    //  </DataSetConnectionSection>
    /// <summary>
    /// </summary>
    public class BaseDataSetConnection {

        /// <summary>
        ///     Gets the specified data set name.
        /// </summary>
        /// <param name="dataSetName">Name of the data set.</param>
        /// <returns></returns>
        internal static DataSetConnection Get(string dataSetName) {
            if (_dataSourceLookup.ContainsKey(dataSetName) == false) {
                throw new DataSetConnectionDoesNotExist(dataSetName);
            }
            return _dataSourceLookup[dataSetName];
        }

        /// <summary>
        ///     Initializes the <see cref="BaseDataSetConnection"/> class.
        /// </summary>
        static BaseDataSetConnection() {
            _dataSourceLookup = new Dictionary<String, DataSetConnection> {
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
        private static IDictionary<String, DataSetConnection> _dataSourceLookup;
    }
}