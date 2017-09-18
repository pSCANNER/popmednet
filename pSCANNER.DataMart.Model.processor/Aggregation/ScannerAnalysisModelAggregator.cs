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

using Lpp.Dns.DataMart.Model;
using Lpp.Dns.DataMart.Model.Settings;
using Lpp.Scanner.DataMart.Model.Processors.Aggregation.Common;
using Lpp.Scanner.DataMart.Model.Processors.Common.Base;
using Lpp.Scanner.DataMart.Model.Processors.Common.Models;
using Lpp.Scanner.DataMart.Model.Processors.Common.Parameters;
using Lpp.Scanner.DataMart.Model.Processors.Common.Processor.Task;
using Newtonsoft.Json;
using pSCANNER.DataMart.Model.processor.Aggregation;
using pSCANNER.DataMart.Model.processor.Common.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

#endregion Using

namespace Lpp.Scanner.DataMart.Model.Processors.Aggregation {

    /// <summary>
    /// </summary>
    /// <seealso cref="Lpp.Scanner.DataMart.Model.Processors.Common.Base.ScannerBase"/>
    [Serializable]
    public class ScannerAnalysisModelAggregator : ScannerBase {

        /// <summary>
        ///     Called repeatedly to provide the Model with the specified document contents.
        /// </summary>
        /// <param name="requestId">Request instance id</param>
        /// <param name="documentId">The id of the Document being transferred</param>
        /// <param name="contentStream">Stream pointer to read the document</param>
        public override void RequestDocument(string requestId, string documentId, Stream contentStream) {
            Log.Debug(string.Format("ScannerAggregationModelProcessor.RequestDocument(RequestId={0}, documentId={1}", requestId, documentId));

            try {
                var doc = DesiredDocuments.First(x => x.DocumentID == documentId);
                using (var reader = new StreamReader(contentStream)) {
                    if (doc.Filename.Contains("parameters.json")) {
                        var docText = reader.ReadToEnd();
                        ParametersJson = docText;
                    } else {
                        AssignPmmlInputDocuments(reader);
                    }
                }
            } catch (Exception ex) {
                Log.Debug(ex.Message, ex);
                RequestStatus.Code = RequestStatus.StatusCode.Error;
                RequestStatus.Message = ex.Message;
                throw;
            }
        }

        /// <summary>
        ///     Gets input stream for the specified Document.
        /// </summary>
        /// <param name="requestId">Request instance idr</param>
        /// <param name="documentId">The id of the document being transferred</param>
        /// <param name="contentStream">Stream pointer to a specified Document</param>
        /// <param name="maxSize">Maximum chunk size (returned chunk may be smaller)</param>
        public override void ResponseDocument(string requestId, string documentId, out Stream contentStream, int maxSize) {
            Log.Debug(string.Format("ScannerStudyModelProcessor:ResponseDocument: RequestId={0}, documentId={1}", requestId, documentId));

            contentStream = null;

            if (documentId == "0") // response JSON string
            {
                contentStream = new MemoryStream(Encoding.UTF8.GetBytes(ResponseJson));

                return;
            }

            var idoc = ResponseDocuments.FirstOrDefault(d => d.Document.DocumentID == documentId);
            if (idoc != null) {
                contentStream = new FileStream(idoc.Path, FileMode.Open);
            }
        }

        //        {
        //  "PMML.Header.Extension.Type": "Iteration",
        //  "PMML.Header.Extension.DataSetName": null,
        //  "PMML.Header.Extension.DataSetSchemaVersion": "1.0",
        //  "PMML.GeneralRegressionModel.modelName": "General_Regression_Model",
        //  "PMML.GeneralRegressionModel.linkFunction": "identity",
        //  "PMML.GeneralRegressionModel.MiningSchema.MiningField.target": "work",
        //  "PMML.GeneralRegressionModel.MiningSchema.MiningField.active": [
        //    "minority",
        //    "jobcat",
        //    "sex",
        //    "age"
        //  ],
        //  "PMML.Header.Extension.MaxIterations": "5"
        //}
        /// <summary>
        ///     Starts the specified request identifier.
        /// </summary>
        /// <param name="requestId">The request identifier.</param>
        /// <param name="viewSql">if set to <c>true</c> [view SQL].</param>
        /// <exception cref="ModelProcessorError">
        ///     Error in response - new Exception("Error in response") or Error in response - new Exception("Error in response") or
        /// </exception>
        /// <exception cref="Exception">Error in response or Error in response</exception>
        public override void Start(string requestId, bool viewSql = false) {
            Log.Debug(string.Format("ScannerAnalysisModelAggregator.Start(RequestId={0}, viewSQL={1}{2}", requestId, viewSql, ')'));

            try {
                var rLocationPath = Settings.GetAsString(Constants.Aggregator.Input.SettingsEnum.rLocation.ToString(), "");

                var pmmlInputDocs = PmmlInputDocs;
                var parametersJson = ParametersJson;

                InitializeParameters(ref parametersJson);
                var siteNames = DesiredDocuments.Select(x => x.Filename).Select(x => x.Split('.')[0]).ToArray();

                var requestParameter = new AggregatorServerRequestParameter(requestId, rLocationPath, pmmlInputDocs, parametersJson, siteNames);

                var responseBase = Proxy.PostRequest(requestParameter);

                if (responseBase.Status != Constants.ResponseStatus.Error) {
                    if (responseBase == null) {
                        throw new ModelProcessorError("Error in response", new Exception("Error in response"));
                    }

                    RequestStatus.Code = RequestStatus.StatusCode.InProgress;

                    while (responseBase.Status == Constants.ResponseStatus.InProgress || responseBase.Status == Constants.ResponseStatus.Undefined) {
                        Thread.Sleep(1000);
                        responseBase = Proxy.GetStatus(requestParameter);
                    }

                    // this should cycle through and load up the document information as well as actual response JSON
                    if (responseBase.Status != Constants.ResponseStatus.InProgress) {
                        ResponseJsonPath = string.Format("{0}{1}.json", Path.GetTempPath(), Guid.NewGuid());

                        AddResponseDocuments(requestParameter);

                        // Set completion status
                        RequestStatus.Code = RequestStatus.StatusCode.Complete;
                        RequestStatus.Message = string.Empty;
                    } else {
                        // I need to figure out the best error response here
                        throw new ModelProcessorError("Error in response", new Exception("Error in response"));
                    }
                } else {
                    Status(requestId).Code = RequestStatus.StatusCode.Error;
                }
            } catch (Exception ex) {
                Log.Debug(ex);

                //while (Statuses(requestId).Code == RequestStatus.StatusCode.InProgress) {
                //    Thread.Sleep(10000);
                //}

                if (Status(requestId).Code == RequestStatus.StatusCode.Error) {
                    throw new ModelProcessorError(ex.Message, ex);
                }
            }
        }

