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
using Lpp.Scanner.DataMart.Model.Processors.Analysis.Common;
using Lpp.Scanner.DataMart.Model.Processors.Common.Base;
using Lpp.Scanner.DataMart.Model.Processors.Common.Models;
using Lpp.Scanner.DataMart.Model.Processors.Common.Parameters;
using Lpp.Scanner.DataMart.Model.Processors.Common.Parameters.Response;
using Lpp.Scanner.DataMart.Model.Processors.DataSetMapping.Configuration;
using Newtonsoft.Json;
using pSCANNER.DataMart.Model.processor.Analysis;
using pSCANNER.DataMart.Model.processor.Common.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

#endregion Using

namespace Lpp.Scanner.DataMart.Model.Processors.Analysis {

    [Serializable]
    public class ScannerAnalysisModelProcessor : ScannerBase {

        #region Setup

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ScannerAnalysisModelProcessor"/> class.
        /// </summary>
        public ScannerAnalysisModelProcessor() : this(new PScannerProcessorProxy(), new ScannerAnalysisModelMetadata()) { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ScannerAnalysisModelProcessor"/> class.
        /// </summary>
        /// <param name="proxy">The proxy.</param>
        /// <param name="metaDataModel">The meta data model.</param>
        public ScannerAnalysisModelProcessor(ProxyBase proxy, IModelMetadata metaDataModel) : base(proxy, metaDataModel) { }

        #endregion Constructors

        /// <summary>
        ///     Called repeatedly to provide the Model with the specified document contents.
        /// </summary>
        /// <param name="requestId">Request instance id</param>
        /// <param name="documentId">The id of the Document being transferred</param>
        /// <param name="contentStream">Stream pointer to read the document</param>
        public override void RequestDocument(string requestId, string documentId, Stream contentStream) {
            Log.Debug(string.Format("ScannerAnalysisModelProcessor.RequestDocument(RequestId={0}, documentId={1}", requestId, documentId));

            try {
                var doc = DesiredDocuments.First(d => d.DocumentID == documentId);
                using (var reader = new StreamReader(contentStream)) {
                    if (doc.Filename.ToLowerInvariant().Contains("dataset")) {
                        DatasetJson = reader.ReadToEnd();
                    } else if (doc.Filename.ToLowerInvariant().Contains("parameters")) {
                        ParametersJson = reader.ReadToEnd();
                    } else if (doc.Filename.ToLowerInvariant().Contains("pmml")) {
                        PmmlJson = reader.ReadToEnd();
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
        ///     Gets the processor identifier.
        /// </summary>
        /// <returns></returns>
        protected override string GetProcessorId() {
            return "9B30325D-1863-4A1F-A2B3-5C58B52D3CE0";
        }

        protected override void request() {
        }

        /// <summary>
        ///     Gets the name of the data set.
        /// </summary>
        /// <param name="parametersJson">The parameters json.</param>
        /// <returns></returns>
        private static string GetDataSetName(string parametersJson) {
            dynamic parameters = JsonConvert.DeserializeObject(parametersJson);

            dynamic item = parameters["DataSetName"];
            if (item == null) {
                throw new NullReferenceException("DataSetName");
            }

            string retVal = item.ToString();

            if (retVal == string.Empty) {
                throw new NullReferenceException("DataSetName");
            }

            return retVal;
        }

        /// <summary>
        ///     Gets the extensions.
        /// </summary>
        /// <param name="parametersJson">The parameters json.</param>
        /// <returns></returns>
        private static IDictionary<string, Extension> GetExtensions(string parametersJson) {
            var retVal = new Dictionary<string, Extension>();

            dynamic parameters = JsonConvert.DeserializeObject(parametersJson);
            foreach (var extension in parameters) {
                Extension ext = null;
                if (extension.ToString().Contains("PMML.Header.Extension")) {
                    var name = ((string)extension.Name).Split('.').Last();
                    ext = new Extension(name, ((string)extension.Value));
                }
                if (ext != null) {
                    retVal.Add(ext.Name, ext);
                }
            }

            return retVal;
        }

        /// <summary>
        ///     Determines whether the specified parameters json is iterative.
        /// </summary>
        /// <param name="extensions">The parameters json.</param>
        /// <returns></returns>
        private static bool IsIterative(IDictionary<string, Extension> extensions) {
            var retVal = false;

            var type = "Type";
            if (extensions.ContainsKey(type)) {
                var extension = extensions[type];
                retVal = extension.Value.Equals("iteration", StringComparison.InvariantCultureIgnoreCase);
            }

            return retVal;
        }

        public class Extension {

            #region Constructors

            /// <summary>
            ///     Initializes a new instance of the <see cref="Extension"/> class.
            /// </summary>
            /// <param name="name">The name.</param>
            /// <param name="value">The value.</param>
            public Extension(string name, string value) {
                Name = name;
                Value = value;
            }

            #endregion Constructors

            #region Properties

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public string Name { get; private set; }

            /// <summary>
            ///     Gets the value.
            /// </summary>
            /// <value>The value.</value>
            public string Value { get; private set; }

            #endregion Properties
        }

        #endregion Setup

        #region Model Processor Life Cycle Methods

        /// <summary>
        ///     Gets input stream for the specified Document.
        /// </summary>
        /// <param name="requestId">Request instance idr</param>
        /// <param name="documentId">The id of the document being transferred</param>
        /// <param name="contentStream">Stream pointer to a specified Document</param>
        /// <param name="maxSize">Maximum chunk size (returned chunk may be smaller)</param>
        public override void ResponseDocument(string requestId, string documentId, out Stream contentStream, int maxSize) {
            Log.Debug(string.Format("ScannerAnalysisModelProcessor.ResponseDocument(RequestId={0}, documentId={1}{2}", requestId, documentId, ')'));

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

        /// <summary>
        ///     Starts the specified request identifier.
        /// </summary>
        /// <param name="requestId">The request identifier.</param>
        /// <param name="viewSql">if set to <c>true</c> [view SQL].</param>
        /// <exception cref="ModelProcessorError"></exception>
        public override void Start(string requestId, bool viewSql = false) {
            Log.Debug(string.Format("ScannerAnalysisModelProcessor.Start(RequestId={0}, viewSQL={1}{2}", requestId, viewSql, ')'));

            try {
                var parametersJson = ParametersJson;
                var dataSetJson = DatasetJson;
                var pmmlJson = PmmlJson;

                ResponseJson = GetResponse(requestId, parametersJson, dataSetJson, pmmlJson, Settings);

                const string responseDocType = "xml";
                var document = new Document("0", string.Format("application/{0}", responseDocType), string.Format("response.{0}", responseDocType)) { IsViewable = false, Size = ResponseJson.Length };

                ResponseDocuments.Add(new InternalDocument(document, string.Empty, document.Size));

                RequestStatus.Code = RequestStatus.StatusCode.Complete;
                RequestStatus.Message = string.Empty;
            } catch (Exception ex) {
                Log.Debug(ex);

                //if (Status(requestId).Code == RequestStatus.StatusCode.Error) {
                throw new ModelProcessorError(ex.Message, ex);

                //}
            }
        }

        /// <summary>
        ///     Gets the response.
        /// </summary>
        /// <param name="requestId">The request identifier.</param>
        /// <param name="parametersJson">The parameters json.</param>
        /// <param name="dataSetJson">The data set json.</param>
        /// <param name="pmmlJson">The PMML json.</param>
        /// <param name="settings">The settings.</param>
        /// <returns></returns>
        private string GetResponse(string requestId, string parametersJson, string dataSetJson, string pmmlJson, IDictionary<string, object> settings) {
            var extensions = GetExtensions(parametersJson);
            var dataSetName = GetDataSetName(dataSetJson);
            var connection = DataSetConnection.Get(dataSetName);
            var iterative = IsIterative(extensions);

            BaseProxyRequestParameter requestParameter;

            if (iterative) {
                InitializeParameters(dataSetJson, ref parametersJson);
                var rLocation = settings.GetAsString(Constants.Processor.Input.SettingsEnum.rLocation.ToString(), string.Empty);
                requestParameter = new AggregatorClientRequestParameter(requestId, dataSetJson, parametersJson, pmmlJson, connection, rLocation);
            } else {
                var serviceUrl = settings.GetAsString(Constants.Processor.Input.SettingsEnum.DanURL.ToString(), string.Empty);
                requestParameter = new DanRequestParameter(requestId, dataSetJson, parametersJson, pmmlJson, connection, serviceUrl);
            }

            var requestResponse = Proxy.PostRequest(requestParameter);

            string responseJson;

            if (requestResponse is WebResponse) {
                responseJson = ((WebResponse)requestResponse).Response;
            } else {
                responseJson = ((RResponse)requestResponse).Result;
            }

            return responseJson;
        }

        private void InitializeParameters(string dataDictionaryJson, ref string parametersJson) {
            if (parametersJson.Contains("PMML.GeneralRegressionModel.Coefficients") == false) {
                dynamic deserializeObject = JsonConvert.DeserializeObject(dataDictionaryJson);
                dynamic parametersObject = JsonConvert.DeserializeObject(parametersJson);

                //DataDictionary.DataField[0].name
                dynamic dataDictionary = deserializeObject["DataDictionary"];
                dynamic datafields = dataDictionary["DataField"];
                int max = datafields.Count;
                var index = 0;

                parametersObject["PMML.GeneralRegressionModel.Coefficients." + VariableConstants.InterceptConst] = Constants.InitialCoefficient;

                while (index < max) {
                    string name = datafields[index].name;
                    parametersObject[string.Format("PMML.GeneralRegressionModel.Coefficients.{0}", name)] = Constants.InitialCoefficient;
                    index++;
                }

                parametersJson = JsonConvert.SerializeObject(parametersObject);
            }
        }

        #endregion Model Processor Life Cycle Methods
    }
}