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


#endregion

namespace Lpp.Scanner.DataMart.Model.Processors.Common.Pmml {

    /// <summary>
    /// </summary>
    public class MiningSchema {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="MiningSchema" /> class.
        /// </summary>
        /// <param name="miningSchemaNode">The mining schema node.</param>
        /// <exception cref="System.Exception">Could not locate usage type</exception>
        public MiningSchema(XmlNode miningSchemaNode) : this() {

            var childNodes = miningSchemaNode.ChildNodes;
            foreach (XmlNode childNode in childNodes) {

                Debug.Assert(childNode.Attributes != null, "childNode.Attributes != null");
                var name = childNode.Attributes["name"].Value;

                switch (childNode.Attributes["usageType"].Value) {
                    case "target":
                    case "predicted": {
                        DependentVariable = name;
                        break;
                    }
                    case "active": {
                        IndependantVariables.Add(name);
                        break;
                    }
                    default: {
                        throw new Exception("Could not locate usage type");
                    }
                }
            }
        }

        /// <summary>
        ///     Prevents a default instance of the <see cref="MiningSchema" /> class from being created.
        /// </summary>
        private MiningSchema() {
            IndependantVariables = new List<string>();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the dependent variable.
        /// </summary>
        /// <value>
        ///     The dependent variable.
        /// </value>
        public string DependentVariable { get; private set; }

        /// <summary>
        ///     Gets the independant variables.
        /// </summary>
        /// <value>
        ///     The independant variables.
        /// </value>
        public IList<string> IndependantVariables { get; private set; }

        #endregion
    }

}