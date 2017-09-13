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

using System.Diagnostics;
using System.Xml;


#endregion

namespace Lpp.Scanner.DataMart.Model.Processors.Common.Base {

    public abstract class CellBase {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="CellBase" /> class.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="key">The key.</param>
        protected CellBase(XmlNode item, string key) {
            Debug.Assert(item.Attributes != null, "item.Attributes != null");
            Value = item.Attributes[key].Value;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the value.
        /// </summary>
        /// <value>
        ///     The value.
        /// </value>
        public string Value { get; private set; }

        #endregion
    }

}