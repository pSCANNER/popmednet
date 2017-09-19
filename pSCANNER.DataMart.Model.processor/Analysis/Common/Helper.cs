using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pSCANNER.DataMart.Model.processor.Analysis.Common {

    /// <summary>
    /// </summary>
    public static class Helper {

        /// <summary>
        ///     Determines whether this instance is empty.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the specified value is empty; otherwise, <c>false</c>.</returns>
        public static bool IsEmpty(this String value) {
            bool retVal = false;
            retVal |= value == null;
            if (retVal == false) {
                retVal |= String.IsNullOrWhiteSpace(value);
            }
            return retVal;
        }
    }
}