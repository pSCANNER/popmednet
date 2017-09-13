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

#endregion

#region Using

#endregion

#region Using

using System;
using Lpp.Scanner.DataMart.Model.Processors.Common.Base;


#endregion

namespace Lpp.Scanner.DataMart.Model.Processors.Common.R {

    /// <summary>
    /// </summary>
    /// <seealso cref="BaseScriptBuilder" />
    /// <seealso cref="BaseScriptBuilder" />
    public class RScriptBuilder : BaseScriptBuilder {

        private const string cranLocation = "http://cran.rstudio.com/";

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="RScriptBuilder" /> class.
        /// </summary>
        /// <param name="title">The title.</param>
        public RScriptBuilder(string title) {

            AddLine("########################################################");
            AddLine("#  {0}", title);
            AddLine("########################################################");
        }

        #endregion

        /// <summary>
        ///     Adds the response line.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="section">The section.</param>
        public void AddResponseLine(string response, string section) {
            AddLine(string.Format("writeLines('<pScannerResult name=\"{0}\">')", section));
            AddLine(response);
            AddLine("writeLines('</pScannerResult>')");
        }

        /// <summary>
        ///     Gets the response.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public static string GetResponse(string response, string token) {
            var lookingFor = string.Format("<pScannerResult name=\"{0}\">", token);
            var indexOfBeginning = response.IndexOf(lookingFor, StringComparison.Ordinal) + lookingFor.Length;
            var indexOfEnd = response.IndexOf("</pScannerResult>", indexOfBeginning, StringComparison.Ordinal);
            var substring = response.Substring(indexOfBeginning, indexOfEnd - indexOfBeginning);
            return substring;
        }

        /// <summary>
        ///     Adds the requirement.
        /// </summary>
        /// <param name="library">The library.</param>
        /// <returns></returns>
        public RScriptBuilder AddRequirement(string library) {
            AddLine(string.Format(@"if (!require(""{0}"")) {{ 
                                        install.packages(""{0}"", repos=""{1}"", dependencies=TRUE); 
                                        library({0}); 
                                    }}", library, cranLocation));
            return this;
        }

    }

}