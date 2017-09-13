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
using System.Xml;
using Newtonsoft.Json;


#endregion

namespace Lpp.Scanner.DataMart.Model.Processors.Common.Pmml {

    public static class PmmlParser {

        /// <summary>
        ///     Creates the specified PMML request.
        /// </summary>
        /// <param name="pmmlRequest">The PMML request.</param>
        /// <returns></returns>
        public static PmmlDocument CreateFromJson(string pmmlRequest) {
            var xmlDocument = JsonConvert.DeserializeXmlNode(pmmlRequest);
            var fromXmlDocument = CreateFromXmlDocument(xmlDocument);
#if DEBUG
            var theXml = fromXmlDocument.GetRawXml();
#endif
            return fromXmlDocument;
        }

        public static PmmlDocument GetFromXmlString(string coefficientInput) {
            var doc = new XmlDocument();
            doc.LoadXml(coefficientInput);
            var fromXmlDocument = doc.CreateFromXmlDocument();
            return fromXmlDocument;
        }

        /// <summary>
        ///     Creates from XML.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <returns></returns>
        public static PmmlDocument CreateFromXmlDocument(this XmlDocument doc) {
            doc.migratePmmlNodesToAttributes();

            return new PmmlDocument(doc);
        }

        /// <summary>
        ///     Migrates the PMML nodes to attributes.
        /// </summary>
        /// <param name="xmlDocument">The XML document.</param>
        /// <exception cref="System.NullReferenceException">PMML node does not exist.</exception>
        private static void migratePmmlNodesToAttributes(this XmlDocument xmlDocument) {

            var pmmlElement = PmmlElement.ExtractPmmlElement(xmlDocument);

            if (pmmlElement == null) {
                throw new NullReferenceException("PMML node does not exist.");
            }

            pmmlElement.updatePmml();

            pmmlElement.updateHeader();

            pmmlElement.updateDataDictionary();

            pmmlElement.updateGeneralRegressionModel();
        }

        /// <summary>
        ///     Updates the PMML.
        /// </summary>
        /// <param name="pmmlElement">The PMML element.</param>
        private static void updatePmml(this PmmlElement pmmlElement) {
            pmmlElement.Element.migrateChildNodeToAttribute("version");
            pmmlElement.Element.removeChildNode("xmlns");
            pmmlElement.Element.removeChildNode("xmlns:xsi");
            pmmlElement.Element.removeChildNode("xsi:schemaLocation");
        }

        /// <summary>
        ///     Removes the child node.
        /// </summary>
        /// <param name="parentNode">The parent node.</param>
        /// <param name="childNodeName">Name of the child node.</param>
        private static void removeChildNode(this XmlNode parentNode, string childNodeName) {
            if (parentNode != null) {
                var modelNameNode = parentNode[childNodeName];
                if (modelNameNode != null) {
                    parentNode.RemoveChild(modelNameNode);
                }
            }
        }

        /// <summary>
        ///     Updates the data dictionary.
        /// </summary>
        /// <param name="pmmlElement">The PMML element.</param>
        private static void updateDataDictionary(this PmmlElement pmmlElement) {
            var dataDictionary = pmmlElement.Element["DataDictionary"];
            dataDictionary.migrateChildNodeToAttribute("numberOfFields");
            var dataFields = dataDictionary.GetElementsByTagName("DataField");
            foreach (XmlNode dataField in dataFields) {
                dataField.migrateChildNodeToAttribute("name");
                dataField.migrateChildNodeToAttribute("optype");
                dataField.migrateChildNodeToAttribute("dataType");
            }
        }

        /// <summary>
        ///     Updates the header.
        /// </summary>
        /// <param name="pmmlElement">The PMML element.</param>
        private static void updateHeader(this PmmlElement pmmlElement) {
            var header = pmmlElement.Element["Header"];
            header.migrateChildNodeToAttribute("copyright");
            header.migrateChildNodeToAttribute("description");

            var application = header["Application"];
            application.migrateChildNodeToAttribute("name");
            application.migrateChildNodeToAttribute("version");

            var extensions = header.GetElementsByTagName("Extension");

            foreach (XmlNode extension in extensions) {
                extension.migrateChildNodeToAttribute("name");
                extension.migrateChildNodeToAttribute("value");
                extension.migrateChildNodeToAttribute("extender");
            }
        }

