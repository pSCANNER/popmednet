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
using System.Linq;
using System.Web;
using System.Xml;
using Lpp.Scanner.DataMart.Model.Processors.Common.Models;


#endregion

namespace Lpp.Scanner.DataMart.Model.Processors.Common.Pmml {

    public class PmmlDocument {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PmmlDocument" /> class.
        /// </summary>
        /// <param name="xmlDocument">The XML document.</param>
        protected internal PmmlDocument(XmlDocument xmlDocument) {
            TheXmlDocument = xmlDocument;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the XML document.
        /// </summary>
        /// <value>
        ///     The XML document.
        /// </value>
        public XmlDocument TheXmlDocument { get; private set; }

        #endregion

        /// <summary>
        ///     Gets or sets the mining schema.
        /// </summary>
        /// <value>
        ///     The mining schema.
        /// </value>
        public MiningSchema GetMiningSchema() {
            return new MiningSchema(TheXmlDocument.GetElementsByTagName(Constants.Pmml.Tags.PMML.MiningSchema.TagName)[0]);
        }

        /// <summary>
        ///     Gets the encoded XML.
        /// </summary>
        /// <returns></returns>
        public string GetEncodedXml() {

            var retVal = GetRawXml();

            // UrlEncode
            retVal = HttpUtility.UrlEncode(retVal);
            return retVal;
        }

        /// <summary>
        ///     Gets the raw XML.
        /// </summary>
        /// <returns></returns>
        public string GetRawXml() {
            string retVal;
            using (var stringWriter = new StringWriter()) {
                using (var xmlTextWriter = XmlWriter.Create(stringWriter)) {
                    TheXmlDocument.WriteTo(xmlTextWriter);
                    xmlTextWriter.Flush();
                    retVal = stringWriter.GetStringBuilder().ToString();
                }
            }
            return retVal;
        }

        /// <summary>
        ///     Gets the pp matrix.
        /// </summary>
        /// <returns></returns>
        public PPMatrix GetPPMatrix() {
            var elementsByTagName = TheXmlDocument.GetElementsByTagName(Constants.Pmml.Tags.PMML.GeneralRegressionModel.PPMatrix.TagName);
            var ppMatrixNode = elementsByTagName[0];
            return new PPMatrix(ppMatrixNode);
        }

        /// <summary>
        ///     Gets the parameter matrix.
        /// </summary>
        /// <returns></returns>
        public ParamMatrix GetParamMatrix() {
            return new ParamMatrix(TheXmlDocument.GetElementsByTagName(Constants.Pmml.Tags.PMML.GeneralRegressionModel.ParamMatrix.TagName)[0]);
        }

        /// <summary>
        ///     Gets the data dictionary.
        /// </summary>
        /// <returns></returns>
        public DataDictionary GetDataDictionary() {
            return new DataDictionary(TheXmlDocument.GetElementsByTagName(Constants.Pmml.Tags.PMML.DataDictionary.TagName)[0]);
        }

        /// <summary>
        ///     Gets the request parameter list.
        /// </summary>
        /// <returns></returns>
        public RequestParameterList GetRequestParameterList() {
            var elementsByTagName = TheXmlDocument.GetElementsByTagName(Constants.Pmml.Tags.PMML.GeneralRegressionModel.RequestParameterList.TagName);
            return new RequestParameterList(elementsByTagName);
        }

        /// <summary>
        ///     Gets the previous iterations.
        /// </summary>
        /// <returns></returns>
        public PreviousIteration GetPreviousIterations() {

            var extensions = TheXmlDocument.GetElementsByTagName(Constants.Pmml.Tags.PMML.Header.Extension.TagName);
            var xmlNode = extensions.Cast<XmlNode>().First(x => x.Attributes["name"].Value == "previousIterations");
            return new PreviousIteration(xmlNode);

        }

        /// <summary>
        ///     Gets the link function.
        /// </summary>
        /// <param name="linkFunctionName">Name of the link function.</param>
        /// <returns></returns>
        public string GetLinkFunction(string linkFunctionName) {
            //modelName=""General_Regression_Model"" modelType=""regression"" functionName=""logit"" algorithmName=""glm"" distribution=""normal"" {0}=""identity""
            return TheXmlDocument.GetElementsByTagName(Constants.Pmml.Tags.PMML.GeneralRegressionModel.TagName)[0].Attributes[linkFunctionName].Value;
        }

        public string GetFamily(string family) {
            return "binomial"; //TheXmlDocument.GetElementsByTagName(Constants.Pmml.Tags.PMML.GeneralRegressionModel.TagName)[0].Attributes[family].Value;
        }

    }

}