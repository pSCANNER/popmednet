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
using System.Diagnostics;
using System.IO;
using Lpp.Scanner.DataMart.Model.Processors.Common.Base;
using Lpp.Scanner.DataMart.Model.Processors.Common.Models;
using Lpp.Scanner.DataMart.Model.Processors.Common.Parameters.Request;
using Lpp.Scanner.DataMart.Model.Processors.Common.Parameters.Response;
using Lpp.Scanner.DataMart.Model.Processors.Common.Processor.Adapers.Base;


#endregion

namespace Lpp.Scanner.DataMart.Model.Processors.Common.R {

    /// <summary>
    /// </summary>
    /// <seealso cref="BaseAdapter" />
    public class RAdapter : RAdapterBase {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="RAdapter" /> class.
        /// </summary>
        /// <param name="responseConversion">The response conversion.</param>
        /// <param name="request">The request.</param>
        /// <param name="parameters">The parameters.</param>
        public RAdapter(Func<string, BaseRequestParameter, object[], string> responseConversion = null, BaseRequestParameter request = null, params object[] parameters) {
            ResponseConversion = responseConversion;
            Parameters = parameters;
            RequestParameter = request;
        }

        #endregion

        #region Properties

        private object[] Parameters { get; set; }

        /// <summary>
        ///     Gets the request parameter.
        /// </summary>
        /// <value>
        ///     The request parameter.
        /// </value>
        private BaseRequestParameter RequestParameter { get; set; }

        /// <summary>
        ///     Gets the response conversion.
        /// </summary>
        /// <value>
        ///     The response conversion.
        /// </value>
        private Func<string, BaseRequestParameter, object[], string> ResponseConversion { get; set; }

        #endregion

        /// <summary>
        ///     Does the post request.
        /// </summary>
        /// <param name="requestParameter">The request parameter.</param>
        /// <returns></returns>
        public override ResponseBase PostRequest(BaseRequestParameter requestParameter) {

            var request = (RRequestParameter) requestParameter;
            Response = new RResponse {
                Status = Constants.ResponseStatus.InProgress,
                Result = runScriptInR(request.Script, request.ExecutionPath, ResponseConversion, RequestParameter, Parameters)
            };
            Response.Status = Constants.ResponseStatus.Complete;
            return Response;

        }

        /// <summary>
        ///     Does the stop request.
        /// </summary>
        /// <param name="requestParameter">The request parameter.</param>
        /// <returns></returns>
        protected override ResponseBase StopRequest(BaseRequestParameter requestParameter) {
            Response.Status = Constants.ResponseStatus.Canceled;
            return Response;
        }

        /// <summary>
        ///     Does the get status.
        /// </summary>
        /// <param name="requestParameter">The request parameter.</param>
        /// <returns></returns>
        protected override ResponseBase GetStatus(BaseRequestParameter requestParameter) {
            return Response;
        }

        /// <summary>
        ///     Does the get response.
        /// </summary>
        /// <param name="requestParameter">The request parameter.</param>
        /// <returns></returns>
        protected override ResponseBase GetResponse(BaseRequestParameter requestParameter) {
            return Response;
        }

        static string _source = "DMC-RAdapter";
        static string _log = "Application";

        /// <summary>
        ///     Runs from command.
        /// </summary>
        /// <param name="script">The r command.</param>
        /// <param name="executionPath">The r path.</param>
        /// <param name="responseConversion">The response conversion.</param>
        /// <param name="requestParameter">The request parameter.</param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        /// <exception cref="System.Exception">R failed to compute. Output:  + result</exception>
        private static string runScriptInR(string script, string executionPath, Func<string, BaseRequestParameter, object[], string> responseConversion, BaseRequestParameter requestParameter, object[] parameters = null) {

            var output = string.Empty;

            lock (typeof(RAdapter)) {

                try {

                    using (var inputFile = TempFile.CreateTempRFile()) {

                        var inputFileName = inputFile.FileInfo.FullName;

                        using (var streamWriter = new StreamWriter(new FileStream(inputFileName, FileMode.Open, FileAccess.Write))) {
                            if (!EventLog.SourceExists(_source))
                            {
                                EventLog.CreateEventSource(_source, _log);
                            }
                            EventLog.WriteEntry(_source, script, EventLogEntryType.Information);
                            streamWriter.Write(script);
                        }

                        var info = new ProcessStartInfo {
                            FileName = string.Format("\"{0}\\Rscript.exe\"", executionPath),
                            WorkingDirectory = executionPath,
                            Arguments = string.Format("\"{0}\"", inputFileName),
                            RedirectStandardInput = false,
                            RedirectStandardOutput = true,
                            UseShellExecute = false,
                            CreateNoWindow = true
                        };


                        using (var proc = new Process()) {
                            proc.StartInfo = info;
                            var start = proc.Start();
                            output = proc.StandardOutput.ReadToEnd();
                            proc.Close();
                        }

                    }

                } catch (Exception ex) {
                    throw new Exception(string.Format("R failed to compute. Output: {0}", output), ex);
                }

            }

            return responseConversion(output, requestParameter, parameters);
        }

    }

}