using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Lpp.Scanner.DataMart.Model.Processors.Common.Base;
using Lpp.Scanner.DataMart.Model.Processors.Common.ExtMethods;
using Lpp.Scanner.DataMart.Model.Processors.Common.Models;
using Lpp.Scanner.DataMart.Model.Processors.Common.Pmml;
using Lpp.Scanner.DataMart.Model.Processors.Common.R;


namespace Lpp.Scanner.DataMart.Model.Processors.Aggregation
{

    public static class RScriptWriter
    {

        /// <summary>
        ///     Sets the feature number.
        /// </summary>
        /// <param name="scriptBuilder">The script builder.</param>
        /// <param name="numberFeatures"></param>
        /// <exception cref="System.NotImplementedException"></exception>
        private static void setFeatureNumber(BaseScriptBuilder scriptBuilder, int numberFeatures)
        {
            scriptBuilder.AddLine("{1} <- {0}", numberFeatures.ToString(), ScriptConstants._columnCount.ToString());
        }

        /// <summary>
        ///     Generates the r script from p cov matrices.
        /// </summary>
        /// <param name="pCovMatricesPerSite">The p cov matrices per site.</param>
        /// <param name="errorGradientPerSite">The error gradient per site.</param>
        /// <param name="coefficients">The coefficients.</param>
        /// <param name="dataFields">The data fields.</param>
        /// <param name="aggregationCount"></param>
        /// <param name="maxAggregationCount"></param>
        /// <returns></returns>
        /// <exception cref="System.NullReferenceException">pCovMatricesPerSite</exception>
        public static string GenerateRScript(IDictionary<string, PCovMatrix> pCovMatricesPerSite, IDictionary<string, ErrorGradientModel> errorGradientPerSite, Coefficients coefficients, IEnumerable<string> dataFields, int aggregationCount, int maxAggregationCount) {

            if (pCovMatricesPerSite == null) {
                throw new NullReferenceException("pCovMatricesPerSite");
            }

            var firstMatrix = pCovMatricesPerSite.FirstOrDefault();

            var features = firstMatrix.Value.PCovCells.Select(x => x.Row).Distinct().ToArray();

            var numberFeatures = features.Length;

            var rScriptBuilder = new RScriptBuilder("Aggregator - Processing Code");

            rScriptBuilder.AddLine(string.Format("{0} <- {1}", ScriptConstants.AggregationCountConst, aggregationCount));
            rScriptBuilder.AddLine(string.Format("{0} <- {1}", ScriptConstants.MaxAggrCountConst, maxAggregationCount));

            setFeatureNumber(rScriptBuilder, numberFeatures);

            rScriptBuilder.AddLine("{0} <- {1}", ScriptConstants._epsilon.ToString(), ScriptConstants._epsilon.DefaultValue);

            rScriptBuilder.AddLine("{0} <- matrix(c({1}), {2}, 1)", ScriptConstants._beta0.ToString(), coefficients.GetCoefficientListString(), ScriptConstants._columnCount.ToString());

            //setBeta1(scriptBuilder);

            rScriptBuilder.AddLine("{0} <- {1}", ScriptConstants._siteCount.ToString(), pCovMatricesPerSite.Count.ToString());

            initCovarianceMatrix(rScriptBuilder, pCovMatricesPerSite);

            initGradientError(rScriptBuilder, errorGradientPerSite);

            //initKServer(scriptBuilder);

            initSdTrue(rScriptBuilder);

            calculateNewCoefficients(rScriptBuilder);

            writeOutFeatures(rScriptBuilder, features, dataFields);

            return rScriptBuilder.Text;
        }

        /// <summary>
        /// </summary>
        /// <seealso cref="Lpp.Scanner.DataMart.Model.Processors.Common.Base.ProxyBase" />
        private class StringListComparer : IEqualityComparer<string>
        {
            #region Interface Implementation

