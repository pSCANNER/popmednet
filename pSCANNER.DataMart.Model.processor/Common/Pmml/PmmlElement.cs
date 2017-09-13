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
using System.Xml;


#endregion

namespace Lpp.Scanner.DataMart.Model.Processors.Common.Pmml {

    internal class PmmlElement {

        private const string localname = "PMML";

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PmmlElement" /> class.
        /// </summary>
        /// <param name="element">The element.</param>
        public PmmlElement(XmlElement element) {
            Element = element;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the element.
        /// </summary>
        /// <value>
        ///     The element.
        /// </value>
        public XmlElement Element { get; private set; }

        #endregion

        /// <summary>
        ///     Extracts the PMML element.
        /// </summary>
        /// <param name="xmlDocument">The XML document.</param>
        /// <returns></returns>
        /// <exception cref="System.NullReferenceException">Does not contain PMML reference</exception>
        public static PmmlElement ExtractPmmlElement(XmlDocument xmlDocument) {
            var retVal = xmlDocument[localname];
            if (retVal == null) {
                throw new NullReferenceException("Does not contain PMML reference");
            }
            return new PmmlElement(retVal);
        }

    }

}