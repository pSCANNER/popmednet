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

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;
using Lpp.Scanner.DataMart.Model.Processors.Common.Models;
using Lpp.Scanner.DataMart.Model.Processors.Common.Pmml;


#endregion

namespace Lpp.Scanner.DataMart.Model.Processors.Common.Base {

    public abstract class PmmlBase {

        protected const string StudySiteName = "SiteName";
        private string _siteName = null;

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PmmlBase" /> class.
        /// </summary>
        /// <param name="pmml"></param>
        protected PmmlBase(string pmml) : this() {

            XmlDoc.LoadXml(pmml);

            var extensions = XmlDoc.GetElementsByTagName(Constants.Pmml.Tags.PMML.Header.Extension.TagName);

            var extensionList = extensions.GetEnumerator();

            if (extensionList != null) {

                while (extensionList.MoveNext()) {
                    var currentExtension = (XmlNode) extensionList.Current;
                    var xmlAttributeCollection = currentExtension.Attributes;
                    Debug.Assert(xmlAttributeCollection != null, "currentExtension.Attributes != null");
                    var nameAttribute = xmlAttributeCollection["name"];
                    var valueAttribute = xmlAttributeCollection["value"];

                    var nodeName = nameAttribute != null ? nameAttribute.Value : String.Empty;
                    var nodeValue = valueAttribute != null ? valueAttribute.Value : String.Empty;
                    var child = currentExtension.ChildNodes;
                    Extensions.Add(nodeName, new Extension(nodeName, nodeValue, child));
                }

            }

        }

        protected PmmlBase(string siteName, string pmml) : this(pmml) {
            _siteName = siteName;
        }

        /// <summary>
        ///     Prevents a default instance of the <see cref="PmmlBase" /> class from being created.
        /// </summary>
        protected PmmlBase() {
            Extensions = new Dictionary<string, Extension>();
            XmlDoc = new XmlDocument();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the name of the site.
        /// </summary>
        /// <value>
        ///     The name of the site.
        /// </value>
        public string SiteName {
            get {
                return _siteName ?? (_siteName = Extensions[StudySiteName].Value);
            }
        }

        /// <summary>
        ///     Gets the extensions.
        /// </summary>
        /// <value>
        ///     The extensions.
        /// </value>
        public IDictionary<string, Extension> Extensions { get; private set; }

        /// <summary>
        ///     Gets the XML document.
        /// </summary>
        /// <value>
        ///     The XML document.
        /// </value>
        protected XmlDocument XmlDoc { get; private set; }

        #endregion
    }

}