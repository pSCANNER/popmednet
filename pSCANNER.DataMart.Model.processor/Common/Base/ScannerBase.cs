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

using log4net;
using Lpp.Dns.DataMart.Model;
using Microsoft.Win32;
using pSCANNER.DataMart.Model.processor.Common.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

#endregion Using

namespace Lpp.Scanner.DataMart.Model.Processors.Common.Base {

    public abstract class ScannerBase : IModelProcessor {

        /// <summary>
        ///     Appends a file to the list of response PmmlInputDocuments.
        /// </summary>
        /// <param name="requestId">Request instance id</param>
        /// <param name="filePath">Local path to the file to attach</param>
        public void AddResponseDocument(string requestId, string filePath) {
            _log.Debug("ScannerAnalysisModelAggregator.AddResponseDocument(RequestId=" + requestId + ", filePath=" + filePath + ')');

            try {
                var document = new Document(Guid.NewGuid().ToString(), GetMimeType(filePath), Path.GetFileName(filePath));
                var fileInfo = new FileInfo(filePath);
                _responseDocuments.Add(new InternalDocument(document, filePath, fileInfo.Length));
            } catch (Exception ex) {
                _log.Debug(ex);
                throw new ModelProcessorError(ex.Message, ex);
            }

            _requestStatus.Code = RequestStatus.StatusCode.AwaitingResponseApproval;
        }

        /// <summary>
        ///     Closes the specified request. Local and memory resident data for the request will be cleaned up. Closed request cannot be restarted.
        /// </summary>
        /// <param name="requestId">Request instance id</param>
        /// <exception cref="NotImplementedException"></exception>
        public void Close(string requestId) {
            _log.Debug("ScannerAnalysisModelAggregator.Close: RequestId=" + requestId + ')');
        }

        /// <summary>
        ///     Runs the post processor. This method is called by the model processor only if the status returned has a message to be displayed and the user
        ///     responded "yes".
        /// </summary>
        /// <param name="requestId"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void PostProcess(string requestId) {
            _log.Debug(string.Format("ScannerAnalysisModelAggregator.PostProcess: RequestId={0}{1}", requestId, ')'));
        }

        /// <summary>
        ///     Removes a file from the list of response Documents.
        /// </summary>
        /// <param name="requestId">Request instance id</param>
        /// <param name="documentId"></param>
        /// <exception cref="ModelProcessorError"></exception>
        public void RemoveResponseDocument(string requestId, string documentId) {
            _log.Debug("ScannerAnalysisModelAggregator.RemoveResponseDocument(RequestId=" + requestId + ", documentId=" + documentId + ')');

            try {
                IList<Document> documents = new List<Document>();
                foreach (var idoc in _responseDocuments) {
                    if (idoc.Document.DocumentID == documentId) {
                        _responseDocuments.Remove(idoc);
                        break;
                    }
                }
            } catch (Exception ex) {
                _log.Debug(ex);
                throw new ModelProcessorError(ex.Message, ex);
            }
            _requestStatus.Code = (_responseDocuments == null || _responseDocuments.Count == 0) ? RequestStatus.StatusCode.Pending : RequestStatus.StatusCode.AwaitingResponseApproval;
        }

        /// <summary>
        ///     Requests the specified request identifier.
        /// </summary>
        /// <param name="requestId">The request identifier.</param>
        /// <param name="network">The network.</param>
        /// <param name="md">The md.</param>
        /// <param name="requestDocuments">The request documents.</param>
        /// <param name="requestProperties">The request properties.</param>
        /// <param name="desiredDocuments">The desired documents.</param>
        /// <exception cref="ModelProcessorError"></exception>
        public void Request(string requestId
                                    , NetworkConnectionMetadata network
                                    , RequestMetadata md
                                    , Document[] requestDocuments
                                    , out IDictionary<string, string> requestProperties
                                    , out Document[] desiredDocuments) {
            if (string.IsNullOrEmpty(requestId) || string.IsNullOrWhiteSpace(requestId)) {
                throw new ArgumentException("RequestId");
            }

            if (network == null) {
                throw new ArgumentException("NetworkConnectionMetadata");
            }

            if (requestDocuments == null || requestDocuments.Count() == 0) {
                throw new ArgumentException("Document[]");
            }

            if (md == null) {
                throw new ArgumentException("RequestMetadata");
            }

            _log.Debug("ScannerAnalysisModelAggregator.Request(RequestId=" + requestId + "...) documentCount=" + (requestDocuments == null ? 0 : requestDocuments.Length));

            try {
                if (md != null) {
                    RequestTypeId = new Guid(md.RequestTypeId);
                }
                requestProperties = null;
                desiredDocuments = requestDocuments;
                DesiredDocuments = requestDocuments;
                _requestStatus.Code = RequestStatus.StatusCode.InProgress;
                _requestStatus.Message = "";
                request();
            } catch (Exception ex) {
                _log.Debug(ex);
                throw new ModelProcessorError(ex.Message, ex);
            }
        }