        /// <summary>
        ///     Gets the size of the buffer.
        /// </summary>
        /// <returns></returns>
        protected override int GetBufferSize() {
            return 10000;
        }

        /// <summary>
        ///     Gets the processor identifier.
        /// </summary>
        /// <returns></returns>
        protected override string GetProcessorId() {
            return "BC937771-136C-4883-8D7C-B6CE442B49F5";
        }

        /// <summary>
        ///     Requests this instance.
        /// </summary>
        protected override void request() {
            PmmlInputDocs = new List<string>();
        }

        /// <summary>
        ///     Adds the document.
        /// </summary>
        /// <param name="documentId">The document identifier.</param>
        /// <param name="fileType">Type of the file.</param>
        /// <param name="responseType">Type of the response.</param>
        /// <param name="finalResponse">The final response.</param>
        private void AddDocument(string documentId, string fileType, string responseType, string finalResponse) {
            var mimeType = string.Format("application/{0}", fileType);
            var filename = string.Format("{0}.{1}", responseType.ToLower(), fileType);
            var document = new Document(documentId, mimeType, filename) { IsViewable = false, Size = finalResponse.Length };
            ResponseJson = finalResponse;
            ResponseDocuments.Add(new InternalDocument(document, ResponseJsonPath, document.Size));
        }

        /// <summary>
        ///     Asses the response documents.
        /// </summary>
        /// <param name="requestParameter">The request parameter.</param>
        private void AddResponseDocuments(BaseRequestParameter requestParameter) {
            var response = (AggregatorResponse)Proxy.GetResponse(requestParameter);

            AddDocument("0", "json", "Parameters", response.FinalResponse);
        }

        /// <summary>
        ///     Assigns the PMML input documents.
        /// </summary>
        /// <param name="reader">The reader.</param>
        private void AssignPmmlInputDocuments(TextReader reader) {
            var docs = reader.ReadToEnd();

            var matchCollection = _pmmlCapture.Matches(docs);

            var enumerator = matchCollection.GetEnumerator();
            while (enumerator.MoveNext()) {
                var value = ((Match)enumerator.Current).Captures[0].Value;
                PmmlInputDocs.Add(value);
            }
        }

        /// <summary>
        ///     Initializes the parameters.
        /// </summary>
        /// <param name="parametersJson">The parameters json.</param>
        private void InitializeParameters(ref string parametersJson) {
            if (parametersJson.Contains("PMML.GeneralRegressionModel.Coefficients") == false) {
                dynamic parametersObject = JsonConvert.DeserializeObject(parametersJson);

                var target = parametersObject["PMML.GeneralRegressionModel.MiningSchema.MiningField.target"];
                var interceptTag = string.Format("PMML.GeneralRegressionModel.Coefficients.{0}", VariableConstants.InterceptConst);
                var targetTag = "PMML.GeneralRegressionModel.Coefficients." + target;

                if (parametersObject[targetTag] == null) {
                    parametersObject[interceptTag] = Constants.InitialCoefficient;
                    parametersObject[targetTag] = Constants.InitialCoefficient;
                    var active = parametersObject["PMML.GeneralRegressionModel.MiningSchema.MiningField.active"];
                    int max = active.Count;
                    var index = 0;

                    while (index < max) {
                        parametersObject["PMML.GeneralRegressionModel.Coefficients." + active[index]] = Constants.InitialCoefficient;
                        index++;
                    }

                    parametersJson = JsonConvert.SerializeObject(parametersObject);
                }
            }
        }

        /// <summary>
        ///     Gets or sets the response json path.
        /// </summary>
        /// <value>The response json path.</value>
        public string ResponseJsonPath { get; set; }

        /// <summary>
        ///     Gets the PMML input docs.
        /// </summary>
        /// <value>The PMML input docs.</value>
        private IList<string> PmmlInputDocs { get; set; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ScannerAnalysisModelAggregator"/> class.
        /// </summary>
        public ScannerAnalysisModelAggregator() : this(new PScannerAggregatorProxy<AsyncTask>(), new ScannerAggregationModelMetadata()) { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ScannerAnalysisModelAggregator"/> class.
        /// </summary>
        /// <param name="proxy">The proxy.</param>
        /// <param name="metaDataModel">The meta data model.</param>
        public ScannerAnalysisModelAggregator(ProxyBase proxy, IModelMetadata metaDataModel) : base(proxy, metaDataModel) {
            PmmlInputDocs = new List<string>();
        }

        /// <summary>
        ///     The PMML capture
        /// </summary>
        private static readonly Regex _pmmlCapture = new Regex(@"<\?xml[\s\w="".-]+\?>.+?</PMML>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
    }
}