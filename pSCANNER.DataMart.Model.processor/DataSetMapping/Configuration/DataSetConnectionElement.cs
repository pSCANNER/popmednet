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

using System.Configuration;


#endregion

namespace Lpp.Scanner.DataMart.Model.Processors.DataSetMapping.Configuration {

    public class DataSetConnectionElement : ConfigurationElement {

        /// <summary>
        /// </summary>
        public enum ConnectionTypeEnum {

            HTTP,
            Database

        }

        /// <summary>
        /// </summary>
        public enum DataTypeEnum {

            MSSQL,
            CSV

        }

        #region Properties

        /// <summary>
        ///     Gets or sets the type of the connection.
        /// </summary>
        /// <value>
        ///     The type of the connection.
        /// </value>
        [ConfigurationProperty("ConnectionType", IsRequired = true)]
        public ConnectionTypeEnum ConnectionType {
            get {
                return (ConnectionTypeEnum) this["ConnectionType"];
            }
            set {
                this["ConnectionType"] = value;
            }
        }

        /// <summary>
        ///     Gets or sets the type of the data.
        /// </summary>
        /// <value>
        ///     The type of the data.
        /// </value>
        [ConfigurationProperty("DataType", IsRequired = true)]
        public DataTypeEnum DataType {
            get {
                return (DataTypeEnum) this["DataType"];
            }
            set {
                this["DataType"] = value;
            }
        }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        [ConfigurationProperty("Name", IsRequired = true)]
        public string Name {
            get {
                return (string) this["Name"];
            }
            set {
                this["Name"] = value;
            }
        }

        public string Source { get; set; }

        /// <summary>
        ///     Gets or sets the URI.
        /// </summary>
        /// <value>
        ///     The URI.
        /// </value>
        [ConfigurationProperty("Uri", IsRequired = true)]
        public string Uri {
            get {
                return (string) this["Uri"];
            }
            set {
                this["Uri"] = value;
            }
        }

        /// <summary>
        ///     Gets or sets the version.
        /// </summary>
        /// <value>
        ///     The version.
        /// </value>
        [ConfigurationProperty("Version", IsRequired = true)]
        public string Version {
            get {
                return (string) this["Version"];
            }
            set {
                this["Version"] = value;
            }
        }

        #endregion
    }

}