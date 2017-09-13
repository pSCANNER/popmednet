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

namespace Lpp.Scanner.DataMart.Model.Processors.Common.Models {

    /// <summary>
    /// </summary>
    /// <seealso cref="IModelBase" />
    public class ConvergenceWarningModel : IModelBase {
        #region Properties

        /// <summary>
        ///     Gets or sets a value indicating whether [cannot converge].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [cannot converge]; otherwise, <c>false</c>.
        /// </value>
        public bool CannotConverge { private get; set; }

        #endregion

        #region Interface Implementation

        /// <summary>
        ///     Gets the PMML value.
        /// </summary>
        /// <returns></returns>
        public string GetPmmlValue() {
            return CannotConverge.ToString();
        }

        #endregion
    }

}