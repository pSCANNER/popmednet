#region Legal Information

// ====================================================================================
//
// Center for Population Health Informatics
// Solution: Lpp.Adapters
// Project: Lpp.Scanner.DataMart.Model.Processors Last Updated By: Westerman, Dax Marek
//
// ====================================================================================

#endregion Legal Information

#region Using

using System.Text.RegularExpressions;

#endregion Using

namespace Lpp.Scanner.DataMart.Model.Processors.Aggregation.Common {

    /// <summary>
    /// </summary>
    public static class ScriptConstants {

        /// <summary>
        ///     The aggregation count constant
        /// </summary>
        public const string AggregationCountConst = "aggregationCount";

        /// <summary>
        ///     The coefficient string suffix constant
        /// </summary>
        public const string CoefficientStringSuffixConst = "PMML.GeneralRegressionModel.Coefficients.";

        /// <summary>
        ///     The featurelist constant
        /// </summary>
        public const string FeaturelistConst = "FeatureList";

        /// <summary>
        ///     The iteration constant
        /// </summary>
        public const string IterationConst = "Iteration";

        /// <summary>
        ///     The iteration type constant
        /// </summary>
        public const string IterationTypeConst = "PMML.Header.Extension.Type";

        /// <summary>
        ///     The maximum aggr count constant
        /// </summary>
        public const string MaxAggrCountConst = "maxAggrCount";

        /// <summary>
        ///     The reg ex string
        /// </summary>
        public const string RegExString = @"(?:<pScannerResult\s+name=""{0}"">[\r\n]+\s+\[,1\]\r\n(?:(?-x)\[\d*,\]\s+(?<coefficient>\-?\d+(?:\.\d+)?)[\r\n]+)*</pScannerResult>)";

        /// <summary>
        ///     The stop iteration constant
        /// </summary>
        public const string StopIterationConst = "StopIteration";

        /// <summary>
        ///     The beta0
        /// </summary>
        public static readonly VariableEntry _beta0 = new VariableEntry { VariableName = "beta0" };

        /// <summary>
        ///     The beta1
        /// </summary>
        public static readonly VariableEntry _beta1 = new VariableEntry { VariableName = "beta1" };

        /// <summary>
        ///     The column count
        /// </summary>
        public static readonly VariableEntry _columnCount = new VariableEntry { VariableName = "columnCount", DefaultValue = "0.0000001" };

        /// <summary>
        ///     The covariate matrix
        /// </summary>
        public static readonly VariableEntry _covariateMatrix = new VariableEntry { VariableName = "covariateMatrix" };

        /// <summary>
        ///     The deviation matrix
        /// </summary>
        public static readonly VariableEntry _deviationMatrix = new VariableEntry { VariableName = "deviation", DefaultValue = "10^(-20)" };

        /// <summary>
        ///     The epsilon
        /// </summary>
        public static readonly VariableEntry _epsilon = new VariableEntry { VariableName = "epsilon", DefaultValue = "10^(-6)" };

        /// <summary>
        ///     The error matrix
        /// </summary>
        public static readonly VariableEntry _errorMatrix = new VariableEntry { VariableName = "error", DefaultValue = "10^(-20)" };

        //private static readonly Regex _regex = new Regex("\\[\\d+,\\]\\s+(?<value>-?\\d*(?:\\.\\d+(?:e\\+\\d\\d)?)?)", RegexOptions.CultureInvariant | RegexOptions.Compiled);
        /// <summary>
        ///     The feature list regular expression
        /// </summary>
        public static readonly Regex _featureListRegularExpression = new Regex("\"(?<value>.+?)\"", RegexOptions.CultureInvariant | RegexOptions.Compiled);

        /// <summary>
        ///     The k server
        /// </summary>
        public static readonly VariableEntry _kServer = new VariableEntry { VariableName = "kServer" };

        /// <summary>
        ///     The site count
        /// </summary>
        public static readonly VariableEntry _siteCount = new VariableEntry { VariableName = "siteCount" };

        /// <summary>
        ///     The standard deviation
        /// </summary>
        public static readonly VariableEntry _standardDeviation = new VariableEntry { VariableName = "stdDev" };
    }
}