        /// <summary>
        ///     Called repeatedly to provide the Model with the specified document contents.
        /// </summary>
        /// <param name="requestId">Request instance id</param>
        /// <param name="documentId">The id of the Document being transferred</param>
        /// <param name="contentStream">Stream pointer to read the document</param>
        public abstract void RequestDocument(string requestId, string documentId, Stream contentStream);

        /// <summary>
        ///     Returns information about the list of PmmlInputDocuments that can be returned. Called when RequestStatus is Complete. Does not return actual contents.
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns>List of response PmmlInputDocuments</returns>
        /// <exception cref="ModelProcessorError"></exception>
        public Document[] Response(string requestId) {
            _log.Debug("ScannerAnalysisModelAggregator.Response(RequestId=" + requestId + ')');
            return _responseDocuments.Select(idoc => idoc.Document).ToArray();
        }

        /// <summary>
        ///     Gets input stream for the specified Document.
        /// </summary>
        /// <param name="requestId">Request instance idr</param>
        /// <param name="documentId">The id of the document being transferred</param>
        /// <param name="contentStream">Stream pointer to a specified Document</param>
        /// <param name="maxSize">Maximum chunk size (returned chunk may be smaller)</param>
        public abstract void ResponseDocument(string requestId, string documentId, out Stream contentStream, int maxSize);

        /// <summary>
        ///     Associates the request properties for the specified request.
        /// </summary>
        /// <param name="requestId">Values set by user for the model's properties</param>
        /// <param name="requestProperties">Values of properties associated with the request</param>
        public void SetRequestProperties(string requestId, IDictionary<string, string> requestProperties) {
            if (_requestProperties == null) {
                _requestProperties = new Dictionary<string, IDictionary<string, string>>();
            }

            if (requestProperties == null) {
                return;
            }

            if (!_requestProperties.ContainsKey(requestId)) {
                _requestProperties.Add(requestId, requestProperties);
            } else {
                _requestProperties[requestId] = requestProperties;
            }
        }

        /// <summary>
        ///     Corresponds to IModelProcessor's Start method. Tells the web service that all request Documents are transferred and that it may begin processing.
        /// </summary>
        /// <param name="requestId">Identifies this request for subsequent calls</param>
        /// <param name="viewSql">Denotes the generated SQL should be returned as a result</param>
        public abstract void Start(string requestId, bool viewSql = false);

        /// <summary>
        ///     Return current status of request.
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns>RequestStatus denoting the state of the request</returns>
        public RequestStatus Status(string requestId) {
            _log.Debug("ScannerAnalysisModelAggregator.Statuses(RequestId=" + requestId + ')');
            return _requestStatus;
        }

        /// <summary>
        ///     Stops a request. Multiple calls may be made and the processor implementation should be able to handle or ignore redundant stop calls.
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="reason"></param>
        /// <exception cref="ModelProcessorError"></exception>
        public void Stop(string requestId, StopReason reason) {
            _log.Debug("ScannerAnalysisModelAggregator.StopRequest(RequestId=" + requestId + ')');
            try {
                if (_requestProperties == null || !_requestProperties.ContainsKey(requestId) || !_requestProperties[requestId].ContainsKey("RequestToken")) {
                    return;
                }

                var requestToken = _requestProperties[requestId]["RequestToken"];
            } catch (Exception ex) {
                _log.Debug(ex);
                throw new ModelProcessorError(ex.Message, ex);
            }
        }

        /// <summary>
        ///     Stops a request. Multiple calls may be made and the processor implementation should be able to handle or ignore redundant StopConst calls.
        /// </summary>
        /// <param name="requestParameter">The request parameter.</param>
        /// <exception cref="ModelProcessorError"></exception>
        public void Stop(BaseRequestParameter requestParameter) {
            Stop(requestParameter.RequestId, StopReason.Shutdown);
            Proxy.Stop(requestParameter);
        }

