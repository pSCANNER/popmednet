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

using System.Collections.Generic;
using Lpp.Scanner.DataMart.Model.Processors.DataSetMapping.Configuration;


#endregion

namespace Lpp.Scanner.DataMart.Model.Processors.DataSetMapping {

    public class MockRegistry : IRegistry {
        #region Interface Implementation

        /// <summary>
        ///     Gets the data set connection element map.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, DataSetConnectionElement> GetDataSetConnectionElementMap() {
            var dataSetConnectionElementMap = new Dictionary<string, DataSetConnectionElement>();

            dataSetConnectionElementMap.Add("Example", new DataSetConnectionElement {Name = "Example", ConnectionType = DataSetConnectionElement.ConnectionTypeEnum.HTTP, DataType = DataSetConnectionElement.DataTypeEnum.CSV, Version = "1.0", Uri = "http://www.ats.ucla.edu/stat/data/binary.csv"});

            //dataSetConnectionElementMap.Add("Test", new DataSetConnectionElement {
            //                                                                         Name = "Test",
            //                                                                         ConnectionType = DataSetConnectionElement.ConnectionTypeEnum.Database,
            //                                                                         DataType = DataSetConnectionElement.DataTypeEnum.MSSQL,
            //                                                                         Version = "1.0",
            //                                                                         Uri = "Server=NLDMIWUA51323FX\\DEVELOPMENT;Database=pScannerDataSets;User Id=myUsername;Password=myPassword;",
            //                                                                         Source = "Test"
            //                                                                     });

            dataSetConnectionElementMap.Add("MTE", new DataSetConnectionElement {Name = "MTE", ConnectionType = DataSetConnectionElement.ConnectionTypeEnum.Database, DataType = DataSetConnectionElement.DataTypeEnum.MSSQL, Version = "1.0", Uri = "Server=localpmn;Database=pSCANNER;uid=sa;pwd=pscanner;Query=SELECT * FROM [dbo].[MTE_DataSet];", Source = "MTE"});
            dataSetConnectionElementMap.Add("Dobson1990", new DataSetConnectionElement {Name = "Dobson1990", ConnectionType = DataSetConnectionElement.ConnectionTypeEnum.Database, DataType = DataSetConnectionElement.DataTypeEnum.MSSQL, Version = "1.0", Uri = "Server=NLDMIWUA51323FX\\\\DEVELOPMENT;Database=pScannerDataSets;Trusted_Connection=True;Query=SELECT * FROM [dbo].[Dobson1990];", Source = "Dobson1990"});
            dataSetConnectionElementMap.Add("Dobson1990[1]", new DataSetConnectionElement {Name = "Dobson1990[1]", ConnectionType = DataSetConnectionElement.ConnectionTypeEnum.Database, DataType = DataSetConnectionElement.DataTypeEnum.MSSQL, Version = "1.0", Uri = "Server=NLDMIWUA51323FX\\\\DEVELOPMENT;Database=pScannerDataSets;Trusted_Connection=True;Query=SELECT * FROM [dbo].[Dobson1990] where [dataSetIndex]=1;", Source = "Dobson1990"});
            dataSetConnectionElementMap.Add("Dobson1990[2]", new DataSetConnectionElement {Name = "Dobson1990[2]", ConnectionType = DataSetConnectionElement.ConnectionTypeEnum.Database, DataType = DataSetConnectionElement.DataTypeEnum.MSSQL, Version = "1.0", Uri = "Server=NLDMIWUA51323FX\\\\DEVELOPMENT;Database=pScannerDataSets;Trusted_Connection=True;Query=SELECT * FROM [dbo].[Dobson1990] where [dataSetIndex]=2;", Source = "Dobson1990"});
            dataSetConnectionElementMap.Add("Dobson1990[3]", new DataSetConnectionElement {Name = "Dobson1990[3]", ConnectionType = DataSetConnectionElement.ConnectionTypeEnum.Database, DataType = DataSetConnectionElement.DataTypeEnum.MSSQL, Version = "1.0", Uri = "Server=NLDMIWUA51323FX\\\\DEVELOPMENT;Database=pScannerDataSets;Trusted_Connection=True;Query=SELECT * FROM [dbo].[Dobson1990] where [dataSetIndex]=3;", Source = "Dobson1990"});

            return dataSetConnectionElementMap;
        }

        public string getRSection() {
            return string.Empty;
        }

        #endregion
    }

}