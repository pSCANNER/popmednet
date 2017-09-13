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
using System.Linq;
using System.Text.RegularExpressions;
using Lpp.Scanner.DataMart.Model.Processors.Common.ExtMethods;
using Lpp.Scanner.DataMart.Model.Processors.Common.R;
using LinqKit;


#endregion

namespace Lpp.Scanner.DataMart.Model.Processors.Common.Models {

    /// <summary>
    /// </summary>
    public class ErrorAndCovarianceModel {

        public const string GlmConvergenceWarningConst = "GLM Convergence Warning";
        public const string ErrorGradientConst = "Error/Gradient";
        public const string CovarianceUnscaledConst = "Covariance - Unscaled";
        public const string CovarianceScaledConst = "Covariance - Scaled";

        private static readonly Func<string, string> _trimIntercept = x => x.Replace("(", string.Empty).Replace(")", string.Empty);

        #region Properties

        /// <summary>
        ///     Gets or sets the convergence warning.
        /// </summary>
        /// <value>
        ///     The convergence warning.
        /// </value>
        public ConvergenceWarningModel ConvergenceWarning { get; set; }

        /// <summary>
        ///     Gets or sets the error gradient.
        /// </summary>
        /// <value>
        ///     The error gradient.
        /// </value>
        public ErrorGradientModel ErrorGradient { get; set; }

        /// <summary>
        ///     Gets or sets the scaled covariance.
        /// </summary>
        /// <value>
        ///     The scaled covariance.
        /// </value>
        public CovarianceModel ScaledCovariance { get; set; }

        /// <summary>
        ///     Gets or sets the unscaled covariance.
        /// </summary>
        /// <value>
        ///     The unscaled covariance.
        /// </value>
        public CovarianceModel UnscaledCovariance { get; set; }

        #endregion

        /// <summary>
        ///     Creates the specified r response.
        /// </summary>
        /// <param name="rResponse">The r response.</param>
        /// <returns></returns>
        public static ErrorAndCovarianceModel Create(string rResponse) {

            var model = new ErrorAndCovarianceModel();

            var results = RResultParser.Parse(rResponse);

            model.ConvergenceWarning = getConvergenceWarning(results, GlmConvergenceWarningConst);
            model.ErrorGradient = getErrorGradient(results);
            model.UnscaledCovariance = getUnscaledCovariance(results);
            model.ScaledCovariance = getScaledCovariance(results);

            return model;
        }

        /// <summary>
        ///     Gets the scaled covariance.
        /// </summary>
        /// <param name="dict">The r response.</param>
        /// <returns></returns>
        private static CovarianceModel getScaledCovariance(Dictionary<string, string> dict) {
            //<pScannerResult name="Covariance - Unscaled">
            //                (Intercept)          xgre          xgpa         xrank
            //(Intercept)  1.8233933177 -4.532666e-04 -0.4085565134 -4.988367e-02
            //xgre        -0.0004532666  1.665428e-06 -0.0001714873  1.185798e-05
            //xgpa        -0.4085565134 -1.714873e-04  0.1492857547 -2.311472e-03
            //xrank       -0.0498836662  1.185798e-05 -0.0023114718  2.362034e-02
            //</pScannerResult>

            return getCovariance(dict, CovarianceScaledConst);
        }

        /// <summary>
        ///     Gets the unscaled covariance.
        /// </summary>
        /// <param name="dict">The r response.</param>
        /// <returns></returns>
        private static CovarianceModel getUnscaledCovariance(Dictionary<string, string> dict) {
            //<pScannerResult name="Covariance - Scaled">k
            //              (Intercept)          xgre          xgpa         xrank
            //(Intercept)  1.8233933177 -4.532666e-04 -0.4085565134 -4.988367e-02
            //xgre        -0.0004532666  1.665428e-06 -0.0001714873  1.185798e-05
            //xgpa        -0.4085565134 -1.714873e-04  0.1492857547 -2.311472e-03
            //xrank       -0.0498836662  1.185798e-05 -0.0023114718  2.362034e-02
            //</pScannerResult>

            return getCovariance(dict, CovarianceUnscaledConst);

        }

        /// <summary>
        /// Gets the covariance.
        /// </summary>
        /// <param name="dict">The dictionary.</param>
        /// <param name="rResponseX">The r response x.</param>
        /// <returns></returns>
        private static CovarianceModel getCovariance(Dictionary<string, string> dict, string rResponseX) {
            var model = new CovarianceModel();

            if (dict.ContainsKey(rResponseX)) {

                Regex splitRegex = new Regex("(.*)\\n");
                var matchCollection = splitRegex.Matches(dict[rResponseX]);
                Match[] matches = new Match[matchCollection.Count];
                matchCollection.CopyTo(matches, 0);

                List<Match> lines = matches.Where(x => string.IsNullOrWhiteSpace(x.Captures[0].Value) == false).ToList();

                var enumerable = lines[0].Captures[0].Value.Split(new char[0], StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim());
                var dictionary = enumerable.ToDictionary(x => _trimIntercept(x).DropPrefix("x"), x => new List<double>());
                model.Set(dictionary);

                var next_lines = lines.Skip(1);

                foreach (var next_line in next_lines) {
                    var entries = next_line.Captures[0].Value.Split(new char[0], StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim());
                    var rows = entries.ToArray();
                    var name = _trimIntercept(rows[0]);
                    model.Entries.Set(name.DropPrefix("x"), rows.Skip(1).Select(double.Parse).ToList());
                }



            }

            return model;
        }

        /// <summary>
        /// Gets the error gradient.
        /// </summary>
        /// <param name="dict">The dictionary.</param>
        /// <returns></returns>
        private static ErrorGradientModel getErrorGradient(Dictionary<string, string> dict) {
            var model = new ErrorGradientModel();

            if (dict.ContainsKey(ErrorGradientConst)) {

                var rResponse = dict[ErrorGradientConst];

                rResponse = rResponse.Substring(rResponse.IndexOf("(Intercept)", StringComparison.Ordinal));

                var lines = rResponse.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var line in lines) {
                    var strings = line.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
                    model.Values.Add(_trimIntercept(strings[0]), strings[1]);
                }




            }
            return model;
        }

        /// <summary>
        ///     Gets the convergence warning.
        /// </summary>
        /// <param name="rResponse">The r response.</param>
        /// <returns></returns>
        private static ConvergenceWarningModel getConvergenceWarning(Dictionary<string, string> results, string key) {

            //<pScannerResult name="GLM Convergence Warning">
            //Warning message:
            //glm.fit: algorithm did not converge
            //</pScannerResult>

            var convergenceWarningModel = results.ContainsKey(key) ? new ConvergenceWarningModel { CannotConverge = results[key].Contains("glm.fit: algorithm did not converge") == false } : null;

            return convergenceWarningModel;
        }

    }

}