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
using System.Xml;

#endregion Using

namespace Lpp.Scanner.DataMart.Model.Processors.Common.Pmml {

    /// <summary>
    /// </summary>
    public class PmmlRequestParameterList {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="RequestParameterList"/> class.
        /// </summary>
        /// <param name="xmlNodeList">The XML node list.</param>
        public PmmlRequestParameterList(XmlNodeList xmlNodeList) : this() {
            foreach (XmlNode xmlNode in xmlNodeList) {
                var childNodes = xmlNode.ChildNodes;
                foreach (XmlNode childNode in childNodes) {
                    Debug.Assert(childNode.Attributes != null, "childNode.Attributes != null");
                    RequestParameters.Add(childNode.Attributes["name"].Value, childNode.Attributes["label"].Value);
                }
            }
        }

        /// <summary>
        ///     Prevents a default instance of the <see cref="RequestParameterList"/> class from being created.
        /// </summary>
        private PmmlRequestParameterList() {
            RequestParameters = new Dictionary<string, string>();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        ///     Gets or sets the request parameters.
        /// </summary>
        /// <value>The request parameters.</value>
        public Dictionary<string, string> RequestParameters { get; set; }

        #endregion Properties
    }
}