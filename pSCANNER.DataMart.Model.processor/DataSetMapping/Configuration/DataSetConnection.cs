#region Legal Information

// ====================================================================================
//  
//      Center for Population Health Informatics
//      Solution: Lpp.Adapters
//      Project: Lpp.Scanner.DataMart.Model.Processors
//      Last Updated By: Westerman, Dax Marek
// 
// ====================================================================================

#endregion

#region Using

using System;


#endregion

namespace Lpp.Scanner.DataMart.Model.Processors.DataSetMapping.Configuration {

    public static class DataSetConnection {

        /// <summary>
        ///     Gets the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static BaseDataSetConnection Get(string name) {

            //<DataSetConnectionSection>
            //    <DataSetConnections>
            //      <add Name="Example" ConnectionType="HTTP" DataType="CSV" Uri="http://www.ats.ucla.edu/stat/data/binary.csv" />
            //      <add Name="Test" ConnectionType="Database" DataType="MSSQL" Uri="Server=DAX-PC;Database=pScannerDataSources;User Id=myUsername;Password=myPassword;" Version="1.0" />
            //      <add Name="MTE" ConnectionType="Database" DataType="MSSQL" Uri="Server=NLDMIWUA51323FX\\DEVELOPMENT;Database=pScannerDataSources;Trusted_Connection=True;" Version="1.0"/>
            //    </DataSetConnections>
            //  </DataSetConnectionSection>

            BaseDataSetConnection connection = null;

            //var section = (DataSetConnectionSection) ConfigurationManager.GetSection(DataSetConnectionSection.SectionName);
            IRegistry registry = new AppConfigRegistry();
            var dataSetConnectionElementMap = registry.GetDataSetConnectionElementMap();

            var dataSetConnectionElement = dataSetConnectionElementMap[name];

            switch (dataSetConnectionElement.ConnectionType) {

                case DataSetConnectionElement.ConnectionTypeEnum.HTTP: {
                    connection = new HttpDataSetConnection(dataSetConnectionElement);
                    break;
                }
                case DataSetConnectionElement.ConnectionTypeEnum.Database: {
                    connection = new DatabaseDataSetConnection(dataSetConnectionElement);
                    break;
                }
                default: {
                    throw new ArgumentOutOfRangeException();
                }
            }

            return connection;
        }

    }

}
