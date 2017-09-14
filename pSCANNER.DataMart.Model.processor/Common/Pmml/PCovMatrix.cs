#region Legal Information

// ====================================================================================
//
// Center for Population Health Informatics
// Solution: Lpp.Adapters
// Project: Lpp.Scanner.DataMart.Model.Processors Last Updated By: Westerman, Dax Marek
//
// ====================================================================================

#endregion Legal Information

#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Lpp.Scanner.DataMart.Model.Processors.Aggregation;
using Lpp.Scanner.DataMart.Model.Processors.Common.Base;
using Lpp.Scanner.DataMart.Model.Processors.Common.Models;
using Lpp.Scanner.DataMart.Model.Processors.Aggregation.Common;

#endregion Using

namespace Lpp.Scanner.DataMart.Model.Processors.Common.Pmml {

    /// <summary>
    /// </summary>
    /// <seealso cref="Lpp.Scanner.DataMart.Model.Processors.Common.Base.PmmlBase"/>
    public class PCovMatrix : PmmlBase {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PCovMatrix"/> class.
        /// </summary>
        /// <param name="pmml">The PMML.</param>
        /// <exception cref="System.Exception">Could not locate TagName tag</exception>
        /// <exception cref="System.NullReferenceException">Could not find PCovCells</exception>
        public PCovMatrix(SiteResponse pmml) : base(pmml.SiteName, pmml.Response) {
            PCovCells = new List<PCovCell>();

            var pCovMatrices = XmlDoc.GetElementsByTagName(Constants.Pmml.Tags.PMML.GeneralRegressionModel.PCovMatrix.TagName);

            if (pCovMatrices.Count == 0) {
                throw new Exception("Could not locate TagName tag");
            }

            var pCovMatrix = pCovMatrices[0];

            var pCovCells = pCovMatrix.ChildNodes.Cast<XmlNode>().Where(x => x.Name == Constants.Pmml.Tags.PMML.GeneralRegressionModel.PCovMatrix.PCovCell.TagName).ToList();

            if (pCovCells == null) {
                throw new NullReferenceException("Could not find PCovCells");
            }

            foreach (var item in pCovCells) {
                PCovCells.Add(new PCovCell(item));
            }
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        ///     Gets the p cov cells.
        /// </summary>
        /// <value>The p cov cells.</value>
        public List<PCovCell> PCovCells { get; private set; }

        #endregion Properties
    }
}