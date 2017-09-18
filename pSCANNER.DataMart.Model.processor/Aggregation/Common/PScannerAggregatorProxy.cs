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
using Lpp.Scanner.DataMart.Model.Processors.Common.ExtMethods;
using Lpp.Scanner.DataMart.Model.Processors.Common.Models;
using Lpp.Scanner.DataMart.Model.Processors.Common.Parameters;
using Lpp.Scanner.DataMart.Model.Processors.Common.Parameters.Request;
using Lpp.Scanner.DataMart.Model.Processors.Common.Parameters.Response;
using Lpp.Scanner.DataMart.Model.Processors.Common.Pmml;
using Lpp.Scanner.DataMart.Model.Processors.Common.Processor.Task;
using Lpp.Scanner.DataMart.Model.Processors.Common.R;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

#endregion Using

namespace Lpp.Scanner.DataMart.Model.Processors.Aggregation.Common {

    /// <summary>
    /// </summary>
    /// <typeparam name="TTask">The type of the task.</typeparam>
    /// <seealso cref="Lpp.Scanner.DataMart.Model.Processors.Common.Base.ProxyBase"/>
    /// <seealso cref="ProxyBase"/>
    public class PScannerAggregatorProxy<TTask> : ProxyBase where TTask : TaskBase {

        /// <summary>
        ///     Does the get response.
        /// </summary>
        /// <param name="requestParameter">The request parameter.</param>
        /// <returns></returns>
        public override ResponseBase DoGetResponse(BaseRequestParameter requestParameter) {
            var response = new AggregatorResponse { Status = Constants.ResponseStatus.InProgress };

            var task = GetTask(requestParameter);

            if (task == null) {
                response.Status = Constants.ResponseStatus.DoesNotExist;
            } else {
                var responseBase = (RResponse)task.Adapter.PerformRequest(requestParameter);
                response = new AggregatorResponse(responseBase) { FinalResponse = responseBase.Result };
            }

            return response;
        }

        /// <summary>
        ///     Does the get status.
        /// </summary>
        /// <param name="requestParameter">The request parameter.</param>
        /// <returns></returns>
        public override ResponseBase DoGetStatus(BaseRequestParameter requestParameter) {
            var taskBase = GetTask(requestParameter);
            ;
            var baseAdapter = taskBase.Adapter;
            var doGetStatus = baseAdapter.PerformRequest(requestParameter);
            return doGetStatus;
        }

        /// <summary>
        ///     Posts the request.
        /// </summary>
        /// <param name="requestParameter">The request parameter.</param>
        /// <returns></returns>
        public override ResponseBase DoPostRequest(BaseRequestParameter requestParameter) {
            var response = new AggregatorResponse { Status = Constants.ResponseStatus.InProgress };

            try {
                var parameter = (AggregatorServerRequestParameter)requestParameter;

                var dataFields = getDataFields(parameter.SiteResponses);

                var siteResponses = new SiteResponse[parameter.SiteResponses.Count];

                var index = 0;
                while (index < siteResponses.Length) {
                    siteResponses[index] = new SiteResponse(parameter.SiteNames[index], parameter.SiteResponses[index]);
                    index++;
                }

                var pCovMatrixPerSite = getPCovMatrixPerSite(siteResponses);

                var errorGradientPerSite = getErrorGradientPerSite(siteResponses);

                var beta0 = getBeta0(parameter.ParametersJson);

                var maxAggregationCount = getMaxAggregationCount(parameter.ParametersJson);
                var paramsJson = parameter.ParametersJson;
                var aggregationCount = getAggregationCount(ref paramsJson);
                parameter.ParametersJson = paramsJson;
                var rScript = RScriptWriter.GenerateRScript(pCovMatrixPerSite, errorGradientPerSite, beta0, dataFields, aggregationCount, maxAggregationCount);

                var rRequest = new RRequestParameter(rScript, parameter.ExecutionPath, parameter.RequestId, parameter.RequestFor, parameter.DataSetConnection);

                var task = GetTask(parameter);

                if (task != null) {
                    task.WasRequestedAgain();
                } else {
                    task = (TTask)Activator.CreateInstance(typeof(TTask), rRequest, new RAdapter(convertToParametersJson, parameter, parameter.ParametersJson));
                    Tasks.Add(parameter.RequestId, task);
                    task.Start();
                }
            } catch (Exception exception) {
                response.Status = Constants.ResponseStatus.Error;
                response.FinalResponse = exception.Message;
            }

            return response;
        }

        /// <summary>
        ///     Does the stop.
        /// </summary>
        /// <param name="requestParameter">The request parameter.</param>
        /// <returns></returns>
        protected override ResponseBase DoStop(BaseRequestParameter requestParameter) {
            var response = new AggregatorResponse { Status = Constants.ResponseStatus.InProgress };

            var task = GetTask(requestParameter);
            if (task == null) {
                response.Status = Constants.ResponseStatus.DoesNotExist;
            } else {
                response = task.Adapter.PerformRequest(requestParameter) as AggregatorResponse;
                RemoveTask(requestParameter);
            }

            return response;
        }

