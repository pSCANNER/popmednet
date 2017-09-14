using System.Collections.Generic;

namespace Lpp.Scanner.DataMart.Model.Processors.Analysis.Common {

    internal class MiningFields {

        #region Properties

        public string DependantVariable { get; set; }

        public IList<string> IndependantVariables { get; set; }

        #endregion Properties
    }
}