            /// <summary>
            ///     Determines whether the specified objects are equal.
            /// </summary>
            /// <param name="x">The first object of type <paramref name="T" /> to compare.</param>
            /// <param name="y">The second object of type <paramref name="T" /> to compare.</param>
            /// <returns>
            ///     true if the specified objects are equal; otherwise, false.
            /// </returns>
            public bool Equals(string x, string y)
            {
                return x.Equals(y, StringComparison.InvariantCultureIgnoreCase);
            }

            /// <summary>
            ///     Returns a hash code for this instance.
            /// </summary>
            /// <param name="obj">The object.</param>
            /// <returns>
            ///     A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
            /// </returns>
            public int GetHashCode(string obj)
            {
                return 1;
            }

            #endregion
        }

        /// <summary>
        ///     Writes the out features.
        /// </summary>
        /// <param name="scriptBuilder">The script builder.</param>
        /// <param name="features">The features.</param>
        /// <param name="dataFields">The data fields.</param>
        private static void writeOutFeatures(RScriptBuilder scriptBuilder, string[] features, IEnumerable<string> dataFields)
        {
            IEqualityComparer<string> comparer = new StringListComparer();
            var enumerable = features.Select(x => x.DropPrefix("x")).ToList();
            var intersect = enumerable.Intersect(dataFields, comparer).ToList();
            var @select = intersect.Select(x => string.Format("\"{0}\"", x)).ToList();
            var values = string.Join(", ", @select);
            var featureList = string.Format("c({0})", values);

            scriptBuilder.AddResponseLine(featureList, ScriptConstants.FeaturelistConst);
        }

        /// <summary>
        ///     Calculates the new coefficients.
        /// </summary>
        /// <param name="scriptBuilder">The script builder.</param>
        private static void calculateNewCoefficients(RScriptBuilder scriptBuilder)
        {

            scriptBuilder.AddLine("     {1} <- {0} + solve(rowSums({2}, dims=2) + diag({5}, {3})) %*% (rowSums({4}, dims=2))", ScriptConstants._beta0, ScriptConstants._beta1, ScriptConstants._deviationMatrix, ScriptConstants._columnCount, ScriptConstants._errorMatrix, ScriptConstants._columnCount.DefaultValue);
            scriptBuilder.AddLine("     if(({3} >= {4}) || mean(abs({1} - {0})) < {2})", ScriptConstants._beta0, ScriptConstants._beta1, ScriptConstants._epsilon, ScriptConstants.AggregationCountConst, ScriptConstants.MaxAggrCountConst);
            scriptBuilder.AddLine("     {");
            scriptBuilder.AddLine("         {0} <- solve(rowSums({1}, dims=2) + diag({2}, {3}))", ScriptConstants._covariateMatrix, ScriptConstants._deviationMatrix, ScriptConstants._columnCount.DefaultValue, ScriptConstants._columnCount);
            scriptBuilder.AddLine("         {0} <- sqrt(diag({1}))", ScriptConstants._standardDeviation, ScriptConstants._covariateMatrix);
            scriptBuilder.AddLine("         {0} <- NULL", ScriptConstants._beta0);
            scriptBuilder.AddLine("     }");
            scriptBuilder.AddResponseLine(ScriptConstants._beta0.VariableDescription, ScriptConstants._beta0.ToString());
            scriptBuilder.AddResponseLine(ScriptConstants._beta1.VariableDescription, ScriptConstants._beta1.ToString());
            scriptBuilder.AddResponseLine(ScriptConstants._standardDeviation.VariableDescription, ScriptConstants._standardDeviation.ToString());

        }

        /// <summary>
        ///     Initializes the sd true.
        /// </summary>
        /// <param name="scriptBuilder">The script builder.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        private static void initSdTrue(BaseScriptBuilder scriptBuilder)
        {
            scriptBuilder.AddLine("{0} <- 0", ScriptConstants._standardDeviation.ToString());
        }

        ///// <summary>
        /////     Initializes the k server.
        ///// </summary>
        ///// <param name="scriptBuilder">The script builder.</param>
        ///// <exception cref="System.NotImplementedException"></exception>
        //private static void initKServer(BaseScriptBuilder scriptBuilder) {
        //    scriptBuilder.AddLine("{0} <- 0", _kServer.ToString());
        //}

