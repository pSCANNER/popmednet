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

using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;


#endregion

namespace Lpp.Scanner.DataMart.Model.Processors.Common.Models {

    public class DataDictionary {
        #region Fields

        private readonly int _numberOfFields;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DataDictionary" /> class.
        /// </summary>
        /// <param name="dataDictionaryNode">The data dictionary node.</param>
        public DataDictionary(XmlNode dataDictionaryNode) {
            DataFields = new Dictionary<string, DataField>();

            Debug.Assert(dataDictionaryNode.Attributes != null, "dataDictionaryNode.Attributes != null");

            if (int.TryParse(dataDictionaryNode.Attributes["numberOfFields"].Value, out _numberOfFields)) {
                _numberOfFields = dataDictionaryNode.ChildNodes.Count;
                foreach (XmlNode childNode in dataDictionaryNode.ChildNodes) {

                    Debug.Assert(childNode.Attributes != null, "childNode.Attributes != null");

                    DataFields.Add(childNode.Attributes["name"].Value, new DataField(childNode));
                }
            }
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the number of fields.
        /// </summary>
        /// <value>
        ///     The number of fields.
        /// </value>
        public int NumberOfFields {
            get {
                return _numberOfFields;
            }
        }

        /// <summary>
        ///     Gets the data fields.
        /// </summary>
        /// <value>
        ///     The data fields.
        /// </value>
        public Dictionary<string, DataField> DataFields { get; private set; }

        #endregion
    }

}