#region Legal Information

// ====================================================================================
//  
//      Center for Population Health Informatics
//      Solution: Lpp.Adapters
//      Project: Lpp.Scanner.DataMart.Model.Processors
//      Last Updated By: Westerman, Dax Marek
// 
// ====================================================================================

#endregion

#region Using

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using log4net;
using Lpp.Dns.DataMart.Model;
using Lpp.Dns.DataMart.Model.Settings;
using Lpp.Dns.DTO.Scanner;
using Microsoft.Win32;
using Newtonsoft.Json;


#endregion

namespace Lpp.Scanner.DataMart.Model.Processors.Study {

    [Serializable]
    public class ScannerStudyModelProcessor : IModelProcessor {

        private const string PROCESSOR_ID = "594CFFDC-4C19-492E-9A85-147378B28405";
        private const string HtmlTemplate = @"<html>
          <body style=""font-family: Calibri; font-size: font-size: 16px;"">
            <table style=""border-style:solid; border-width: 2px; width: 620px; margin:4px; padding:4px"">
              <tr>
                <td><b>DataMart {0} will support for the following:</b>
                  <table style=""border-style:solid; border-width: 1px; border-color: Gray; width: 600px; text-align:left;"">
                    <thead style=""background-color: #E3E6EB; font-weight: bold; "">
                      <th>Library</th>
                      <th>Method</th>
                      <th>DataSet / DataModel</th>
                      <th>Result Release Method</th>
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

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #region Fields

        private readonly IModelMetadata modelMetadata = new ScannerStudyModelMetadata();
        private IDictionary<string, IDictionary<string, string>> requestProperties; // Key: requestId, Value: Request Properties

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the model metadata.
        /// </summary>
        /// <value>
        ///     The model metadata.
        /// </value>
        public IModelMetadata ModelMetadata {
            get {
                return modelMetadata;
            }
        }

        /// <summary>
        ///     Gets the model processor identifier.
        /// </summary>
        /// <value>
        ///     The model processor identifier.
        /// </value>
        public Guid ModelProcessorId {
            get {
                return Guid.Parse(PROCESSOR_ID);
            }
        }

        public IDictionary<string, object> Settings { get; set; }

        #endregion

        [Serializable]
        internal class ScannerStudyModelMetadata : IModelMetadata {

            private const string MODEL_ID = "D01D1941-240E-411B-BB26-BA063CD463BE"; // ID for Scanner Study model

            #region Fields

            private readonly IDictionary<string, bool> capabilities;
            private readonly IDictionary<string, string> properties;

            #endregion

            #region Constructors

            /// <summary>
            ///     Initializes a new instance of the <see cref="ScanneAnalysisModelMetadata" /> class.
            /// </summary>
            public ScannerStudyModelMetadata() {
                capabilities = new Dictionary<string, bool> {{"IsSingleton", true}, {"RequiresConfig", false}, {"AddFiles", true}, {"CanRunAndUpload", true}, {"CanUploadWithoutRun", true}};
                properties = new Dictionary<string, string> {{"LibraryMethodLocation", "C:\\_LibraryMethodCache"}};
            }

            #endregion

            #region Properties

            /// <summary>
            ///     List of capabilities of the Model.
            /// </summary>
            public IDictionary<string, bool> Capabilities {
                get {
                    return capabilities;
                }
            }

            /// <summary>
            ///     Returns the Model Id.
            /// </summary>
            public Guid ModelId {
                get {
                    return Guid.Parse(MODEL_ID);
                }
            }

            /// <summary>
            ///     Returns the Model Name.
            /// </summary>
            public string ModelName {
                get {
                    return "Scanner Study";
                }
            }

            /// <summary>
            ///     List of properties of the Model. These are properties whose values will be stored by the DataMartClient.
            /// </summary>
            public IDictionary<string, string> Properties {
                get {
                    return properties;
                }
            }

