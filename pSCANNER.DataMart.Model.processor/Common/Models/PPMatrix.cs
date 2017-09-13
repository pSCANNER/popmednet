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
using System.Xml;
using Lpp.Scanner.DataMart.Model.Processors.Common.Base;


#endregion

namespace Lpp.Scanner.DataMart.Model.Processors.Common.Models {

    // ReSharper disable once InconsistentNaming
    public class PPMatrix {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PPMatrix" /> class.
        /// </summary>
        /// <param name="ppMatrixNode">The pp matrix node.</param>
        public PPMatrix(XmlNode ppMatrixNode) {
            PPCells = new Dictionary<string, PPCell>();

            foreach (XmlNode node in ppMatrixNode.ChildNodes) {
                var ppCell = new PPCell(node) {PredictorName = node.Attributes["predictorName"].Value, ParameterName = node.Attributes["parameterName"].Value};
                PPCells.Add(ppCell.ParameterName, ppCell);
            }
        }

        #endregion

        #region Properties

        // ReSharper disable once InconsistentNaming
        public IDictionary<string, PPCell> PPCells { get; private set; }

        #endregion

        // ReSharper disable once InconsistentNaming
        public class PPCell : CellBase {
            #region Constructors

            /// <summary>
            ///     Initializes a new instance of the <see cref="PPCell" /> class.
            /// </summary>
            /// <param name="item">The item.</param>
            public PPCell(XmlNode item) : base(item, Constants.Pmml.Tags.PMML.GeneralRegressionModel.PPMatrix.PPCell.value) {}

            #endregion

            #region Properties

            /// <summary>
            ///     Gets the name of the parameter.
            /// </summary>
            /// <value>
            ///     The name of the parameter.
            /// </value>
            public string ParameterName { get; internal set; }

            /// <summary>
            ///     Gets the name of the predictor.
            /// </summary>
            /// <value>
            ///     The name of the predictor.
            /// </value>
            public string PredictorName { get; internal set; }

            #endregion
        }

    }

}