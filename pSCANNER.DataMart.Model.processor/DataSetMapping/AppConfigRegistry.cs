#region Legal Information

// ====================================================================================
//
// Center for Population Health Informatics
// Solution: Lpp.Adapters
// Project: Lpp.Scanner.DataMart.Model.Processors Last Updated By: Westerman, Dax Marek
//
// ====================================================================================

#endregion Legal Information

#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;
using Lpp.Scanner.DataMart.Model.Processors.DataSetMapping.Configuration;

#endregion Using

namespace Lpp.Scanner.DataMart.Model.Processors.DataSetMapping {

    /// <summary>
    /// </summary>
    /// <seealso cref="Lpp.Scanner.DataMart.Model.Processors.DataSetMapping.IRegistry"/>
    public class AppConfigRegistry : IRegistry {

        #region Interface Implementation

        /// <summary>
        ///     Gets the data set connection element map.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, DataSetConnectionElement> GetDataSetConnectionElementMap() {
            var retVal = new Dictionary<string, DataSetConnectionElement>();

            var xmlElement = getConfigXml();

            foreach (XmlNode dataSet in xmlElement.ChildNodes) {
                if (dataSet.Name == "setting" && dataSet.Attributes["name"].Value == "DataSetMappings") {
                    //<value>DataSet=MTE|ConnectionType="Database"|DataType="MSSQL"|Uri="Server=localpmn;Database=pSCANNER;uid=sa;pwd=pscanner;Query=SELECT * FROM [dbo].[MTE_DataSet_1];"|Version="1.0"</value>
                    var value = dataSet.ChildNodes[0].InnerXml;
                    var values = value.Split('|');
                    var dictionary = values.Select(x => x.Split(':')).ToDictionary(x => x[0], x => x[1]);
                    var dataSetConnectionElement = new DataSetConnectionElement();
                    var dataSetName = dictionary["DataSet"].Replace("\"", "");
                    dataSetConnectionElement.Name = dataSetName;
                    dataSetConnectionElement.ConnectionType = dictionary["ConnectionType"].Replace("\"", "") == "Database" ? DataSetConnectionElement.ConnectionTypeEnum.Database : DataSetConnectionElement.ConnectionTypeEnum.HTTP;
                    dataSetConnectionElement.DataType = dictionary["DataType"].Replace("\"", "") == "MSSQL" ? DataSetConnectionElement.DataTypeEnum.MSSQL : DataSetConnectionElement.DataTypeEnum.CSV;
                    dataSetConnectionElement.Source = dataSetName;
                    dataSetConnectionElement.Uri = dictionary["Uri"].Replace("\"", "");
                    dataSetConnectionElement.Version = dictionary["Version"].Replace("\"", "");
                    retVal.Add(dataSetName, dataSetConnectionElement);
                }
            }

            return retVal;
        }

        /// <summary>
        ///     Gets the r section.
        /// </summary>
        /// <returns></returns>
        public string getRSection() {
            string retVal = string.Empty;
            var xmlElement = getConfigXml();

            foreach (XmlNode dataSet in xmlElement.ChildNodes) {
                if (dataSet.Name == "setting" && dataSet.Attributes["name"].Value == "RSection") {
                    retVal = dataSet.ChildNodes[0].InnerXml;
                }
            }

            return retVal;
        }

        /// <summary>
        ///     Gets the configuration XML.
        /// </summary>
        /// <returns></returns>
        private static XmlElement getConfigXml() {
            var currentAssembly = Assembly.GetCallingAssembly();
            var configPath = new Uri(currentAssembly.CodeBase).LocalPath.Replace("Lpp.Dns.DataMart.Client.DomainManger.dll", "Lpp.Dns.DataMart.Client.exe.config");
            var doc = new XmlDocument();
            doc.Load(configPath);
            var xmlElement = doc["configuration"]["userSettings"]["Lpp.Dns.DataMart.Client.Properties.Settings"];
            return xmlElement;
        }

        #endregion Interface Implementation
    }
}