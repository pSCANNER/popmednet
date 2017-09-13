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
using System.Linq;
using System.Text.RegularExpressions;
using Lpp.Scanner.DataMart.Model.Processors.Common.Base;


#endregion

namespace Lpp.Scanner.DataMart.Model.Processors.Common.R {

    public class RResultParser : BaseScriptParser {

        private const string regExStr = "(?:(?<section>\\<pScannerResult name=\"(?<name>.+)\"\\>)(?<result>[^\\<]+)(?:\\</pScannerResult\\>))";
        private static readonly Regex _sectionParseRegEx = new Regex(regExStr, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.ExplicitCapture);

        /// <summary>
        ///     Parses the specified r response.
        /// </summary>
        /// <param name="rResponse">The r response.</param>
        /// <returns></returns>
        public static Dictionary<string, string> Parse(string rResponse) {
            var matches = _sectionParseRegEx.Matches(rResponse);
            var dictionary = matches.Cast<Match>().ToDictionary(match => match.Groups["name"].Value, match => match.Groups["result"].Value);
            return dictionary;
        }

    }

}