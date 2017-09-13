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

namespace Lpp.Scanner.DataMart.Model.Processors.Aggregation {

    public class SiteResponse {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="SiteResponse" /> class.
        /// </summary>
        /// <param name="siteName">Name of the site.</param>
        /// <param name="response">The response.</param>
        public SiteResponse(string siteName, string response) {
            SiteName = siteName;
            Response = response;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the response.
        /// </summary>
        /// <value>
        ///     The response.
        /// </value>
        public string Response { get; set; }

        /// <summary>
        ///     Gets or sets the name of the site.
        /// </summary>
        /// <value>
        ///     The name of the site.
        /// </value>
        public string SiteName { get; set; }

        #endregion
    }

}