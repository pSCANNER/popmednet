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

using Lpp.Scanner.DataMart.Model.Processors.DataSetMapping.Configuration;


#endregion

namespace Lpp.Scanner.DataMart.Model.Processors.DataSetMapping {

    public abstract class BaseDataSetConnection {
        #region Fields

        private readonly DataSetConnectionElement _element;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseDataSetConnection" /> class.
        /// </summary>
        /// <param name="element">The element.</param>
        protected BaseDataSetConnection(DataSetConnectionElement element) {
            _element = element;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the type of the data.
        /// </summary>
        /// <value>
        ///     The type of the data.
        /// </value>
        public DataSetConnectionElement.DataTypeEnum DataType {
            get {
                return _element.DataType;
            }
        }

        /// <summary>
        ///     Gets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        public string Name {
            get {
                return _element.Name;
            }
        }

        /// <summary>
        ///     Gets or sets the version.
        /// </summary>
        /// <value>
        ///     The version.
        /// </value>
        public string Version {
            get {
                return _element.Version;
            }
        }

        /// <summary>
        ///     Gets the URI.
        /// </summary>
        /// <value>
        ///     The URI.
        /// </value>
        protected string Uri {
            get {
                return _element.Uri;
            }
        }

        #endregion
    }

}