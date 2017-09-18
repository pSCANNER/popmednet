using System.Collections.Generic;

namespace Lpp.Scanner.DataMart.Model.Processors.Analysis.Common {

    /// <summary>
    /// </summary>
    internal class MiningFields {

        #region Properties

        /// <summary>
        ///     Gets or sets the dependant variable.
        /// </summary>
        /// <value>The dependant variable.</value>
        public string DependantVariable { get; set; }

        /// <summary>
        ///     Gets or sets the independant variables.
        /// </summary>
        /// <value>The independant variables.</value>
        public IList<string> IndependantVariables { get; set; }

        #endregion Properties
    }
}