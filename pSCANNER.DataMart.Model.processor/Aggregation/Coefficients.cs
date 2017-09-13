using System;
using System.Collections.Generic;
using System.Linq;
using Lpp.Scanner.DataMart.Model.Processors.Common.ExtMethods;


namespace Lpp.Scanner.DataMart.Model.Processors.Aggregation {

    public class Coefficients {
        #region Fields

        readonly IList<KeyValuePair<string, string>> _list = new List<KeyValuePair<string, string>>();

        /// <summary>
        /// Gets the target value.
        /// </summary>
        /// <value>
        /// The target value.
        /// </value>
        public string TargetValue { get; private set; }

        #endregion

        /// <summary>
        ///     Adds the specified variable name.
        /// </summary>
        /// <param name="varName">Name of the variable.</param>
        /// <param name="varValue">The variable value.</param>
        public void Add(string varName, string varValue) {
            var keyValuePair = new KeyValuePair<string, string>(varName, varValue);
            _list.Add(keyValuePair);
        }

        /// <summary>
        ///     Gets the coefficient list string.
        /// </summary>
        /// <returns></returns>
        public string GetCoefficientListString() {
            return _list
                .Where(x =>
                {
                    return x.Key.Equals(TargetValue, StringComparison.InvariantCultureIgnoreCase) == false;

                })
                .Select(x => x.Value)
                
                .Join(",");
        }

        /// <summary>
        /// Sets the target.
        /// </summary>
        /// <param name="targetValue">The target value.</param>
        public void setTarget(string targetValue) {
            TargetValue = targetValue;
        }

    }

}