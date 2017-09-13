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
using System.Linq;
using System.Xml;
using Lpp.Scanner.DataMart.Model.Processors.Common.Base;
using Lpp.Scanner.DataMart.Model.Processors.Common.Models;


#endregion

namespace Lpp.Scanner.DataMart.Model.Processors.Common.Pmml {

    public class DataDictionaryEntry : PmmlBase {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DataDictionaryEntry" /> class.
        /// </summary>
        /// <param name="entry">The entry.</param>
        public DataDictionaryEntry(string entry) : base(entry) {
            var dataDictionaries = XmlDoc.GetElementsByTagName(Constants.Pmml.Tags.PMML.DataDictionary.TagName);

            var dataDictionary = dataDictionaries[0];

            DataFields = dataDictionary.ChildNodes.Cast<XmlNode>().Where(x => x.Name == Constants.Pmml.Tags.PMML.DataDictionary.DataField).Select(x => new DataField(x)).ToList();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the data fields.
        /// </summary>
        /// <value>
        ///     The data fields.
        /// </value>
        public List<DataField> DataFields { get; private set; }

        #endregion
    }

}