        /// <summary>
        ///     Initializes the gradient error.
        /// </summary>
        /// <param name="scriptBuilder">The script builder.</param>
        /// <param name="errorGradientPerSite"></param>
        /// <exception cref="System.NotImplementedException"></exception>
        private static void initGradientError(BaseScriptBuilder scriptBuilder, IDictionary<string, ErrorGradientModel> errorGradientPerSite)
        {
            scriptBuilder.AddLine("{0} <- array({1}, c({2}, 1, {3}))", ScriptConstants._errorMatrix.ToString(), ScriptConstants._errorMatrix.DefaultValue, ScriptConstants._columnCount.ToString(), ScriptConstants._siteCount.ToString());

            var siteMap = new Dictionary<string, Dictionary<string, string>>();

            foreach (var errorGradientPerSiteKey in errorGradientPerSite.Keys)
            {
                var map = new Dictionary<string, string>();
                siteMap.Add(errorGradientPerSiteKey, map);
                var errorGradient = errorGradientPerSite[errorGradientPerSiteKey];
                foreach (var entry in errorGradient.Values)
                {
                    map.Add(entry.Key, entry.Value.ToString(CultureInfo.InvariantCulture));
                }
            }

            var siteIndex = 1;

            foreach (var siteMapKey in siteMap.Keys)
            {
                var entry = siteMap[siteMapKey];
                var rowIndex = 1;
                foreach (var rowKey in entry.Keys)
                {
                    scriptBuilder.AddLine("{3}[{0}, 1, {1}] <- {2}", rowIndex.ToString(), siteIndex.ToString(), entry[rowKey], ScriptConstants._errorMatrix);
                    rowIndex++;
                }
                siteIndex++;
            }

        }

        /// <summary>
        ///     Initializes the covariance matrix.
        /// </summary>
        /// <param name="scriptBuilder">The script builder.</param>
        /// <param name="pCovMatricesPerSite">The p cov matrices per site.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        private static void initCovarianceMatrix(BaseScriptBuilder scriptBuilder, IDictionary<string, PCovMatrix> pCovMatricesPerSite)
        {
            scriptBuilder.AddLine("{0} <- array({1}, c({2}, {2}, {3}))", ScriptConstants._deviationMatrix.ToString(), ScriptConstants._deviationMatrix.DefaultValue, ScriptConstants._columnCount.ToString(), ScriptConstants._siteCount.ToString());

            //BaseError

            var siteMap = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();

            foreach (var pCovMatrix in pCovMatricesPerSite)
            {
                var map = new Dictionary<string, Dictionary<string, string>>();
                siteMap.Add(pCovMatrix.Key, map);
                foreach (var pCovCell in pCovMatrix.Value.PCovCells)
                {
                    var pRow = pCovCell.Row;
                    var pCol = pCovCell.Column;
                    var value = pCovCell.Value;

                    if (map.ContainsKey(pRow) == false)
                    {
                        map.Add(pRow, new Dictionary<string, string>());
                    }
                    var dictionary = map[pRow];
                    if (dictionary.ContainsKey(pCol) == false)
                    {
                        dictionary.Add(pCol, value);
                    }
                }
            }

            var siteIndex = 1;

            foreach (var siteKey in siteMap.Keys)
            {
                var siteMatrix = siteMap[siteKey];
                var rowIndex = 1;
                foreach (var rowKey in siteMatrix.Keys)
                {
                    var row = siteMatrix[rowKey];
                    var columnIndex = 1;
                    foreach (var columnKey in row.Keys)
                    {
                        var entry = row[columnKey];
                        scriptBuilder.AddLine("{4}[{0}, {1}, {2}] <- {3}", rowIndex.ToString(), columnIndex.ToString(), siteIndex.ToString(), entry, ScriptConstants._deviationMatrix);
                        columnIndex++;
                    }
                    rowIndex++;
                }
                siteIndex++;
            }

        }

    }
}
