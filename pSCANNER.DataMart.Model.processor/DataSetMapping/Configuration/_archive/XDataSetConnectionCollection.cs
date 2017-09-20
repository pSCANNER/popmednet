//#region Legal Information

//// ====================================================================================
////
//// Center for Population Health Informatics
//// Solution: Lpp.Adapters
//// Project: Lpp.Scanner.DataMart.Model.Processors Last Updated By: Westerman, Dax Marek
////
//// ====================================================================================

//#endregion Legal Information

//#region Using

//using System.Configuration;

//#endregion Using

//namespace Lpp.Scanner.DataMart.Model.Processors.DataSetMapping.Configuration {
//    /// <summary>
//    /// </summary>
//    /// <seealso cref="System.Configuration.ConfigurationElementCollection"/>
//    public class XDataSetConnectionCollection : ConfigurationElementCollection {
//        /// <summary>
//        ///     When overridden in a derived class, creates a new <see cref="T:System.Configuration.ConfigurationElement"/>.
//        /// </summary>
//        /// <returns>A newly created <see cref="T:System.Configuration.ConfigurationElement"/>.</returns>
//        protected override ConfigurationElement CreateNewElement() {
//            return new DataSetConnectionElement();
//        }

//        /// <summary>
//        ///     Gets the element key for a specified configuration element when overridden in a derived class.
//        /// </summary>
//        /// <param name="element">The <see cref="T:System.Configuration.ConfigurationElement"/> to return the key for.</param>
//        /// <returns>An <see cref="T:System.Object"/> that acts as the key for the specified <see cref="T:System.Configuration.ConfigurationElement"/>.</returns>
//        protected override object GetElementKey(ConfigurationElement element) {
//            return ((DataSetConnectionElement)element).Name;
//        }
//    }
//}