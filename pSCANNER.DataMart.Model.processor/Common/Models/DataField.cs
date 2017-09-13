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

    public class DataField {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DataField" /> class.
        /// </summary>
        /// <param name="childNode">The child node.</param>
        public DataField(XmlNode childNode) {
            Name = childNode.Attributes["name"].Value;
            Optype = childNode.Attributes["optype"].Value;
            DataType = childNode.Attributes["dataType"].Value;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the type of the data.
        /// </summary>
        /// <value>
        ///     The type of the data.
        /// </value>
        public string DataType { get; private set; }

        /// <summary>
        ///     Gets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        public string Name { get; private set; }

        /// <summary>
        ///     Gets the optype.
        /// </summary>
        /// <value>
        ///     The optype.
        /// </value>
        public string Optype { get; private set; }

        #endregion
    }

}