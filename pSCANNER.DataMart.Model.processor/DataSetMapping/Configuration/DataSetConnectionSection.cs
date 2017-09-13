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

    public class DataSetConnectionSection : ConfigurationSection {

        /// <summary>
        ///     The name of this section in the app.config.
        /// </summary>
        public const string SectionName = "DataSetConnectionSection";

        private const string dataSetConnectionName = "DataSetConnections";

        #region Properties

        [ConfigurationProperty(dataSetConnectionName)]
        [ConfigurationCollection(typeof(DataSetConnectionCollection), AddItemName = "add")]
        public DataSetConnectionCollection DataSetConnections {
            get {
                return (DataSetConnectionCollection) base[dataSetConnectionName];
            }
        }

        #endregion
    }

}