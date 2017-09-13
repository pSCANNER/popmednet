using System.Collections.Generic;


namespace Lpp.Scanner.DataMart.Model.Processors.Analysis {

    internal class MiningFields
    {
        #region Properties

        public string DependantVariable { get; set; }

        public IList<string> IndependantVariables { get; set; }

        #endregion
    }

}