        /// <summary>
        ///     Gets the task.
        /// </summary>
        /// <param name="requestParameter">The request parameter.</param>
        /// <returns></returns>
        protected override TaskBase GetTask(BaseRequestParameter requestParameter) {
            return Tasks.Where(x => x.Key == requestParameter.RequestId).Select(x => x.Value).FirstOrDefault();
        }

        /// <summary>
        ///     Removes the task.
        /// </summary>
        /// <param name="requestParameter">The request parameter.</param>
        protected override void RemoveTask(BaseRequestParameter requestParameter) {
            Tasks.Remove(requestParameter.RequestId);
        }

        /// <summary>
        ///     Converts to PMML.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        private static string convertToParametersJson(string response, BaseRequestParameter parameter, object[] parameters) {
            var features = RScriptBuilder.GetResponse(response, "FeatureList");

            var featureMatches = ScriptConstants._featureListRegularExpression.Matches(features);

            var featureList = (from Match entry in featureMatches select entry.Groups["value"] into value select value.ToString()).ToList();
            featureList.Insert(0, VariableConstants.InterceptConst);

            var doIterate = false;

            var regexObj = new Regex(string.Format(ScriptConstants.RegExString, ScriptConstants._beta0), RegexOptions.IgnoreCase | RegexOptions.Compiled);
            var matchResults = regexObj.Match(response);
            if (matchResults.Success) {
                var captureCollection = ((Match)matchResults.Groups[0].Captures[0]).Groups[1].Captures;
                if (captureCollection.Count >= 1) {
                    if (captureCollection.Count > 0) {
                        doIterate = true;
                    }
                }
            }

            regexObj = new Regex(string.Format(ScriptConstants.RegExString, ScriptConstants._beta1), RegexOptions.IgnoreCase | RegexOptions.Compiled);

            matchResults = regexObj.Match(response);
            var values = new List<double>();
            if (matchResults.Success) {
                var captureCollection = ((Match)matchResults.Groups[0].Captures[0]).Groups[1].Captures;
                foreach (Capture capture in captureCollection) {
                    var valueAsDouble = capture.Value.ToDouble();
                    values.Add(valueAsDouble);
                }
            }

            var requestParameter = (AggregatorServerRequestParameter)parameter;

            var jobj = JObject.Parse(requestParameter.ParametersJson.Replace(@"\r\n", string.Empty));

            jobj[ScriptConstants.IterationTypeConst] = doIterate ? ScriptConstants.IterationConst : ScriptConstants.StopIterationConst;

            int aggregationCycleCount;
            if (int.TryParse((jobj[string.Format("{0}.{1}", VariableConstants.PMML_Header_Extension, VariableConstants.AggregationCycleCountConst)] ?? string.Empty).ToString(), out aggregationCycleCount) == false) {
                aggregationCycleCount = 0;
            }

            var index = 0;
            while (index < featureList.Count) {
                jobj[ScriptConstants.CoefficientStringSuffixConst + featureList[index].DropPrefix("x")] = values[index].ToString(CultureInfo.InvariantCulture);
                index++;
            }

            var serializeObject = JsonConvert.SerializeObject(jobj, Formatting.Indented);

            return serializeObject;
        }

        /// <summary>
        ///     Gets the beta0.
        /// </summary>
        /// <param name="parametersJson">The parameters json.</param>
        /// <returns></returns>
        private static Coefficients getBeta0(string parametersJson) {
            var coefficients = new Coefficients();

            var regexObj = new Regex(string.Format(@"((?nix)""PMML.GeneralRegressionModel.Coefficients.(?<{0}>(?<{1}>[^""]+)""\s*:\s*""(?<{2}>\-?\d*(\.\d*)?)(?="")))", pair, name, value), RegexOptions.IgnoreCase | RegexOptions.Compiled);

            var matchResults = regexObj.Match(parametersJson);

            while (matchResults.Success) {
                var varName = matchResults.Groups[name].Value;
                var varValue = matchResults.Groups[value].Value;
                coefficients.Add(varName, varValue);
                matchResults = matchResults.NextMatch();
            }

            var targetVariableRegEx = new Regex(@"((?nix)""PMML.GeneralRegressionModel.MiningSchema.MiningField.target""\s*:\s*""(?<target>.+)"")");

            matchResults = targetVariableRegEx.Match(parametersJson);

            if (matchResults.Success) {
                var targetValue = matchResults.Groups["target"].Value;
                coefficients.setTarget(targetValue);
            }

            return coefficients;
        }

        /// <summary>
        ///     Gets the aggregation count.
        /// </summary>
        /// <param name="parametersJson">The parameters json.</param>
        /// <returns></returns>
        private int getAggregationCount(ref string parametersJson) {
            dynamic paramsObject = JsonConvert.DeserializeObject(parametersJson);
            var tag = string.Format("{0}.{1}", VariableConstants.PMML_Header_Extension, VariableConstants.AggregationCycleCountConst);
            var aggrCountNode = paramsObject[tag];
            aggrCountNode = (int)(aggrCountNode ?? 0) + 1;
            paramsObject[tag] = aggrCountNode;
            parametersJson = JsonConvert.SerializeObject(paramsObject);

            return aggrCountNode;
        }

