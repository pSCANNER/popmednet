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

namespace Lpp.Scanner.DataMart.Model.Processors.Common.Pmml {

    /// <summary>
    /// </summary>
    public class Extension {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Extension" /> class.
        /// </summary>
        /// <param name="name">Name of the node.</param>
        /// <param name="value">The node value.</param>
        /// <param name="children"></param>
        public Extension(string name, string value, XmlNodeList children) {
            Name = name;
            Value = value;
            Children = children;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the children.
        /// </summary>
        /// <value>
        ///     The children.
        /// </value>
        public XmlNodeList Children { get; private set; }

        /// <summary>
        ///     Gets the name of the node.
        /// </summary>
        /// <value>
        ///     The name of the node.
        /// </value>
        public string Name { get; private set; }

        /// <summary>
        ///     Gets the node value.
        /// </summary>
        /// <value>
        ///     The node value.
        /// </value>
        public string Value { get; private set; }

        #endregion
    }

}