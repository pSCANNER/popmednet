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

using System.Xml;
using Lpp.Scanner.DataMart.Model.Processors.Common.Base;
using Lpp.Scanner.DataMart.Model.Processors.Common.Models;


#endregion

namespace Lpp.Scanner.DataMart.Model.Processors.Common.Pmml {

    public class PCovCell : CellBase {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PCovCell" /> class.
        /// </summary>
        /// <param name="item">The item.</param>
        public PCovCell(XmlNode item) : base(item, Constants.Pmml.Tags.PMML.GeneralRegressionModel.PCovMatrix.value) {
            Row = item.Attributes[Constants.Pmml.Tags.PMML.GeneralRegressionModel.PCovMatrix.pRow].Value;
            Column = item.Attributes[Constants.Pmml.Tags.PMML.GeneralRegressionModel.PCovMatrix.pCol].Value;
            TargetCategory = item.Attributes[Constants.Pmml.Tags.PMML.GeneralRegressionModel.PCovMatrix.targetCategory].Value;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the column.
        /// </summary>
        /// <value>
        ///     The column.
        /// </value>
        public string Column { get; private set; }

        /// <summary>
        ///     Gets the row.
        /// </summary>
        /// <value>
        ///     The row.
        /// </value>
        public string Row { get; private set; }

        /// <summary>
        ///     Gets the target category.
        /// </summary>
        /// <value>
        ///     The target category.
        /// </value>
        public string TargetCategory { get; private set; }

        #endregion
    }

}