            /// <summary>
            ///     List of the properties the processor needs.
            /// </summary>
            public ICollection<ProcessorSetting> Settings {
                get {
                    var settings = new List<ProcessorSetting>();
                    settings.Add(new ProcessorSetting {Title = "Library/Method Location", Key = "LibraryMethodLocation", DefaultValue = "C:\\_LibraryMethodCache", ValueType = typeof(string), Required = true});
                    return settings;
                }
            }

            /// <summary>
            ///     Gets the type of sql providers the processor supports.
            /// </summary>
            public IEnumerable<SQLProvider> SQlProviders {
                get {
                    return Enumerable.Empty<SQLProvider>();
                }
            }

            /// <summary>
            ///     Returns the Model Version.
            /// </summary>
            public string Version {
                get {
                    return "0.1";
                }
            }

            #endregion
        }

        #region Private Methods

        private string GetMimeType(string fileName) {
            var mimeType = "application/octet-stream";
            var ext = Path.GetExtension(fileName).ToLower();
            var regKey = Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null) {
                mimeType = regKey.GetValue("Content Type").ToString();
            }
            return mimeType;
        }

        #endregion

        private string SerializeSiteProtocolsToHTML(ScannerStudySiteProtocolsDTO siteProtocols) {
            var sb = new StringBuilder();
            sb.AppendLine("");
            foreach (var protocol in siteProtocols.Protocols) {
                sb.AppendLine("                <tr>" + "<td>" + WebUtility.HtmlEncode(protocol.LibraryName) + "</td>" + "<td>" + WebUtility.HtmlEncode(protocol.MethodName) + "</td>" + "<td>" + WebUtility.HtmlEncode(protocol.DataSetName) + "</td>" + "<td>" + WebUtility.HtmlEncode(protocol.ResultReleaseName) + "</td>" + "</tr>");
            }
            sb.AppendLine("");
            var studyProtocols = sb.ToString().TrimEnd();

            return string.Format(HtmlTemplate, /* 0 */ requestDataMartName, /* 1 */ studyProtocols);
        }

        #region Model Processor Life Cycle Methods

        public class InternalDocument {
            #region Constructors

            public InternalDocument(Document Doc, string Path, long Size) {
                document = Doc;
                document.Size = Convert.ToInt32(Size);
                path = Path;
                size = Size;
            }

            #endregion

            #region Properties

            public Document document { get; set; }

            public string path { get; set; }

            public long size { get; set; }

            #endregion
        }

        private Guid requestTypeId = Guid.Empty;
        private string requestDataMartName = "";
        private Document[] desiredDocuments;
        private readonly RequestStatus status = new RequestStatus(RequestStatus.StatusCode.Pending);
        private string requestJson;
        private string responseJson;
        private string responseHtml;
        private readonly List<InternalDocument> responseDocuments = new List<InternalDocument>();

        /// <summary>
        ///     Associates the request properties for the specified request.
        /// </summary>
        /// <param name="requestId">Values set by user for the model's properties</param>
        /// <param name="requestProperties">Values of properties associated with the request</param>
        public void SetRequestProperties(string requestId, IDictionary<string, string> requestProperties) {
            if (this.requestProperties == null) {
                this.requestProperties = new Dictionary<string, IDictionary<string, string>>();
            }

            if (requestProperties == null) {
                return;
            }

            if (!this.requestProperties.ContainsKey(requestId)) {
                this.requestProperties.Add(requestId, requestProperties);
            } else {
                this.requestProperties[requestId] = requestProperties;
            }
        }

        public void Request(string requestId, NetworkConnectionMetadata network, RequestMetadata md, Document[] requestDocuments, out IDictionary<string, string> requestProperties, out Document[] desiredDocuments) {
            log.Debug("ScannerStudyModelProcessor:Request: RequestId=" + requestId + ", doc count=" + (requestDocuments == null ? 0 : requestDocuments.Length));

            requestTypeId = new Guid(md.RequestTypeId);
            requestDataMartName = md.DataMartName;
            requestProperties = null;
            desiredDocuments = requestDocuments;
            this.desiredDocuments = requestDocuments;
            status.Code = RequestStatus.StatusCode.InProgress;
            status.Message = "";
        }

