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

using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Xml;
using LinqKit;
using Lpp.Scanner.DataMart.Model.Processors.Aggregation;
using Lpp.Scanner.DataMart.Model.Processors.Common.Base;
using Lpp.Scanner.DataMart.Model.Processors.Common.ExtMethods;
using Lpp.Scanner.DataMart.Model.Processors.Aggregation.Common;

#endregion Using

namespace Lpp.Scanner.DataMart.Model.Processors.Common.Models {

    public class ErrorGradientModel : PmmlBase, IModelBase {

        #region Fields

        private readonly Dictionary<string, string> _values;

        #endregion Fields

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ErrorGradientModel"/> class.
        /// </summary>
        public ErrorGradientModel() {
            _values = new Dictionary<string, string>();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ErrorGradientModel"/> class.
        /// </summary>
        /// <param name="pmml">The PMML.</param>
        public ErrorGradientModel(SiteResponse pmml) : base(pmml.SiteName, pmml.Response) {
            _values = new Dictionary<string, string>();
            var errorGradient = Extensions["errorGradient"];
            foreach (XmlNode child in errorGradient.Children) {
                Debug.Assert(child.Attributes != null, "child.Attributes != null");
                var name = child.Attributes["name"].Value;
                var value = child.Attributes["value"].Value;
                _values.Add(name, value);
            }
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        ///     Gets or sets the values.
        /// </summary>
        /// <value>The values.</value>
        public IDictionary<string, string> Values {
            get {
                return _values;
            }
        }

        #endregion Properties

        #region Interface Implementation

        /// <summary>
        ///     Gets the PMML value.
        /// </summary>
        /// <returns></returns>
        public string GetPmmlValue() {
            var sb = new StringBuilder();
            sb.AppendLine();

            foreach (var value in Values) {
                sb.AppendFormat("<error name=\"{0}\" value=\"{1}\" />", value.Key.DropPrefix("x"), value.Value);
                sb.AppendLine();
            }

            return sb.ToString();
        }

        #endregion Interface Implementation
    }
}