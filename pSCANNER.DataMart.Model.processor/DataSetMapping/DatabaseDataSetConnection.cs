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

    internal class DatabaseDataSetConnection : BaseDataSetConnection {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DatabaseDataSetConnection" /> class.
        /// </summary>
        /// <param name="element">The element.</param>
        public DatabaseDataSetConnection(DataSetConnectionElement element) : base(element) {}

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the connection string.
        /// </summary>
        /// <value>
        ///     The connection string.
        /// </value>
        public string ConnectionString {
            get {
                return Uri;
            }
        }

        #endregion
    }

}