        /// <summary>
        ///     Called repeatedly to provide the Model with the specified document contents.
        /// </summary>
        /// <param name="requestId">Request instance id</param>
        /// <param name="documentId">The id of the Document being transferred</param>
        /// <param name="contentStream">Stream pointer to read the document</param>
        public void RequestDocument(string requestId, string documentId, Stream contentStream) {
            log.Debug("ScannerStudyModelProcessor:RequestDocument: RequestId=" + requestId + ", documentId=" + documentId);

            var doc = desiredDocuments.FirstOrDefault(d => d.DocumentID == documentId);
            if (doc.Filename.ToLowerInvariant().Contains("study participation request")) {
                using (var reader = new StreamReader(contentStream)) {
                    requestJson = reader.ReadToEnd();
                    log.Debug("ScannerStudyModelProcessor:RequestDocument: RequestId=" + requestId + ", doc.Filename=" + doc.Filename + ", requestJson=" + requestJson);
                }
            }
        }

        /// <summary>
        ///     Starts the specified request identifier.
        /// </summary>
        /// <param name="requestId">The request identifier.</param>
        /// <param name="viewSql">if set to <c>true</c> [view SQL].</param>
        /// <exception cref="ModelProcessorError"></exception>
        public void Start(string requestId, bool viewSql) {
            log.Debug("ScannerStudyModelProcessor:Start: RequestId=" + requestId);

            try {
                var libraryMethodLocation = Settings.GetAsString("LibraryMethodLocation", "");
                log.Debug("ScannerStudyModelProcessor:Start: RequestId=" + requestId + ", LibraryMethodLocation=" + libraryMethodLocation);

                status.Code = RequestStatus.StatusCode.InProgress;

                // Note: for testing send back the same protocols requested.
                var startIndex = requestJson.IndexOf("\"StudyProtocols\": [");
                if (startIndex < 0) {
                    responseJson = "{ \"Protocols\": [] }";
                } else {
                    responseJson = requestJson.Substring(startIndex);
                    responseJson = responseJson.Replace("\"StudyProtocols\": [", "{ \"Protocols\": [");
                }

                log.Debug("ScannerStudyModelProcessor:Start: RequestId=" + requestId + ", responseJson=" + responseJson);

                // Add the JSON response document
                var document = new Document("0", "application/json", "response.json");
                document.IsViewable = false;
                document.Size = responseJson.Length;

                responseDocuments.Add(new InternalDocument(document, "", document.Size));

                // Add the HTML response document
                var siteProtocols = JsonConvert.DeserializeObject<ScannerStudySiteProtocolsDTO>(responseJson);
                responseHtml = SerializeSiteProtocolsToHTML(siteProtocols);

                //var htmlDocument = new Document("1", "text/html", "response.html");
                //htmlDocument.IsViewable = true;
                //htmlDocument.Size = responseHtml.Length;

                //responseDocuments.Add(new InternalDocument(htmlDocument, "", htmlDocument.Size));

                status.Code = RequestStatus.StatusCode.Complete;
                status.Message = "";
            } catch (Exception e) {
                status.Code = RequestStatus.StatusCode.Error;
                status.Message = e.Message;
                throw e;
            }
        }

        public void Stop(string requestId, StopReason reason) {
            log.Debug("ScannerStudyModelProcessor:Statuses: StopRequest=" + requestId + ", reason=" + reason);
        }

        /// <summary>
        ///     Return current status of request.
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns>
        ///     RequestStatus denoting the state of the request
        /// </returns>
        public RequestStatus Status(string requestId) {
            log.Debug("ScannerStudyModelProcessor:Statuses: RequestId=" + requestId);
            return status;
        }

