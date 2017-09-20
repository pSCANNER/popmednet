#region Legal Information

// ====================================================================================
//
// Center for Population Health Informatics
// Solution: Lpp.Adapters
// Project: Lpp.Scanner.DataMart.Model.Processors Last Updated By: Westerman, Dax Marek
//
// ====================================================================================

#endregion Legal Information

using Lpp.Scanner.DataMart.Model.Processors.Common.Base;
using Lpp.Scanner.DataMart.Model.Processors.Common.Pmml;

namespace Lpp.Scanner.DataMart.Model.Processors.Common.Parameters {

    /// <summary>
    /// </summary>
    public interface IRExecution : IDataSetConnectionParameter {

        #region Properties

        /// <summary>
        ///     Gets the execution path.
        /// </summary>
        /// <value>The execution path.</value>
        string ExecutionPath { get; }

        #endregion Properties

        /// <summary>
        ///     Sets the PMML document.
        /// </summary>
        /// <param name="pmml">The PMML.</param>
        void SetPmmlDocument(string pmml);

        /// <summary>
        ///     Gets or sets the PMML.
        /// </summary>
        /// <value>The PMML.</value>
        PmmlDocument Pmml { get; set; }
    }
}