        /// <summary>
        ///     Generates the general regression model attributes.
        /// </summary>
        /// <param name="parentNode">The parent node.</param>
        private static void updateGeneralRegressionModel(this PmmlElement parentNode) {
            var xmlElement = parentNode.Element;

            xmlElement.migrateNodeToAttribute("GeneralRegressionModel", "modelName");
            xmlElement.migrateNodeToAttribute("GeneralRegressionModel", "modelType");
            xmlElement.migrateNodeToAttribute("GeneralRegressionModel", "functionName");
            xmlElement.migrateNodeToAttribute("GeneralRegressionModel", "algorithmName");
            xmlElement.migrateNodeToAttribute("GeneralRegressionModel", "distribution");
            xmlElement.migrateNodeToAttribute("GeneralRegressionModel", "linkFunction");

            // TODO -- fill out for all of them
            var generalRegressionModel = xmlElement["GeneralRegressionModel"];

            var miningSchema = generalRegressionModel["MiningSchema"];
            if (miningSchema != null) {
                var miningFields = miningSchema.GetElementsByTagName("MiningField");

                foreach (XmlNode miningField in miningFields) {
                    migrateChildNodeToAttribute(miningField, "name");
                    migrateChildNodeToAttribute(miningField, "usageType");
                }
            }

            var output = generalRegressionModel["Output"];
            if (output != null) {
                migrateNodeToAttribute(output, "OutputField", "name");
                migrateNodeToAttribute(output, "OutputField", "feature");
            }

            var parameterList = generalRegressionModel["ParameterList"];
            if (parameterList != null) {
                migrateNodeToAttribute(parameterList, "RequestParameter", "name");
                migrateNodeToAttribute(parameterList, "RequestParameter", "label");
            }

            var factorList = generalRegressionModel["FactorList"];
            if (factorList != null) {
                migrateNodeToAttribute(factorList, "Predictor", "name");
            }

            var covariateList = generalRegressionModel["CovariateList"];
            if (covariateList != null) {
                migrateNodeToAttribute(covariateList, "Predictor", "name");
            }

            var ppMatrices = generalRegressionModel.GetElementsByTagName("PPMatrix");

            var index = ppMatrices.Count - 1;

            while (index >= 0) {

                var ppmMatrix = ppMatrices[index];
                migrateNodeToAttribute(ppmMatrix, "PPCell", "value");
                migrateNodeToAttribute(ppmMatrix, "PPCell", "predictorName");
                migrateNodeToAttribute(ppmMatrix, "PPCell", "parameterName");
                if (index > 0) {
                    var child = (XmlNode) ppmMatrix["PPCell"];
                    ppmMatrix.RemoveChild(child);
                    ppMatrices[0].AppendChild(child);
                    ppmMatrix.ParentNode.RemoveChild(ppmMatrix);
                }

                index--;
            }

            var paramMatrix = generalRegressionModel.GetElementsByTagName("ParamMatrix");

            index = paramMatrix.Count - 1;

            while (index >= 0) {

                var paramItem = paramMatrix[index];
                migrateNodeToAttribute(paramItem, "PCell", "parameterName");
                migrateNodeToAttribute(paramItem, "PCell", "df");
                migrateNodeToAttribute(paramItem, "PCell", "beta");
                if (index > 0) {
                    var child = (XmlNode) paramItem["PCell"];
                    paramItem.RemoveChild(child);
                    paramMatrix[0].AppendChild(child);
                    paramItem.ParentNode.RemoveChild(paramItem);
                }

                index--;
            }

            var requestParameterList = generalRegressionModel.GetElementsByTagName("RequestParameterList");

            index = requestParameterList.Count - 1;

            while (index >= 0) {

                var requestParameter = requestParameterList[index];
                migrateNodeToAttribute(requestParameter, "RequestParameter", "name");
                migrateNodeToAttribute(requestParameter, "RequestParameter", "label");
                if (index > 0) {
                    var child = (XmlNode) requestParameter["RequestParameter"];
                    requestParameter.RemoveChild(child);
                    requestParameterList[0].AppendChild(child);
                    requestParameter.ParentNode.RemoveChild(requestParameter);
                }

                index--;
            }

        }

        /// <summary>
        ///     Migrates the node to attribute.
        /// </summary>
        /// <param name="pmmlElement">The PMML element.</param>
        /// <param name="parentNode">The parent node.</param>
        /// <param name="nodeToMoveToAttribute">The node to modify.</param>
        private static void migrateNodeToAttribute(this XmlNode pmmlElement, string parentNode, string nodeToMoveToAttribute) {
            var generalRegressionModel = pmmlElement[parentNode];

            if (generalRegressionModel != null) {
                migrateChildNodeToAttribute(generalRegressionModel, nodeToMoveToAttribute);
            }
        }

        private static void migrateChildNodeToAttribute(this XmlNode parentNode, string childNodeToMove) {
            if (parentNode != null) {
                var modelNameNode = parentNode[childNodeToMove];
                if (modelNameNode != null) {
                    var modelNameAttribute = parentNode.OwnerDocument.CreateAttribute(childNodeToMove);
                    modelNameAttribute.Value = modelNameNode.ChildNodes[0].Value;
                    parentNode.Attributes.Append(modelNameAttribute);
                    parentNode.RemoveChild(modelNameNode);
                }
            }
        }

    }

}