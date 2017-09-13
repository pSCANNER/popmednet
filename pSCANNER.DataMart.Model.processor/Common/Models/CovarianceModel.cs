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
using System.Text;
using Lpp.Scanner.DataMart.Model.Processors.Common.ExtMethods;


#endregion

namespace Lpp.Scanner.DataMart.Model.Processors.Common.Models {

    /// <summary>
    /// </summary>
    /// <seealso cref="IModelBase" />
    public class CovarianceModel : IModelBase {
        #region Properties

        public CovModelEntry Entries { get; set; }

        #endregion

        #region Interface Implementation

        /// <summary>
        /// Gets the PMML value.
        /// </summary>
        /// <returns></returns>
        public string GetPmmlValue() {

            var entryLists = Entries.Get();

            var sb = new StringBuilder();

            foreach (var rowName in entryLists.Keys) {
                var entryList = entryLists[rowName];
                var values = entryList.ToList();
                var index = 0;

                foreach (var colName in entryLists.Keys) {
                    sb.AppendFormat("<PCovCell pRow=\"{0}\" pCol=\"{1}\" value=\"{2}\" targetCategory=\"y\" />", rowName.DropPrefix("x"), colName.DropPrefix("x"), values[index]);
                    sb.AppendLine();
                    index++;
                }
            }

            return sb.ToString();
        }

        #endregion

        /// <summary>
        ///     Sets the specified dictionary.
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        public void Set(Dictionary<string, List<double>> dictionary) {
            Entries = new CovModelEntry(dictionary);
        }

    }

}