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

    public static class ScriptConstants {
        public const string AggregationCountConst = "aggregationCount";
        public const string CoefficientStringSuffixConst = "PMML.GeneralRegressionModel.Coefficients.";
        public const string FeaturelistConst = "FeatureList";
        public const string IterationConst = "Iteration";
        public const string IterationTypeConst = "PMML.Header.Extension.Type";
        public const string MaxAggrCountConst = "maxAggrCount";
        public const string RegExString = @"(?:<pScannerResult\s+name=""{0}"">[\r\n]+\s+\[,1\]\r\n(?:(?-x)\[\d*,\]\s+(?<coefficient>\-?\d+(?:\.\d+)?)[\r\n]+)*</pScannerResult>)";
        public const string StopIterationConst = "StopIteration";
        public static readonly VariableEntry _beta0 = new VariableEntry { VariableName = "beta0" };

        public static readonly VariableEntry _beta1 = new VariableEntry { VariableName = "beta1" };

        public static readonly VariableEntry _columnCount = new VariableEntry { VariableName = "columnCount", DefaultValue = "0.0000001" };
        public static readonly VariableEntry _covariateMatrix = new VariableEntry { VariableName = "covariateMatrix" };

        public static readonly VariableEntry _deviationMatrix = new VariableEntry { VariableName = "deviation", DefaultValue = "10^(-20)" };
        public static readonly VariableEntry _epsilon = new VariableEntry { VariableName = "epsilon", DefaultValue = "10^(-6)" };
        public static readonly VariableEntry _errorMatrix = new VariableEntry { VariableName = "error", DefaultValue = "10^(-20)" };

        //private static readonly Regex _regex = new Regex("\\[\\d+,\\]\\s+(?<value>-?\\d*(?:\\.\\d+(?:e\\+\\d\\d)?)?)", RegexOptions.CultureInvariant | RegexOptions.Compiled);
        public static readonly Regex _featureListRegularExpression = new Regex("\"(?<value>.+?)\"", RegexOptions.CultureInvariant | RegexOptions.Compiled);

        public static readonly VariableEntry _kServer = new VariableEntry { VariableName = "kServer" };
        public static readonly VariableEntry _siteCount = new VariableEntry { VariableName = "siteCount" };
        public static readonly VariableEntry _standardDeviation = new VariableEntry { VariableName = "stdDev" };
    }
}