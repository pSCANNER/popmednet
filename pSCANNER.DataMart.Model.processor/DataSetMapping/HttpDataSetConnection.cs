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

    public class HttpDataSetConnection : BaseDataSetConnection {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="HttpDataSetConnection" /> class.
        /// </summary>
        /// <param name="element">The element.</param>
        public HttpDataSetConnection(DataSetConnectionElement element) : base(element) {}

        public HttpDataSetConnection() : base(null){
            
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the URI.
        /// </summary>
        /// <value>
        ///     The URI.
        /// </value>
        public string Url {
            get {
                return Uri;
            }
        }

        #endregion
    }

}