        /// <summary>
        ///     Returns information about the list of PmmlInputDocuments that can be returned.
        ///     Called when RequestStatus is Complete. Does not return actual contents.
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns>
        ///     List of response PmmlInputDocuments
        /// </returns>
        /// <exception cref="ModelProcessorError"></exception>
        public Document[] Response(string requestId) {
            log.Debug("ScannerStudyModelProcessor:Response: RequestId=" + requestId);
            return responseDocuments.Select(idoc => idoc.document).ToArray();
        }

        /// <summary>
        ///     Appends a file to the list of response PmmlInputDocuments.
        /// </summary>
        /// <param name="requestId">Request instance id</param>
        /// <param name="filePath">Local path to the file to attach</param>
        public void AddResponseDocument(string requestId, string filePath) {
            log.Debug("ScannerStudyModelProcessor:AddResponseDocument: RequestId=" + requestId + ", filePath=" + filePath);

            try {
                var document = new Document(Guid.NewGuid().ToString(), GetMimeType(filePath), Path.GetFileName(filePath));
                var fileInfo = new FileInfo(filePath);
                responseDocuments.Add(new InternalDocument(document, filePath, fileInfo.Length));
            } catch (Exception ex) {
                log.Debug(ex);
                throw new ModelProcessorError(ex.Message, ex);
            }

            status.Code = RequestStatus.StatusCode.AwaitingResponseApproval;
        }

        /// <summary>
        ///     Removes the response document.
        /// </summary>
        /// <param name="requestId">The request identifier.</param>
        /// <param name="filePath">The file path.</param>
        public void RemoveResponseDocument(string requestId, string documentId) {
            log.Debug("ScannerStudyModelProcessor:RemoveResponseDocument: RequestId=" + requestId + ", documentId=" + documentId);

            try {
                IList<Document> documents = new List<Document>();
                foreach (var idoc in responseDocuments) {
                    if (idoc.document.DocumentID == documentId) {
                        responseDocuments.Remove(idoc);
                        break;
                    }
                }
            } catch (Exception ex) {
                log.Debug(ex);
                throw new ModelProcessorError(ex.Message, ex);
            }
            status.Code = (responseDocuments == null || responseDocuments.Count == 0) ? RequestStatus.StatusCode.Pending : RequestStatus.StatusCode.AwaitingResponseApproval;
        }

        /// <summary>
        ///     Gets input stream for the specified Document.
        /// </summary>
        /// <param name="requestId">Request instance idr</param>
        /// <param name="documentId">The id of the document being transferred</param>
        /// <param name="contentStream">Stream pointer to a specified Document</param>
        /// <param name="maxSize">Maximum chunk size (returned chunk may be smaller)</param>
        public void ResponseDocument(string requestId, string documentId, out Stream contentStream, int maxSize) {
            log.Debug("ScannerStudyModelProcessor:ResponseDocument: RequestId=" + requestId + ", documentId=" + documentId);

            contentStream = null;

            if (documentId == "0") // response JSON string
            {
                contentStream = new MemoryStream(Encoding.UTF8.GetBytes(responseJson));

                return;
            }

            if (documentId == "1") // response HTML string
            {
                contentStream = new MemoryStream(Encoding.UTF8.GetBytes(responseHtml));

                return;
            }

            var idoc = responseDocuments.FirstOrDefault(d => d.document.DocumentID == documentId);
            if (idoc != null) {
                contentStream = new FileStream(idoc.path, FileMode.Open);
            }
        }

        /// <summary>
        ///     Closes the specified request. Local and memory resident data for the request will be cleaned up.
        ///     Closed request cannot be restarted.
        /// </summary>
        /// <param name="requestId">Request instance id</param>
        /// <exception cref="NotImplementedException"></exception>
        public void Close(string requestId) {
            log.Debug("ScannerStudyModelProcessor:Close: RequestId=" + requestId);
        }

        /// <summary>
        ///     Runs the post processor. This method is called by the model processor only if the status returned
        ///     has a message to be displayed and the user responded "yes".
        /// </summary>
        /// <param name="requestId"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void PostProcess(string requestId) {
            log.Debug("ScannerStudyModelProcessor:PostProcess: RequestId=" + requestId);
        }

        #endregion
    }

}