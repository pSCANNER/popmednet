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


#endregion

namespace Lpp.Scanner.DataMart.Model.Processors.Common.Models {

    public class PreviousIteration {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PreviousIteration" /> class.
        /// </summary>
        /// <param name="xmlNode">The XML node.</param>
        public PreviousIteration(XmlNode xmlNode) {
            Node = xmlNode;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the node.
        /// </summary>
        /// <value>
        ///     The node.
        /// </value>
        public XmlNode Node { get; private set; }

        #endregion

        /// <summary>
        ///     Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        ///     A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() {
            return Node.OuterXml;
        }

    }

}