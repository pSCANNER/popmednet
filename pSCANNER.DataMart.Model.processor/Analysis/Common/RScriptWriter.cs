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

using Lpp.Scanner.DataMart.Model.Processors.Common.Base;
using Lpp.Scanner.DataMart.Model.Processors.Common.Exceptions;
using Lpp.Scanner.DataMart.Model.Processors.Common.Models;
using Lpp.Scanner.DataMart.Model.Processors.Common.Parameters;
using Lpp.Scanner.DataMart.Model.Processors.Common.R;
using Lpp.Scanner.DataMart.Model.Processors.DataSetMapping;
using pSCANNER.DataMart.Model.processor.DataSetMapping;
using pSCANNER.DataMart.Model.processor.DataSetMapping.Configuration;
using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Lpp.Scanner.DataMart.Model.Processors.Common.Pmml;

#endregion Using

namespace Lpp.Scanner.DataMart.Model.Processors.Analysis.Common {

    public static class RScriptWriter {

        /// <summary>
        ///     Generates the r script.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public static string GenerateRScript(AggregatorClientRequestParameter parameter) {
            string ret_val = string.Empty;

            GlmParameters parameters = glmHeader(parameter);

            var ppMatrix = parameter.Pmml.GetPPMatrix();
            var paramMatrix = parameter.Pmml.GetParamMatrix();
            var result = paramMatrix.PCells.Select(x => new { Key = parameters.RequestParameters.Parameters[x.Key], x.Value }).ToDictionary(x => x.Key, x => x.Value);
            var ppCell = ppMatrix.PPCells.First(x => x.Value.PredictorName == parameters.MiningFields.DependantVariable);
            var pCells = result.Values.Join(parameters.RequestParameters.Parameters, cell => cell.ParameterName, x => x.Key, (cell, pair) => cell);
            var t = parameters.MiningFields.IndependantVariables.ToDictionary(x => x, x => x);
            var enumerable = pCells.Where(x => x.ParameterName != ppCell.Value.ParameterName).ToList();
            foreach (var pCell in enumerable.Join(ppMatrix.PPCells, cell => cell.ParameterName, pair => pair.Value.ParameterName, (cell, pair) => new { cell, pair }).ToArray()) {
                if (pCell.pair.Value.PredictorName != VariableConstants.InterceptConst) {
                    if (t.ContainsKey(pCell.pair.Value.PredictorName) == false) {
                        enumerable.Remove(pCell.cell);
                    }
                }
            }

            var coefficientValues = String.Join(", ", enumerable.OrderBy(x => x.ParameterName).Select(x => x.Value.ToString()));

            ret_val = BuildScript(parameter, parameters.Family, parameters.MiningFields, parameters.IndependentVariables, coefficientValues);

            return ret_val;
        }

        internal static string GenerateRScript(AnalysticsRequestParameter parameter) {
            string ret_val = string.Empty;
            GlmParameters parameters = glmHeader(parameter);
            ret_val = BuildScript(parameter, parameters.Family, parameters.MiningFields, parameters.IndependentVariables);
            return ret_val;
        }

        private static string BuildScript(AnalysticsRequestParameter parameter, string family, MiningFields miningFields, string[] independentVariables) {
            IRegistry registry = new pSCANNER_R_Registry();
            var rSection = registry.getRSection(TypeIs.Analytics);

            rSection = HttpUtility.HtmlDecode(rSection);

            var rScriptBuilder = new RScriptBuilder("Analytics - Site Code");
            string script = @"

            confint(glmObj)
            glmObj <- glm(%s ~ %s, data=%s, family='%s')
            paste(capture.output({pmml(glmObj, copyright='DAN')}), collapse='\\n')
            paste(capture.output({%s}), collapse='\\n')
            ";

            return rScriptBuilder.Text;
        }

        /// <summary>
        ///     Builds the script.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="family">The family.</param>
        /// <param name="miningFields">The mining fields.</param>
        /// <param name="independentVariables">The independent variables.</param>
        /// <param name="coefficientValues">The coefficient values.</param>
        /// <returns></returns>
        private static string BuildScript(AggregatorClientRequestParameter parameter, string family, MiningFields miningFields, string[] independentVariables, string coefficientValues) {
            IRegistry registry = new pSCANNER_R_Registry();
            var rSection = registry.getRSection(TypeIs.Aggregator);

            rSection = HttpUtility.HtmlDecode(rSection);

            var rScriptBuilder = new RScriptBuilder("Aggregator - Site Code");

            rScriptBuilder.AddLine(@"{0} <- ""{1}""", LocalConstants.FamilyName, family);

            rScriptBuilder.AddRequirement("glmnet");

            rScriptBuilder.AddLine(CreateDataConnectionString(parameter.DataSetConnection.GetType(), parameter.DataSetConnection));

            rScriptBuilder.AddLine(@"y <- {1}[, ""{0}""]", miningFields.DependantVariable, LocalConstants.DataSet);

            rScriptBuilder.AddLine("x <- as.matrix({0}[, c({1})]);", LocalConstants.DataSet, independentVariables);

            rScriptBuilder.AddLine("{0} <- c({1});", LocalConstants.Coefficients, coefficientValues);

            rScriptBuilder.AddLine("{2} = list({0} = {1}, maxit = {3}, trace = FALSE);", LocalConstants.Epsilon, LocalConstants.EpsilonValue, LocalConstants.ParameterListName, LocalConstants.Maxit);

            rScriptBuilder.AddLine("glm.site <- glm(y ~ {0}, start = {1},  family = {2}, control = {3});", String.Join(" + ", LocalConstants.DataSet + "$" + independentVariables), LocalConstants.Coefficients, family, LocalConstants.ParameterListName);

            rScriptBuilder.AddResponseLine("warnings()", ErrorAndCovarianceModel.GlmConvergenceWarningConst);

            if (string.IsNullOrWhiteSpace(rSection)) {
                rScriptBuilder.AddLine("glm.sitedetail <- summary(glm.site);");
                rScriptBuilder.AddLine("x_ <- model.matrix(glm.site$terms);");
                rScriptBuilder.AddLine("p1 <- coefficients%*%t(x_)");
                rScriptBuilder.AddLine("p <- family(glm.s)$linkinv(eta =p1)");
                rScriptBuilder.AddLine("w <- diag(c(p*(1-p)));");
                rScriptBuilder.AddLine("glm.sitedetail$gradient <- t(x_)%*%(y-p);");
                rScriptBuilder.AddLine("glm.sitedetail$cov.new <- t(x_)%*%w%*%x_;");
            } else {
                rScriptBuilder.AddLine(rSection);
            }

            rScriptBuilder.AddResponseLine("glm.sitedetail$gradient;", ErrorAndCovarianceModel.ErrorGradientConst);

            rScriptBuilder.AddResponseLine("glm.sitedetail$cov.new;", ErrorAndCovarianceModel.CovarianceUnscaledConst);

            rScriptBuilder.AddResponseLine("glm.sitedetail$cov.scaled;", ErrorAndCovarianceModel.CovarianceScaledConst);

            return rScriptBuilder.Text;
        }

