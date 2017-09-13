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


#endregion

namespace Lpp.Scanner.DataMart.Model.Processors.Common.ExtMethods {

    public static class StringsExt {

        /// <summary>
        ///     Joins the specified delimiter.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <returns></returns>
        public static string Join(this IEnumerable<string> values, string delimiter) {
            return string.Join(delimiter, values);
        }

        /// <summary>
        ///     To the double.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static double ToDouble(this string value) {
            double result;
            double.TryParse(value, out result);
            return result;
        }

        public static string DropPrefix(this string value, string prefix) {
            var retVal = value;
            if (value != null) {
                if (retVal.IndexOf(prefix, StringComparison.Ordinal) == 0) {
                    retVal = value.Substring(prefix.Length);
                }
            }
            return retVal;
        }

      

    }

}