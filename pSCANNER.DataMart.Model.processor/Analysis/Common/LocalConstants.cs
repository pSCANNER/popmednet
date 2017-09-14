using System.ComponentModel;

/// <summary>
/// </summary>
namespace Lpp.Scanner.DataMart.Model.Processors.Analysis.Common {

    /// <summary>
    /// </summary>
    public enum ConnectAs {

        /// <summary>
        ///     The HTTP
        /// </summary>
        [Description("HttpDataSetConnection")]
        HTTP,

        /// <summary>
        ///     The SQL data base
        /// </summary>
        [Description("DatabaseDataSetConnection")]
        SqlDataBase,

        [Description("")]
        Undefined
    }

    /// <summary>
    /// </summary>
    public enum LocalConstants {

        /// <summary>
        ///     The coefficients
        /// </summary>
        [Description("coefficients")]
        Coefficients,

        /// <summary>
        ///     The data set
        /// </summary>
        [Description("dataSet")]
        DataSet,

        /// <summary>
        ///     The epsilon
        /// </summary>
        [Description("epsilon")]
        Epsilon,

        /// <summary>
        ///     The epsilon value
        /// </summary>
        [Description(".0000008")]
        EpsilonValue,

        /// <summary>
        ///     The family name
        /// </summary>
        [Description("functionName")]
        FamilyName,

        /// <summary>
        ///     The link function name
        /// </summary>
        [Description("linkFunction")]
        LinkFunctionName,

        /// <summary>
        ///     The maxit
        /// </summary>
        [Description("1")]
        Maxit,

        /// <summary>
        ///     The parameter list name
        /// </summary>
        [Description("parameterList")]
        ParameterListName
    }

    /// <summary>
    /// </summary>
    public static class ConstExtensions {

        /// <summary>
        ///     Gets the value.
        /// </summary>
        /// <param name="val">The value.</param>
        /// <returns></returns>
        public static string GetValue(this LocalConstants val) {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val.GetType().GetField(val.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }

        /// <summary>
        ///     Gets the value.
        /// </summary>
        /// <param name="val">The value.</param>
        /// <returns></returns>
        public static string GetValue(this ConnectAs val) {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val.GetType().GetField(val.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }
}