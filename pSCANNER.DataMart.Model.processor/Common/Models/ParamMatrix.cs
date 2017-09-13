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


#endregion

namespace Lpp.Scanner.DataMart.Model.Processors.Common.Models {

    public class ParamMatrix {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ParamMatrix" /> class.
        /// </summary>
        /// <param name="paramMatrixNode">The parameter matrix node.</param>
        public ParamMatrix(XmlNode paramMatrixNode) {
            PCells = new Dictionary<string, PCell>();

            foreach (var ppCell in from XmlNode node in paramMatrixNode.ChildNodes select new PCell(node, Constants.Pmml.Tags.PMML.GeneralRegressionModel.ParamMatrix.PCell.beta) {DegreeOfFreedom = node.Attributes[Constants.Pmml.Tags.PMML.GeneralRegressionModel.ParamMatrix.PCell.df].Value, ParameterName = node.Attributes[Constants.Pmml.Tags.PMML.GeneralRegressionModel.ParamMatrix.PCell.parameterName].Value}) {
                PCells.Add(ppCell.ParameterName, ppCell);
            }
        }

        #endregion

        #region Properties

        // ReSharper disable once InconsistentNaming
        public IDictionary<string, PCell> PCells { get; private set; }

        #endregion

        // ReSharper disable once InconsistentNaming
        public class PCell : CellBase {
            #region Constructors

            /// <summary>
            ///     Initializes a new instance of the <see cref="PCell" /> class.
            /// </summary>
            /// <param name="item">The item.</param>
            /// <param name="key">The key.</param>
            public PCell(XmlNode item, string key) : base(item, key) {}

            #endregion

            #region Properties

            /// <summary>
            ///     Gets the degree of freedom.
            /// </summary>
            /// <value>
            ///     The degree of freedom.
            /// </value>
            public string DegreeOfFreedom { get; internal set; }

            /// <summary>
            ///     Gets the name of the parameter.
            /// </summary>
            /// <value>
            ///     The name of the parameter.
            /// </value>
            public string ParameterName { get; internal set; }

            #endregion
        }

    }

}