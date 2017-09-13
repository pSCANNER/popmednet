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

namespace Lpp.Scanner.DataMart.Model.Processors.Common.Parameters {

    public interface IUserJsonInputParameter {
        #region Properties

        /// <summary>
        ///     Gets the dataset json.
        /// </summary>
        /// <value>
        ///     The dataset json.
        /// </value>
        string DatasetJson { get; }

        /// <summary>
        ///     Gets the parameters json.
        /// </summary>
        /// <value>
        ///     The parameters json.
        /// </value>
        string ParametersJson { get; }

        /// <summary>
        ///     Gets the PMML json.
        /// </summary>
        /// <value>
        ///     The PMML json.
        /// </value>
        string PmmlJson { get; }

        #endregion
    }

}