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

    public class DataSetConnectionCollection : ConfigurationElementCollection {

        protected override ConfigurationElement CreateNewElement() {
            return new DataSetConnectionElement();
        }

        protected override object GetElementKey(ConfigurationElement element) {
            return ((DataSetConnectionElement) element).Name;
        }

    }

}