        /// <summary>
        ///     Gets the type of the MIME.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        protected static string GetMimeType(string fileName) {
            var mimeType = "application/octet-stream";
            var ext = Path.GetExtension(fileName).ToLower();
            var regKey = Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null) {
                mimeType = regKey.GetValue("Content Type").ToString();
            }
            return mimeType;
        }

        /// <summary>
        ///     Gets the size of the buffer.
        /// </summary>
        /// <returns></returns>
        protected virtual int GetBufferSize() {
            return 1000;
        }

        /// <summary>
        ///     Gets the processor identifier.
        /// </summary>
        /// <returns></returns>
        protected abstract string GetProcessorId();

        protected abstract void request();

        /// <summary>
        ///     Gets the model metadata.
        /// </summary>
        /// <value>The model metadata.</value>
        public IModelMetadata ModelMetadata {
            get {
                return _modelMetadata;
            }
        }

        /// <summary>
        ///     Gets the model processor identifier.
        /// </summary>
        /// <value>The model processor identifier.</value>
        public Guid ModelProcessorId {
            get {
                return Guid.Parse(GetProcessorId());
            }
        }

        /// <summary>
        ///     Gets or sets the settings.
        /// </summary>
        /// <value>The settings.</value>
        public IDictionary<string, object> Settings { get; set; }

        /// <summary>
        ///     Gets the log.
        /// </summary>
        /// <value>The log.</value>
        protected static ILog Log {
            get {
                return _log;
            }
        }

        /// <summary>
        ///     Gets or sets the dataset json.
        /// </summary>
        /// <value>The dataset json.</value>
        protected string DatasetJson { get; set; }

        /// <summary>
        ///     Gets or sets the desired documents.
        /// </summary>
        /// <value>The desired documents.</value>
        protected Document[] DesiredDocuments { get; set; }

        /// <summary>
        ///     Gets or sets the parameters json.
        /// </summary>
        /// <value>The parameters json.</value>
        protected string ParametersJson { get; set; }

        /// <summary>
        ///     Gets or sets the PMML json.
        /// </summary>
        /// <value>The PMML json.</value>
        protected string PmmlJson { get; set; }

        /// <summary>
        ///     Gets the proxy.
        /// </summary>
        /// <value>The proxy.</value>
        protected ProxyBase Proxy { get; private set; }

        /// <summary>
        ///     Gets the request status.
        /// </summary>
        /// <value>The request status.</value>
        protected RequestStatus RequestStatus {
            get {
                return _requestStatus;
            }
        }

        /// <summary>
        ///     Gets or sets the request statuses.
        /// </summary>
        /// <value>The request statuses.</value>
        protected IDictionary<string, RequestStatus> RequestStatuses {
            get {
                return _requestStatuses;
            }
            set {
                _requestStatuses = value;
            }
        }

        /// <summary>
        ///     Gets or sets the request type identifier.
        /// </summary>
        /// <value>The request type identifier.</value>
        protected Guid RequestTypeId { get; set; }

        /// <summary>
        ///     Gets the response documents.
        /// </summary>
        /// <value>The response documents.</value>
        protected List<InternalDocument> ResponseDocuments {
            get {
                return _responseDocuments;
            }
        }

        /// <summary>
        ///     Gets or sets the response json.
        /// </summary>
        /// <value>The response json.</value>
        protected string ResponseJson { get; set; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ScannerBase"/> class.
        /// </summary>
        /// <param name="proxy">The proxy.</param>
        /// <param name="metaDataModel">The meta data model.</param>
        protected ScannerBase(ProxyBase proxy, IModelMetadata metaDataModel) {
            Proxy = proxy;
            _modelMetadata = metaDataModel;
        }

        protected const string HtmlTemplate = @"<html>
          <body style=""font-family: Calibri; font-size: font-size: 16px;"">
            <table style=""border-style:solid; border-width: 2px; width: 620px; margin:4px; padding:4px"">
              <tr>
                <td><b>DataMart {0} produced the following results:</b>
                  <table style=""border-style:solid; border-width: 1px; border-color: Gray; width: 600px; text-align:left;"">
                    <thead style=""background-color: #E3E6EB; font-weight: bold; "">
                      <th>Label</th>
                      <th>Value</th>
                    </thead>
                    <tbody>
                    {1}
                    </tbody>
                  </table>
                </td>
              </tr>
            </table>
          </body>
        </html>";

        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IModelMetadata _modelMetadata;
        private readonly RequestStatus _requestStatus = new RequestStatus();
        private readonly List<InternalDocument> _responseDocuments = new List<InternalDocument>();
        private IDictionary<string, IDictionary<string, string>> _requestProperties; // Key: requestId, Value: Request Properties
        private IDictionary<string, RequestStatus> _requestStatuses = new Dictionary<string, RequestStatus>();
    }
}