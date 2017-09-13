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

using System.IO;
using System.Reflection;
using Lpp.Dns.DataMart.Model;


#endregion

namespace Lpp.Scanner.DataMart.Model.Processors.Common.Models {

    /// <summary>
    /// </summary>
    public static class Helper {

        public const string RequestId = "1";

        /// <summary>
        ///     Gets the study response.
        /// </summary>
        /// <returns></returns>
        public static string GetValidStudyResponse(int documentIndex) {
            string jsonDocument;

            readInResource("response.json", Constants.Processor.Input.ValidResponseJson, documentIndex, out jsonDocument);

            return jsonDocument;
        }

        /// <summary>
        ///     Gets the invalid study response.
        /// </summary>
        /// <returns></returns>
        public static string GetInvalidStudyResponse(int documentIndex) {
            string jsonDocument;

            readInResource("response.json", Constants.Processor.Input.InvalidResponseJson, documentIndex, out jsonDocument);

            return jsonDocument;
        }

        /// <summary>
        ///     Loads the analysis data set.
        /// </summary>
        /// <returns></returns>
        public static Document LoadAnalysisDataSet(int documentIndex, out string jsonDocument) {
            return readInResource("dataset.json", Constants.Processor.Input.AnalysisDataSet, documentIndex, out jsonDocument);
        }

        /// <summary>
        ///     Gets the json stream.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <returns></returns>
        public static Stream GetJsonStream(string resource) {
            var assembly = Assembly.GetExecutingAssembly();
            var manifestResourceStream = getManifestResourceStream(resource, assembly);
            return manifestResourceStream;
        }

        /// <summary>
        ///     Loads the analysis parameters.
        /// </summary>
        /// <returns></returns>
        public static Document LoadAnalysisParameters(int documentIndex, out string jsonDocument) {
            return readInResource("parameters.json", Constants.Processor.Input.NonIterativeAnalysisParameters, documentIndex, out jsonDocument);
        }

        /// <summary>
        ///     Loads the analysis PMML.
        /// </summary>
        /// <param name="documentIndex"></param>
        /// <param name="jsonDocument">The json document.</param>
        /// <returns></returns>
        public static Document LoadAnalysisPmml(int documentIndex, out string jsonDocument) {
            return readInResource("pmml.json", Constants.Processor.Input.AnalysisPmml, documentIndex, out jsonDocument);
        }

        /// <summary>
        ///     Gets the XML stream.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <returns></returns>
        public static Stream GetXmlStream(string resource) {
            var assembly = Assembly.GetExecutingAssembly();
            var manifestResourceStream = getManifestResourceStream(resource, assembly);
            return manifestResourceStream;
        }

        /// <summary>
        ///     Gets the populated analysis PMML.
        /// </summary>
        /// <returns></returns>
        public static string GetPopulatedAnalysisPmml() {
            string jsonDocument;

            ReadInResource(Constants.Processor.Input.CombinedJson, out jsonDocument);

            return jsonDocument;
        }

        /// <summary>
        ///     Loads the analysis data set.
        /// </summary>
        /// <returns></returns>
        public static Document LoadAnalysisDataSet(out string jsonDocument) {
            return ReadInResource(Constants.Processor.Input.AnalysisDataSet, out jsonDocument);
        }

        /// <summary>
        ///     Gets the json stream.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <returns></returns>
        public static Stream GetResourceStream(string resource) {
            var assembly = Assembly.GetExecutingAssembly();
            var manifestResourceStream = assembly.GetManifestResourceStream(resource);
            return manifestResourceStream;
        }

        /// <summary>
        ///     Reads the in resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="jsonDocument"></param>
        /// <returns></returns>
        public static Document ReadInResource(string resource, out string jsonDocument) {
            jsonDocument = ReadInText(resource);

            var doc = new Document("0", "text/plain", "JSON Text") {IsViewable = true, Size = jsonDocument.Length};

            return doc;
        }

        /// <summary>
        ///     Reads the in text.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <returns></returns>
        public static string ReadInText(string resource) {
            string text;
            using (var textStreamReader = getStreamReader(resource)) {
                text = textStreamReader.ReadToEnd();
            }
            return text;
        }

        /// <summary>
        ///     Loads the analysis parameters.
        /// </summary>
        /// <returns></returns>
        public static Document LoadAnalysisParameters(out string jsonDocument) {
            return ReadInResource(Constants.Processor.Input.NonIterativeAnalysisParameters, out jsonDocument);
        }

        /// <summary>
        ///     Loads the analysis PMML.
        /// </summary>
        /// <param name="jsonDocument">The json document.</param>
        /// <returns></returns>
        public static Document LoadAnalysisPmml(out string jsonDocument) {
            return ReadInResource(Constants.Processor.Input.AnalysisPmml, out jsonDocument);
        }

        /// <summary>
        ///     Gets the json stream reader.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <returns></returns>
        private static StreamReader getJsonStreamReader(string resource) {
            var manifestResourceStream = GetJsonStream(resource);
            var textStreamReader = new StreamReader(manifestResourceStream);
            return textStreamReader;
        }

        /// <summary>
        ///     Reads the in resource.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="resource">The resource.</param>
        /// <param name="documentIndex">Index of the document.</param>
        /// <param name="jsonDocument">The json document.</param>
        /// <returns></returns>
        private static Document readInResource(string name, string resource, int documentIndex, out string jsonDocument) {
            jsonDocument = ReadInText(resource);

            var doc = new Document(documentIndex.ToString(), "text/plain", name) {IsViewable = true, Size = jsonDocument.Length};

            return doc;
        }

        /// <summary>
        ///     Gets the manifest resource stream.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="assembly">The assembly.</param>
        /// <returns></returns>
        private static Stream getManifestResourceStream(string resource, Assembly assembly) {
            return assembly.GetManifestResourceStream(resource);
        }

        /// <summary>
        ///     Gets the json stream reader.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <returns></returns>
        private static StreamReader getStreamReader(string resource) {
            var manifestResourceStream = GetResourceStream(resource);
            var textStreamReader = new StreamReader(manifestResourceStream);
            return textStreamReader;
        }

    }

}