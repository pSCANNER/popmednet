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

using System.Diagnostics.CodeAnalysis;
using System.ServiceModel;
using Lpp.Dns.DataMart.Model;

#endregion Using

namespace Lpp.Scanner.DataMart.Model.Processors.Common.Models {

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class Constants {

        /// <summary>
        ///     Gets the request status code.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <returns></returns>
        public static RequestStatus.StatusCode GetRequestStatusCode(ResponseStatus status) {
            var code = RequestStatus.StatusCode.Error;

            switch (status) {
                case ResponseStatus.Complete: {
                        code = RequestStatus.StatusCode.Complete;
                        break;
                    }
                case ResponseStatus.InProgress: {
                        code = RequestStatus.StatusCode.InProgress;
                        break;
                    }
                case ResponseStatus.Canceled: {
                        code = RequestStatus.StatusCode.Canceled;
                        break;
                    }
                case ResponseStatus.Error: {
                        code = RequestStatus.StatusCode.Error;
                        break;
                    }
                default: {
                        break;
                    }
            }
            return code;
        }

        public static string InitialCoefficient = "0";

        /// <summary>
        /// </summary>
        public enum ResponseStatus {
            Complete,
            InProgress,
            Canceled,
            Error,
            Undefined,
            DoesNotExist
        }

        public class Aggregator {
            public const string PreviousIterationResult = "PreviousIterationResult";
            public const string RequestId = "1";

            public class Input {
                public const string DatasetJson = "Lpp.Scanner.DataMart.Model.Examples.Aggregator.AnalysisFiles.DatasetJson.json";

                public const string ExampleDataSetName = "Example";

                public const string ParametersJson = "Lpp.Scanner.DataMart.Model.Examples.Aggregator.AnalysisFiles.ParametersJson.json";

                public const string PmmlJson = "Lpp.Scanner.DataMart.Model.Examples.Aggregator.AnalysisFiles.PmmlJson.json";

                public const string RLocation = @"C:\Program Files\R\R-3.1.3\bin\x64\";

                public enum SettingsEnum {
                    rLocation,
                    ServiceURL
                }

                public class Client {
                    public const string AggregatorClientInput = "Lpp.Scanner.DataMart.Model.Examples.Aggregator.AnalysisFiles.AggregatorClientInput.xml";
                }

                public class CovarianceMatrix {
                    public const string SiteA = "Lpp.Scanner.DataMart.Model.Examples.Aggregator.AnalysisFiles.CovarianceMatrixSiteA.xml";
                    public const string SiteB = "Lpp.Scanner.DataMart.Model.Examples.Aggregator.AnalysisFiles.CovarianceMatrixSiteB.xml";
                    public const string SiteC = "Lpp.Scanner.DataMart.Model.Examples.Aggregator.AnalysisFiles.CovarianceMatrixSiteC.xml";
                }
            }

            public class Output {
                public const string DatasetJson = "Lpp.Scanner.DataMart.Model.Examples.Aggregator.AnalysisFiles.Output.DatasetJson.json";

                public const string Empty = "";

                public const string FinalResponse = "Lpp.Scanner.DataMart.Model.Examples.Aggregator.AnalysisFiles.Output.FinalResponse.xml";

                public const string ParametersJson = "Lpp.Scanner.DataMart.Model.Examples.Aggregator.AnalysisFiles.Output.ParametersJson.json";

                public const string PmmlJson = "Lpp.Scanner.DataMart.Model.Examples.Aggregator.AnalysisFiles.Output.PmmlJson.json";

                /// <summary>
                /// </summary>
                public enum ResponseTypes {
                    PmmlJson,
                    DatasetJson,
                    ParametersJson,
                    AggregatorResponse
                }
            }
        }

        public class Pmml {

            public class Tags {

                public class PMML {
                    public const string TagName = "PMML";

                    public class DataDictionary {
                        public const string DataField = "DataField";
                        public const string TagName = "DataDictionary";
                    }

                    public class GeneralRegressionModel {
                        public const string TagName = "GeneralRegressionModel";

                        /// <summary>
                        /// </summary>
                        public class CovariateList {

                            /// <summary>
                            ///     The tag name
                            /// </summary>
                            public const string TagName = "CovariateList";
                        }

                        /// <summary>
                        /// </summary>
                        public class FactorList {

                            /// <summary>
                            ///     The tag name
                            /// </summary>
                            public const string TagName = "FactorList";
                        }

                        public class MiningField {
                            public const string active = "active";
                            public const string predicted = "predicted";
                            public const string TagName = "MiningField";
                            public const string target = "target";
                        }

                        public class ParamMatrix {
                            public const string TagName = "ParamMatrix";

                            public class PCell {
                                public const string beta = "beta";
                                public const string df = "df";
                                public const string parameterName = "parameterName";
                                public const string TagName = "PCell";
                            }
                        }

                        public class PCovMatrix {
                            public const string pCol = "pCol";
                            public const string pRow = "pRow";
                            public const string TagName = "PCovMatrix";
                            public const string targetCategory = "targetCategory";
                            public const string value = "value";

                            public class PCovCell {
                                public const string TagName = "PCovCell";
                            }
                        }

                        public class PPMatrix {
                            public const string TagName = "PPMatrix";

                            public class PPCell {
                                public const string TagName = "PPCell";
                                public const string value = "value";
                            }
                        }

                        /// <summary>
                        /// </summary>
                        public class RequestParameterListConst {

                            /// <summary>
                            ///     The tag name
                            /// </summary>
                            public const string TagName = "RequestParameterList";
                        }
                    }

                    public class Header {

                        public class Extension {
                            public const string DataSetName = "DataSetName";
                            public const string DataSetSchemaVersion = "DataSetSchemaVersion";
                            public const string Name = "";
                            public const string TagName = "Extension";
                            public const string Type = "Type";
                            public const string Value = "Value";
                        }
                    }

                    /// <summary>
                    /// </summary>
                    public class MiningSchema {

                        /// <summary>
                        ///     The tag name
                        /// </summary>
                        public const string TagName = "MiningSchema";
                    }
                }
            }
        }

        public class Processor {

            public class Input {
                public const string AnalysisDataSet = "Lpp.Scanner.DataMart.Model.Examples.Processor.AnalysisFiles.Analysis DataSet.json";

                public const string AnalysisPmml = "Lpp.Scanner.DataMart.Model.Examples.Processor.AnalysisFiles.Analysis PMML.json";

                public const string CombinedJson = "Lpp.Scanner.DataMart.Model.Examples.Processor.AnalysisFiles.Populated Analysis PMML.json";

                public const string InvalidPopulatedAnalysisPmml = "Lpp.Scanner.DataMart.Model.Examples.Processor.AnalysisFiles.Invalid Populated Analysis PMML.json";

                public const string InvalidResponseJson = "Lpp.Scanner.DataMart.Model.Examples.Processor.AnalysisFiles.Invalid Response.json";

                public const string IterativeAnalysisParameters = "Lpp.Scanner.DataMart.Model.Examples.Processor.AnalysisFiles.Iterative Analysis Parameters.json";

                public const string NonIterativeAnalysisParameters = "Lpp.Scanner.DataMart.Model.Examples.Processor.AnalysisFiles.NonIterative Analysis Parameters.json";

                public const string ProcessorResponseJson = "Lpp.Scanner.DataMart.Model.Examples.Processor.AnalysisFiles.Valid Response.json";

                public const string ValidPopulatedAnalysisPmml = "Lpp.Scanner.DataMart.Model.Examples.Processor.AnalysisFiles.Valid Populated Analysis PMML.json";

                public const string ValidResponseJson = "Lpp.Scanner.DataMart.Model.Examples.Processor.AnalysisFiles.Valid Response.json";

                public const string XmlAsNodes = "Lpp.Scanner.DataMart.Model.Examples.Processor.AnalysisFiles.glmAsNodes.xml";

                public enum SettingsEnum {
                    rLocation,
                    ServiceURL,
                    DanURL
                }
            }

            public class Output {
                public const string GetResponse = "Lpp.Scanner.DataMart.Model.Examples.Processor.HttpResponse.GetResponseResponse_Complete.txt";

                public const string PostResponse = "Lpp.Scanner.DataMart.Model.Examples.Processor.HttpResponse.PostRequestResponse_Complete.txt";

                public const string Status = "Lpp.Scanner.DataMart.Model.Examples.Processor.HttpResponse.Status_Complete.txt";

                public const string Stop = "Lpp.Scanner.DataMart.Model.Examples.Processor.HttpResponse.Stop_Complete.txt";

                /// <summary>
                /// </summary>
                public enum RegExTokens {
                    ResponseXml,
                    NewInstructionsJson,
                    Status
                }

                /// <summary>
                /// </summary>
                public enum ResponseTypes {
                    DataAdapterResponse
                }

                public class WebAdapter {
                    public const string canceled = "canceled";
                    public const string complete = "complete";
                    public const string error = "error";
                    public const string inprogress = "inprogress";
                }
            }
        }
    }
}