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

using Lpp.QueryComposer;
using System;
using System.Collections.Generic;

#endregion Using

namespace Lpp.Scanner.DataMart.Model.Processors {

    /// <summary>
    /// </summary>
    /// <seealso cref="Lpp.QueryComposer.IModelTermProvider"/>
    [Serializable]
    public class ScannerAnalysisModelTermProvider : IModelTermProvider {

        /// <summary>
        ///     The scanner analysis description
        /// </summary>
        public static readonly String ScannerAnalysis_Description = "";

        /// <summary>
        ///     The scanner analysis name
        /// </summary>
        public static readonly String ScannerAnalysis_Name = "";

        /// <summary>
        ///     The scanner analysis terms
        /// </summary>
        public static readonly List<ModelTerm> ScannerAnalysis_Terms = new List<ModelTerm> { new ModelTerm { ID = ScannerAnalysisTermID, Name = ScannerAnalysis_Name, Description = ScannerAnalysis_Description, OID = null, ReferenceUrl = null } };

        /// <summary>
        ///     The scanner analysis term identifier
        /// </summary>
        public static readonly Guid ScannerAnalysisTermID = new Guid("52F0EE10-20AB-47AE-B073-2FE994735AF6");

        #region Properties

        /// <summary>
        ///     Gets the model identifier.
        /// </summary>
        /// <value>The model identifier.</value>
        public Guid ModelID {
            get {
                return new Guid("977dee0b-212d-4350-b2d2-e3bfefcf7672");
            }
        }

        /// <summary>
        ///     Gets the processor.
        /// </summary>
        /// <value>The processor.</value>
        public string Processor {
            get {
                return "Scanner Analysis Model Processor";
            }
        }

        /// <summary>
        ///     Gets the processor identifier.
        /// </summary>
        /// <value>The processor identifier.</value>
        public Guid ProcessorID {
            get {
                return new Guid("9B30325D-1863-4A1F-A2B3-5C58B52D3CE0");
            }
        }

        /// <summary>
        ///     Gets the terms.
        /// </summary>
        /// <value>The terms.</value>
        public IEnumerable<IModelTerm> Terms {
            get {
                return ScannerAnalysis_Terms;
            }
        }

        #endregion Properties
    }
}