        /// <summary>
        ///     Creates the data connection string.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="connection">The connection.</param>
        /// <returns></returns>
        /// <exception cref="UnexpectedSwitchValue"></exception>
        private static string CreateDataConnectionString(Type type, IDataSetConnection connection) {
            string readString;

            ConnectAs connectAs = ConnectAs.Undefined;

            if (type.Name == ConnectAs.HTTP.GetValue()) {
                connectAs = ConnectAs.HTTP;
            } else if (type.Name == ConnectAs.SqlDataBase.GetValue()) {
                connectAs = ConnectAs.SqlDataBase;
            }

            switch (connectAs) {
                case ConnectAs.HTTP: {
                    readString = string.Format("{1} <- as.matrix(read.csv(\"{0}\"))", connection.Uri, LocalConstants.DataSet);
                    break;
                }
                case ConnectAs.SqlDataBase: {
                    var sb = new StringBuilder();
                    sb.AppendLine("# BEGIN Database Read");
                    sb.AppendLine("if (!require(\"RODBC\")) {");
                    sb.AppendLine("install.packages(\"RODBC\", repos=\"http://cran.rstudio.com/\", dependencies=TRUE);");
                    sb.AppendLine("library(RODBC);");
                    sb.AppendLine("}");
                    sb.AppendLine("");
                    var connectionString = connection.Uri;

                    var match = _queryMatch.Match(connectionString);
                    if (match.Success) {
                        var query = match.Groups["query"].Captures[0].Value;
                        sb.AppendLine(string.Format("dbhandle <- odbcDriverConnect(\'driver={{SQL Server}};{0}\')", connectionString));
                        sb.AppendLine(string.Format("{0} <- sqlQuery(dbhandle, \'{1}\')", LocalConstants.DataSet, query));
                    }

                    sb.AppendLine("# END Database Read");
                    readString = sb.ToString();
                    break;
                }
                default: {
                    throw new UnexpectedSwitchValue(type.Name);
                }
            }
            return readString;
        }

        private static GlmParameters glmHeader(IRExecution parameter) {
            GlmParameters parameters = null;
            parameters.RequestParameterList = parameter.Pmml.GetRequestParameterList();
            parameters.RequestParameters = new RequestParameterList { Parameters = parameters.RequestParameterList.RequestParameters };
            parameters.LinkFunction = parameter.Pmml.GetLinkFunction(LocalConstants.LinkFunctionName.GetValue());
            parameters.Family = parameter.Pmml.GetFamily(LocalConstants.FamilyName.GetValue());
            parameters.MiningFields = new MiningFields { DependantVariable = parameter.Pmml.GetMiningSchema().DependentVariable, IndependantVariables = parameter.Pmml.GetMiningSchema().IndependantVariables };
            parameters.IndependentVariables = parameters.MiningFields.IndependantVariables.Select(x => String.Format(@"""{0}""", x)).ToArray();
            return parameters;
        }

        private static readonly Regex _queryMatch = new Regex(@"Query=(?<query>[\p{L}\s\*\[\]\._\d]+)(?:;|$)", RegexOptions.Compiled);

        /// <summary>
        /// </summary>
        private class GlmParameters {

            /// <summary>
            ///     Gets or sets the family.
            /// </summary>
            /// <value>The family.</value>
            public string Family { get; internal set; }

            /// <summary>
            ///     Gets or sets the independent variables.
            /// </summary>
            /// <value>The independent variables.</value>
            public string[] IndependentVariables { get; internal set; }

            /// <summary>
            ///     Gets or sets the link function.
            /// </summary>
            /// <value>The link function.</value>
            public string LinkFunction { get; internal set; }

            /// <summary>
            ///     Gets or sets the mining fields.
            /// </summary>
            /// <value>The mining fields.</value>
            public MiningFields MiningFields { get; internal set; }

            /// <summary>
            ///     Gets or sets the request parameter list.
            /// </summary>
            /// <value>The request parameter list.</value>
            public PmmlRequestParameterList RequestParameterList { get; internal set; }

            /// <summary>
            ///     Gets or sets the request parameters.
            /// </summary>
            /// <value>The request parameters.</value>
            public RequestParameterList RequestParameters { get; internal set; }
        }
    }
}