        /// <summary>
        ///     Gets the data fields.
        /// </summary>
        /// <param name="siteResponses">The site responses.</param>
        /// <returns></returns>
        private IEnumerable<string> getDataFields(IEnumerable<string> siteResponses) {
            var dataDictionaries = siteResponses.Select(entry => new DataDictionaryEntry(entry)).ToList();

            return dataDictionaries.First().DataFields.Select(x => x.Name).ToList();
        }

        /// <summary>
        ///     Gets the error gradient per site.
        /// </summary>
        /// <param name="siteResponses">The site responses.</param>
        /// <returns></returns>
        private IDictionary<string, ErrorGradientModel> getErrorGradientPerSite(IEnumerable<SiteResponse> siteResponses) {
            var errorGradients = siteResponses.Select(x => new ErrorGradientModel(x)).ToList();
            return errorGradients.ToDictionary(x => x.SiteName, x => x);
        }

        /// <summary>
        ///     Gets the maximum aggregation count.
        /// </summary>
        /// <param name="parametersJson">The parameters json.</param>
        /// <returns></returns>
        private int getMaxAggregationCount(string parametersJson) {
            var max = 20;

            var pattern = string.Format(@"((?nix)""PMML.Header.Extension.{0}""\s*:\s*""(?<{1}>\d+)?"")", VariableConstants.MaxIterationsConst, value);
            var regexObj = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            var matchResults = regexObj.Match(parametersJson);
            if (matchResults.Success) {
                max = int.Parse(matchResults.Groups[value].Value);
            }
            return max;
        }

        /// <summary>
        ///     Gets the p cov matrix per site.
        /// </summary>
        /// <param name="pmmlCovarianceMatrices">The PMML covariance matrices.</param>
        /// <returns></returns>
        private IDictionary<string, PCovMatrix> getPCovMatrixPerSite(IEnumerable<SiteResponse> pmmlCovarianceMatrices) {
            var pCovMatrices = pmmlCovarianceMatrices.Select(x => new PCovMatrix(x)).ToList();

            return pCovMatrices.ToDictionary(x => x.SiteName, x => x);
        }

        /// <summary>
        ///     The name
        /// </summary>
        private const string name = "name";

        /// <summary>
        ///     The pair
        /// </summary>
        private const string pair = "pair";

        /// <summary>
        ///     The value
        /// </summary>
        private const string value = "value";

        #region Fields

        /// <summary>
        ///     The tasks
        /// </summary>
        private readonly Dictionary<string, TaskBase> _tasks = new Dictionary<string, TaskBase>();

        #endregion Fields

        #region Properties

        ///// <summary>
        /////     Gets the dataset json.
        ///// </summary>
        ///// <value>
        /////     The dataset json.
        ///// </value>
        //public string DatasetJson { get; private set; }

        ///// <summary>
        /////     Gets the parameters json.
        ///// </summary>
        ///// <value>
        /////     The parameters json.
        ///// </value>
        //public string ParametersJson { get; private set; }

        ///// <summary>
        /////     Gets the PMML json.
        ///// </summary>
        ///// <value>
        /////     The PMML json.
        ///// </value>
        //public string PmmlJson { get; private set; }

        /// <summary>
        ///     Gets the tasks.
        /// </summary>
        /// <value>The tasks.</value>
        private Dictionary<string, TaskBase> Tasks {
            get {
                return _tasks;
            }
        }

        #endregion Properties

        /// <summary>
        /// </summary>
        /// <seealso cref="Lpp.Scanner.DataMart.Model.Processors.Common.Base.ProxyBase"/>
        public class VariableEntry {

            #region Fields

            /// <summary>
            ///     The variable description
            /// </summary>
            private string _variableDescription;

            #endregion Fields

            #region Properties

            /// <summary>
            ///     Gets or sets the default value.
            /// </summary>
            /// <value>The default value.</value>
            public string DefaultValue { get; set; }

            /// <summary>
            ///     Gets the variable description.
            /// </summary>
            /// <value>The variable description.</value>
            public string VariableDescription {
                get {
                    return _variableDescription ?? VariableName;
                }
                set {
                    _variableDescription = value;
                }
            }

            /// <summary>
            ///     Gets the name of the variable.
            /// </summary>
            /// <value>The name of the variable.</value>
            public string VariableName { get; set; }

            #endregion Properties

            /// <summary>
            ///     Returns a <see cref="System.String"/> that represents this instance.
            /// </summary>
            /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
            public override string ToString() {
                return VariableName;
            }
        }

        ///// <summary>
        /////     Sets the beta1.
        ///// </summary>
        ///// <param name="scriptBuilder">The script builder.</param>
        ///// <exception cref="System.NotImplementedException"></exception>
        //private static void setBeta1(BaseScriptBuilder scriptBuilder) {
        //    scriptBuilder.AddLine("{0} <- matrix(0, {1}, 1)", _beta1, _columnCount);
        //}
    }
}