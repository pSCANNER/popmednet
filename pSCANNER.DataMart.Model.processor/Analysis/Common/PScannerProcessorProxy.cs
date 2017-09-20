// ====================================================================================
//
// Center for Population Health Informatics
// Solution: Lpp.Adapters
// Project: Lpp.Scanner.DataMart.Model.Processors Last Updated By: Westerman, Dax Marek
//
// ====================================================================================

using Lpp.Scanner.DataMart.Model.Processors.Common.Base;
using Lpp.Scanner.DataMart.Model.Processors.Common.Models;
using Lpp.Scanner.DataMart.Model.Processors.Common.Parameters;
using Lpp.Scanner.DataMart.Model.Processors.Common.Parameters.Request;
using Lpp.Scanner.DataMart.Model.Processors.Common.Parameters.Response;
using Lpp.Scanner.DataMart.Model.Processors.Common.Pmml;
using Lpp.Scanner.DataMart.Model.Processors.Common.Processor.Adapers;
using Lpp.Scanner.DataMart.Model.Processors.Common.Processor.Adapers.Base;
using Lpp.Scanner.DataMart.Model.Processors.Common.Processor.Task;
using Lpp.Scanner.DataMart.Model.Processors.Common.R;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Lpp.Scanner.DataMart.Model.Processors.Analysis.Common {

    /// <summary>
    /// </summary>
    /// <seealso cref="Lpp.Scanner.DataMart.Model.Processors.Common.Base.ProxyBase"/>
    /// <seealso cref="ProxyBase"/>
    public class PScannerProcessorProxy : ProxyBase {

        /// <summary>
        ///     Gets the response.
        /// </summary>
        /// <param name="requestParameter">The request parameter.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override ResponseBase DoGetResponse(BaseRequestParameter requestParameter) {
            ResponseBase response;

            /*
             * Request
             * Type: get
             */

            // RequestId=
            if (requestParameter is AnalysticsRequestParameter) {
                var processorRequestParameter = ((AnalysticsRequestParameter)requestParameter);
                var requestUriString = string.Format("{0}/{1}?{2}={3}", processorRequestParameter.ServiceUrl, BaseRequestParameter.RequestForEnum.GetResponse, BaseRequestParameter.RequestForEnum.RequestId, requestParameter.RequestId);
                var responseFromServer = (new WebAdapter()).PerformRequest(new WebServiceRequestParameter(requestUriString, requestParameter.RequestId));

                /*
             * Response
             */

                // 200OK Statuses=<status> <status> Complete InProgress Canceled Error NextIteration ResponseXml=

                var match = ResponseXmlRegex.Match(((RResponse)responseFromServer).Result);
                var status = match.Groups[Constants.Processor.Output.RegExTokens.Status.ToString()].Value;
                var responseXml = match.Groups[Constants.Processor.Output.RegExTokens.ResponseXml.ToString()].Value;
                response = new ProcessorResponse { Status = ConvertStringStatusToEnumStatus(status), Response = HttpUtility.UrlDecode(responseXml) };
            } else {
                response = new ProcessorResponse();
            }

            return response;
        }

        /// <summary>
        ///     Gets the status.
        /// </summary>
        /// <param name="requestParameter">The request parameter.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override ResponseBase DoGetStatus(BaseRequestParameter requestParameter) {
            ResponseBase responseFromServer;

            /*
             * Request
             * Type: get
             */

            // get RequestId=
            if (requestParameter is AnalysticsRequestParameter) {
                var processorRequestParameter = ((AnalysticsRequestParameter)requestParameter);
                var requestUriString = string.Format("{0}/{1}?{2}={3}", processorRequestParameter.ServiceUrl, BaseRequestParameter.RequestForEnum.GetStatus, BaseRequestParameter.RequestForEnum.RequestId, requestParameter.RequestId);
                responseFromServer = (new WebAdapter()).PerformRequest(new WebServiceRequestParameter(requestUriString, requestParameter.RequestId));
            } else {
                responseFromServer = new ProcessorResponse();
                responseFromServer.Status = Constants.ResponseStatus.Complete;
            }
            /*
             * Response
             */

            // 200OK Statuses=<status> <status> Complete InProgress Canceled Error

            return responseFromServer;
        }

        /// <summary>
        ///     Posts the request.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public override ResponseBase DoPostRequest(BaseRequestParameter parameter) {
            BaseAdapter adapter;
            BaseRequestParameter adapterParameter;

            var combinedJson = combineJsonDocs((IUserJsonInputParameter)parameter);

            //if (parameter is AnalysticsRequestParameter) {
            //    var xmlPmmlRequest = convertJsonPmmlToXmlPmml(combinedJson);
            //    var requestUriString = string.Format("{0}/{1}?{2}={3}&{4}={5}", ((AnalysticsRequestParameter)parameter).ServiceUrl, BaseRequestParameter.RequestForEnum.PostRequest, BaseRequestParameter.RequestForEnum.RequestId, parameter.RequestId, BaseRequestParameter.RequestForEnum.RequestXml, xmlPmmlRequest);
            //    adapter = new WebAdapter();
            //    adapterParameter = new WebServiceRequestParameter(requestUriString, parameter.RequestId);
            //} else if (parameter is AggregatorClientRequestParameter) {
            //    adapter = initializeAdapter(parameter, combinedJson, out adapterParameter);
            //} else {
            //    throw new NullReferenceException(string.Format("Do not know how to handle {0}", parameter.GetType()));
            //}

            adapter = initializeAdapter(parameter, combinedJson, out adapterParameter);

            /*
             * Request
             * Type: get
             */

            // RequestId= RequestXml= <-- UUEncoded

            var response = adapter.PerformRequest(adapterParameter);
            response.Status = Constants.ResponseStatus.Complete;
            return response;
        }

        /// <summary>
        ///     Stops the specified service URL.
        /// </summary>
        /// <param name="requestParameter">The request parameter.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        protected override ResponseBase DoStop(BaseRequestParameter requestParameter) {
            var processorRequestParameter = ((AnalysticsRequestParameter)requestParameter);
            var requestUriString = string.Format("{0}/{1}?{2}={3}", processorRequestParameter.ServiceUrl, BaseRequestParameter.RequestForEnum.StopRequest, BaseRequestParameter.RequestForEnum.RequestId, requestParameter.RequestId);
            return (new WebAdapter()).PerformRequest(new WebServiceRequestParameter(requestUriString, requestParameter.RequestId));
        }

        /// <summary>
        ///     Gets the task.
        /// </summary>
        /// <param name="requestParameter">The request parameter.</param>
        /// <returns></returns>
        protected override TaskBase GetTask(BaseRequestParameter requestParameter) {
            return new NullTask();
        }

        /// <summary>
        ///     Removes the task.
        /// </summary>
        /// <param name="requestParameter">The request parameter.</param>
        protected override void RemoveTask(BaseRequestParameter requestParameter) { }

        /// <summary>
        ///     Converts the json PMML to XML PMML.
        /// </summary>
        /// <param name="pmmlRequest">The PMML request.</param>
        /// <param name="encode">if set to <c>true</c> [encode].</param>
        /// <returns></returns>
        private static string convertJsonPmmlToXmlPmml(string pmmlRequest, bool encode = true) {
            var pmmlDocument = PmmlParser.CreateFromJson(pmmlRequest);
            return encode ? pmmlDocument.GetEncodedXml() : pmmlDocument.TheXmlDocument.OuterXml;
        }

        /// <summary>
        ///     Converts the string status to enum status.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <returns></returns>
        private static Constants.ResponseStatus ConvertStringStatusToEnumStatus(string status) {
            var retVal = Constants.ResponseStatus.Undefined;
            status = status.ToLower();
            switch (status) {
                case Constants.Processor.Output.WebAdapter.complete: {
                        retVal = Constants.ResponseStatus.Complete;
                        break;
                    }
                case Constants.Processor.Output.WebAdapter.inprogress: {
                        retVal = Constants.ResponseStatus.InProgress;
                        break;
                    }
                case Constants.Processor.Output.WebAdapter.canceled: {
                        retVal = Constants.ResponseStatus.Canceled;
                        break;
                    }
                case Constants.Processor.Output.WebAdapter.error: {
                        retVal = Constants.ResponseStatus.Error;
                        break;
                    }
                default: {
                        break;
                    }
            }
            return retVal;
        }

        /// <summary>
        ///     Converts to PMML.
        /// </summary>
        /// <param name="rResponse">The r response.</param>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        private static string convertToPmml(string rResponse, BaseRequestParameter param) {
            var model = ErrorAndCovarianceModel.Create(rResponse);

            var aggrParameter = (AggregatorClientRequestParameter)param;

            var dataDictionary = aggrParameter.Pmml.GetDataDictionary();

            var linkFunction = aggrParameter.Pmml.GetLinkFunction(LocalConstants.LinkFunctionName.GetValue());

            var family = aggrParameter.Pmml.GetFamily(LocalConstants.FamilyName.GetValue());

            var pmml = new PmmlScriptBuilder();

            pmml.AddLine(@"<?xml version=""1.0"" encoding=""utf-8""?>");
            pmml.AddLine(@"<PMML version=""4.2"" xmlns=""http://www.dmg.org/PMML-4_2"">");
            pmml.AddLine(@"<Header copyright=""Copyright (c) 2015 user"" description=""Generalized Linear Regression Model"">");
            pmml.AddLine(string.Format(@"<Extension name=""errorGradient"">{0}</Extension>", model.ErrorGradient.GetPmmlValue()));
            pmml.AddLine(string.Format(@"<Extension name=""convergence"">{0}</Extension>", model.ConvergenceWarning.GetPmmlValue()));
            pmml.AddLine(@"<Application name=""pScannerAggregatorClient"" version=""0.1""></Application>");
            pmml.AddLine(@"<Timestamp>2015-04-21 12:54:37</Timestamp>");
            pmml.AddLine(@"</Header>");

            pmml.AddLine(@"<DataDictionary numberOfFields=""{0}"">", dataDictionary.NumberOfFields.ToString());

            foreach (var independentVariable in dataDictionary.DataFields) {
                pmml.AddLine(@"<DataField name=""{0}"" optype=""{1}"" dataType=""{2}""></DataField>", independentVariable.Value.Name, independentVariable.Value.Optype, independentVariable.Value.DataType);
            }

            pmml.AddLine(@"</DataDictionary>");

            pmml.AddLine(@"<GeneralRegressionModel modelName=""General_Regression_Model"" modelType=""regression"" {0}=""{2}"" algorithmName=""glm"" distribution=""normal"" {1}=""{3}"">", LocalConstants.FamilyName, LocalConstants.LinkFunctionName, family, linkFunction);
            pmml.AddLine(@"<MiningSchema>");
            pmml.AddLine(@"<MiningField name=""xgre"" usageType=""target""></MiningField>");
            pmml.AddLine(@"<MiningField name=""xgpa"" usageType=""active""></MiningField>");
            pmml.AddLine(@"<MiningField name=""xrank"" usageType=""active""></MiningField>");
            pmml.AddLine(@"</MiningSchema>");
            pmml.AddLine(@"<PCovMatrix>");

            pmml.AddLine(model.UnscaledCovariance.GetPmmlValue());

            pmml.AddLine(@"</PCovMatrix>");
            pmml.AddLine(@"</GeneralRegressionModel>");
            pmml.AddLine(@"</PMML>");

            return pmml.ToString();
        }

        /// <summary>
        ///     Copies the analysis data set into analysis PMML.
        /// </summary>
        /// <param name="analysisDataSet">The analysis data set.</param>
        /// <param name="combined">The combined.</param>
        private static void copyAnalysisDataSetIntoAnalysisPmml(string analysisDataSet, ref string combined) {
            var combinedJsonObject = JObject.Parse(combined);
            var analysisDataSetObject = JObject.Parse(analysisDataSet);

            var pmmlNode = combinedJsonObject[Constants.Pmml.Tags.PMML.TagName] as JObject;
            if (pmmlNode != null) {
                pmmlNode.Add(Constants.Pmml.Tags.PMML.DataDictionary.TagName, analysisDataSetObject[Constants.Pmml.Tags.PMML.DataDictionary.TagName]);
            }

            combined = combinedJsonObject.ToString();
        }

        /// <summary>
        ///     Gets the adapter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="conv">The conv.</param>
        /// <param name="rScript">The r script.</param>
        /// <param name="aggrParam">The aggr parameter.</param>
        /// <param name="adapterParameter">The adapter parameter.</param>
        /// <returns></returns>
        private static BaseAdapter getAdapter(BaseRequestParameter parameter, Func<string, BaseRequestParameter, object[], string> conv, string rScript, IRExecution aggrParam, out BaseRequestParameter adapterParameter) {
            BaseAdapter adapter = new RAdapter(conv);
            adapterParameter = new RRequestParameter(rScript, aggrParam.ExecutionPath, parameter.RequestId, BaseRequestParameter.RequestForEnum.PostRequest, aggrParam.DataSetConnection);
            return adapter;
        }

        /// <summary>
        ///     Gets the items.
        /// </summary>
        /// <param name="namespacePieces">The namespace pieces.</param>
        /// <param name="parentItem">The parent item.</param>
        /// <param name="currentItem">The current item.</param>
        /// <returns></returns>
        private static string getItems(IReadOnlyList<string> namespacePieces, out JToken parentItem, ref JToken currentItem) {
            parentItem = null;

            var name = string.Empty;

            var index = 0;

            while (index < namespacePieces.Count) {
                name = namespacePieces[index];

                if ((currentItem is JArray) == false) {
                    if (currentItem[name] is JArray) {
                        currentItem = (JArray)currentItem[name];
                        parentItem = (JArray)currentItem;
                    } else if (currentItem[name] is JObject) {
                        currentItem = (JObject)currentItem[name];
                        parentItem = (JObject)currentItem;
                    } else {
                        currentItem = (JValue)currentItem[name];
                        parentItem = (JValue)currentItem;
                    }
                }

                index++;
            }

            return name;
        }

        /// <summary>
        ///     Initializes the adapter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="combinedJson">The combined json.</param>
        /// <param name="adapterParameter">The adapter parameter.</param>
        /// <returns></returns>
        private static BaseAdapter initializeAdapter(BaseRequestParameter parameter, string combinedJson, out BaseRequestParameter adapterParameter) {
            BaseAdapter adapter = null;
            string xmlPmmlRequest = null;
            Func<string, BaseRequestParameter, object[], string> conv = null;
            string rScript = null;

            if (parameter is AggregatorClientRequestParameter || parameter is AnalysticsRequestParameter) {
                xmlPmmlRequest = convertJsonPmmlToXmlPmml(combinedJson, false);
                ((IRExecution)parameter).SetPmmlDocument(xmlPmmlRequest);
            } else {
                throw new NullReferenceException(string.Format("Do not know how to handle {0}", parameter.GetType()));
            }

            if (parameter is AnalysticsRequestParameter) {
                var aggrParam = (AnalysticsRequestParameter)parameter;
                rScript = RScriptWriter.GenerateRScript(aggrParam);
                var dataDictionary = aggrParam.Pmml.GetDataDictionary();
                var family = aggrParam.Pmml.GetFamily(LocalConstants.FamilyName.GetValue());
                var linkFunction = aggrParam.Pmml.GetLinkFunction(LocalConstants.LinkFunctionName.GetValue());

                conv = (r, req, objs) => {
                    var pmml = new PmmlScriptBuilder();
                    pmml.AddLine(@"<?xml version=""1.0"" encoding=""utf-8""?>");
                    pmml.AddLine(@"<PMML version=""4.2"" xmlns=""http://www.dmg.org/PMML-4_2"">");
                    pmml.AddLine(@"<Header copyright=""Copyright (c) 2015 user"" description=""Generalized Linear Regression Model"">");

                    pmml.AddLine(string.Format(@"<Extension name=""convergence"">{0}</Extension>", model.ConvergenceWarning.GetPmmlValue()));
                    pmml.AddLine(string.Format(@"<Extension name=""errorGradient"">{0}</Extension>", model.ErrorGradient.GetPmmlValue()));
                    pmml.AddLine(@"<Application name=""pScannerAggregatorClient"" version=""0.1""></Application>");
                    pmml.AddLine(@"<Timestamp>2015-04-21 12:54:37</Timestamp>");
                    pmml.AddLine(@"</Header>");

                    pmml.AddLine(@"<DataDictionary numberOfFields=""{0}"">", dataDictionary.NumberOfFields.ToString());

                    foreach (var independentVariable in dataDictionary.DataFields) {
                        pmml.AddLine(@"<DataField name=""{0}"" optype=""{1}"" dataType=""{2}""></DataField>", independentVariable.Value.Name, independentVariable.Value.Optype, independentVariable.Value.DataType);
                    }

                    pmml.AddLine(@"</DataDictionary>");

                    pmml.AddLine(@"<GeneralRegressionModel modelName=""General_Regression_Model"" modelType=""regression"" {3}=""{2}"" algorithmName=""glm"" distribution=""normal"" {0}=""{1}"">", LocalConstants.LinkFunctionName, linkFunction, family, LocalConstants.FamilyName);

                    pmml.AddLine(@"</GeneralRegressionModel>");
                    pmml.AddLine(@"</PMML>");

                    return pmml.ToString();
                };
            } else {
                var aggrParam = (AggregatorClientRequestParameter)parameter;
                rScript = RScriptWriter.GenerateRScript(aggrParam);
                var dataDictionary = aggrParam.Pmml.GetDataDictionary();

                conv = (r, req, objs) => {
                    var model = ErrorAndCovarianceModel.Create(r);

                    var family = aggrParam.Pmml.GetFamily(LocalConstants.FamilyName.GetValue());
                    var linkFunction = aggrParam.Pmml.GetLinkFunction(LocalConstants.LinkFunctionName.GetValue());

                    var pmml = new PmmlScriptBuilder();

                    pmml.AddLine(@"<?xml version=""1.0"" encoding=""utf-8""?>");
                    pmml.AddLine(@"<PMML version=""4.2"" xmlns=""http://www.dmg.org/PMML-4_2"">");
                    pmml.AddLine(@"<Header copyright=""Copyright (c) 2015 user"" description=""Generalized Linear Regression Model"">");

                    pmml.AddLine(string.Format(@"<Extension name=""convergence"">{0}</Extension>", model.ConvergenceWarning.GetPmmlValue()));
                    pmml.AddLine(string.Format(@"<Extension name=""errorGradient"">{0}</Extension>", model.ErrorGradient.GetPmmlValue()));
                    pmml.AddLine(@"<Application name=""pScannerAggregatorClient"" version=""0.1""></Application>");
                    pmml.AddLine(@"<Timestamp>2015-04-21 12:54:37</Timestamp>");
                    pmml.AddLine(@"</Header>");

                    pmml.AddLine(@"<DataDictionary numberOfFields=""{0}"">", dataDictionary.NumberOfFields.ToString());

                    foreach (var independentVariable in dataDictionary.DataFields) {
                        pmml.AddLine(@"<DataField name=""{0}"" optype=""{1}"" dataType=""{2}""></DataField>", independentVariable.Value.Name, independentVariable.Value.Optype, independentVariable.Value.DataType);
                    }

                    pmml.AddLine(@"</DataDictionary>");

                    pmml.AddLine(@"<GeneralRegressionModel modelName=""General_Regression_Model"" modelType=""regression"" {3}=""{2}"" algorithmName=""glm"" distribution=""normal"" {0}=""{1}"">", LocalConstants.LinkFunctionName, linkFunction, family, LocalConstants.FamilyName);

                    pmml.AddLine(@"<PCovMatrix>");

                    pmml.AddLine(model.UnscaledCovariance.GetPmmlValue());

                    pmml.AddLine(@"</PCovMatrix>");
                    pmml.AddLine(@"</GeneralRegressionModel>");
                    pmml.AddLine(@"</PMML>");

                    return pmml.ToString();
                };
            }

            adapter = getAdapter(parameter, conv, rScript, (IRExecution)parameter, out adapterParameter);

            return adapter;
        }

        /// <summary>
        ///     Combines the json docs.
        /// </summary>
        /// <param name="parameter">The request parameter.</param>
        /// <returns></returns>
        private string combineJsonDocs(IUserJsonInputParameter parameter) {
            var combined = parameter.PmmlJson;

            //dynamic dataset = JsonConvert.DeserializeObject(parameter.DatasetJson);
            //dynamic dns = dataset["DataSetName"];

            //dynamic deserializeObject = JsonConvert.DeserializeObject(combined);
            //JArray ext = (JArray)deserializeObject["PMML"]["Header"]["Extension"];

            //var obj = new JObject();

            //obj["name"] = "data";
            //DatabaseDataSetConnection connection = (DatabaseDataSetConnection)DataSetConnection.Get(dns.ToString());
            //obj["value"] = connection.ConnectionString;

            //ext.Add(obj);

            //combined = JsonConvert.SerializeObject(deserializeObject);

            copyAnalysisDataSetIntoAnalysisPmml(parameter.DatasetJson, ref combined);

            mergeAnalysisParametersIntoAnalysisPmml(parameter.ParametersJson, ref combined);

            return combined;
        }

        /// <summary>
        ///     Merges the analysis parameters into analysis PMML.
        /// </summary>
        /// <param name="analysisParameters">The analysis parameters.</param>
        /// <param name="combined">The combined.</param>
        /// <exception cref="NullReferenceException">combinedJsonObject or analysisDataSetObject</exception>
        /// <exception cref="System.NullReferenceException">combinedJsonObject or analysisDataSetObject</exception>
        private void mergeAnalysisParametersIntoAnalysisPmml(string analysisParameters, ref string combined) {
            var combinedJsonObject = JObject.Parse(combined);
            if (combinedJsonObject == null) {
                throw new NullReferenceException("combinedJsonObject");
            }
            var analysisDataSetObject = JObject.Parse(analysisParameters);
            if (analysisDataSetObject == null) {
                throw new NullReferenceException("analysisDataSetObject");
            }

            var pCellIndex = 1;

            var properties = analysisDataSetObject.Properties();

            var enumerable = properties as JProperty[] ?? properties.ToArray();

            MaxIterationsVal = 20;

            foreach (var x in enumerable) {
                JToken parentItem;
                JToken currentItem = combinedJsonObject;
                var namespacePieces = x.Name.Split('.');

                if (x.Name.Contains(VariableConstants.MaxIterationsConst)) {
                    var maxIterations = x.Value.ToString();
                    int maxIterationsVal;
                    if (int.TryParse(maxIterations, out maxIterationsVal) == false) {
                        maxIterationsVal = 20;
                    }

                    MaxIterationsVal = maxIterationsVal;
                } else if (x.Name.Contains(VariableConstants.AggregationCycleCountConst)) {
                    var aggrCount = x.Value.ToString();

                    //int aggrCountVal;
                    //if (int.TryParse(aggrCount, out aggrCountVal) == false)
                    //{
                    //    aggrCountVal = 1;
                    //}

                    //x.Value = AggregationCount = aggrCountVal + 1;
                } else if (x.Name.Contains("PMML.GeneralRegressionModel.Coefficients")) {
                    var index = namespacePieces.Last().Equals(VariableConstants.InterceptConst, StringComparison.InvariantCultureIgnoreCase) ? 0 : pCellIndex++;

                    parentItem = currentItem[namespacePieces[0]];
                    currentItem = parentItem[namespacePieces[1]];

                    var ppMatrix = currentItem[Constants.Pmml.Tags.PMML.GeneralRegressionModel.PPMatrix.TagName];
                    var paramMatrix = currentItem[Constants.Pmml.Tags.PMML.GeneralRegressionModel.ParamMatrix.TagName];
                    var requestParameterList = currentItem[Constants.Pmml.Tags.PMML.GeneralRegressionModel.RequestParameterList.TagName];

                    var parameterName = string.Format("p{0}", index);
                    var predictorName = namespacePieces.Last();

                    if (index > 0) {
                        var value = index.ToString();
                        var ppCell = JsonConvert.DeserializeObject(string.Format("{{\"PPCell\":{{\"value\":\"{0}\",\"predictorName\":\"{1}\",\"parameterName\":\"{2}\"}}}}", value, predictorName, parameterName));
                        ((JArray)ppMatrix).Add(ppCell);
                    }

                    var degreesOfFreedom = "1";
                    var beta = x.Value.ToString();
                    var pCell = JsonConvert.DeserializeObject(string.Format("{{\"PCell\":{{\"parameterName\":\"{0}\",\"df\":\"{1}\",\"beta\":\"{2}\"}}}}", parameterName, degreesOfFreedom, beta));

                    ((JArray)paramMatrix).Add(pCell);

                    var requestParameter = JsonConvert.DeserializeObject(string.Format("{{\"RequestParameter\":{{\"name\":\"{0}\",\"label\":\"{1}\"}}}}", parameterName, predictorName));
                    ((JArray)requestParameterList).Add(requestParameter);
                } else {
                    var name = getItems(namespacePieces, out parentItem, ref currentItem);
                    switch (name) {
                        case Constants.Pmml.Tags.PMML.GeneralRegressionModel.MiningField.target:
                        case Constants.Pmml.Tags.PMML.GeneralRegressionModel.MiningField.predicted:
                        case Constants.Pmml.Tags.PMML.GeneralRegressionModel.MiningField.active: {
                                JArray jsonItems;

                                if ((x.Value is JArray) == false) {
                                    jsonItems = new JArray { x.Value };
                                } else {
                                    jsonItems = ((JArray)x.Value);
                                }

                                foreach (var item in jsonItems) {
                                    var newObject = new JObject { { "usageType", name }, { "name", item } };
                                    ((JArray)parentItem).Add(newObject);
                                }
                            ;

                                break;
                            }

                        // case Constants.Pmml.Tags.PMML.GeneralRegressionModel.MiningField.active: { var jsonItems = ((JArray) x.Value);

                        //    jsonItems.ForEach(item => {
                        //        var newObject = new JObject { { "usageType", name }, { "name", item } };
                        //        ((JArray) parentItem).Add(newObject);
                        //    });
                        //    break;
                        //}

                        case Constants.Pmml.Tags.PMML.Header.Extension.Type:
                        case Constants.Pmml.Tags.PMML.Header.Extension.DataSetName:
                        case Constants.Pmml.Tags.PMML.Header.Extension.DataSetSchemaVersion: {
                                var jsonItems = (x.Value);

                                var newObject = new JObject { { "name", name }, { "value", jsonItems.ToString() } };
                                ((JArray)parentItem).Add(newObject);

                                break;
                            }

                        case Constants.Pmml.Tags.PMML.GeneralRegressionModel.RequestParameterList.TagName:
                        case Constants.Pmml.Tags.PMML.GeneralRegressionModel.CovariateList.TagName:
                        case Constants.Pmml.Tags.PMML.GeneralRegressionModel.FactorList.TagName: {
                                var jsonItems = ((JArray)x.Value);
                                foreach (var activeItem in jsonItems) {
                                    ((JArray)currentItem).Add(activeItem);
                                }
                                break;
                            }
                        default: {
                                if (currentItem != null) {
                                    var jValue = ((JValue)currentItem);
                                    jValue.Value = x.Value.ToString();
                                }
                                break;
                            }
                    }
                }
            }
            combined = combinedJsonObject.ToString();
        }

        /// <summary>
        ///     Gets or sets the aggregation count.
        /// </summary>
        /// <value>The aggregation count.</value>
        public int AggregationCount { get; set; }

        /// <summary>
        ///     Gets or sets the maximum iterations value.
        /// </summary>
        /// <value>The maximum iterations value.</value>
        public int MaxIterationsVal { get; set; }

        /// <summary>
        ///     Gets the response pattern.
        /// </summary>
        /// <value>The response pattern.</value>
        private string ResponsePattern {
            get {
                return string.Format("(?:{0}(?:\\r\\n)?{1}=(?<{2}>.*)\\b)", StatusExpression, Constants.Processor.Output.RegExTokens.ResponseXml, Constants.Processor.Output.RegExTokens.ResponseXml);
            }
        }

        /// <summary>
        ///     Gets the response XML regex.
        /// </summary>
        /// <value>The response XML regex.</value>
        private Regex ResponseXmlRegex { get; set; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="PScannerProcessorProxy"/> class.
        /// </summary>
        public PScannerProcessorProxy() {
            ResponseXmlRegex = new Regex(ResponsePattern, RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Multiline);

            if (!EventLog.SourceExists(_source)) {
                EventLog.CreateEventSource(_source, _log);
            }
        }

        /// <summary>
        ///     The log
        /// </summary>
        private static readonly string _log = "Application";

        /// <summary>
        ///     The source
        /// </summary>
        private static readonly string _source = "DMC-PScannerProcessorProxy";
    }
}