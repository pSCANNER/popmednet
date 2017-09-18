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

using System;
using System.Collections.Generic;
using Lpp.QueryComposer;

#endregion Using

namespace Lpp.Scanner.DataMart.Model.Processors.Study {

    /// <summary>
    /// </summary>
    /// <seealso cref="Lpp.QueryComposer.IModelTermProvider"/>
    [Serializable]
    public class ScannerStudyModelTermProvider : IModelTermProvider {

        /// <summary>
        ///     The scanner study description
        /// </summary>
        public static readonly String ScannerStudy_Description = "";

        /// <summary>
        ///     The scanner study name
        /// </summary>
        public static readonly String ScannerStudy_Name = "";

        /// <summary>
        ///     The scanner study terms
        /// </summary>
        public static readonly List<ModelTerm> ScannerStudy_Terms = new List<ModelTerm> { new ModelTerm { ID = ScannerStudyTermID, Name = ScannerStudy_Name, Description = ScannerStudy_Description, OID = null, ReferenceUrl = null } };

        /// <summary>
        ///     The scanner study term identifier
        /// </summary>
        public static readonly Guid ScannerStudyTermID = new Guid("503EEA4F-D0AD-4CDF-966F-D6A713C41881");

        #region Properties

        /// <summary>
        ///     Gets the model identifier.
        /// </summary>
        /// <value>The model identifier.</value>
        public Guid ModelID {
            get {
                return new Guid("D01D1941-240E-411B-BB26-BA063CD463BE");
            }
        }

        /// <summary>
        ///     Gets the processor.
        /// </summary>
        /// <value>The processor.</value>
        public string Processor {
            get {
                return "Scanner Study Model Processor";
            }
        }

        /// <summary>
        ///     Gets the processor identifier.
        /// </summary>
        /// <value>The processor identifier.</value>
        public Guid ProcessorID {
            get {
                return new Guid("594CFFDC-4C19-492E-9A85-147378B28405");
            }
        }

        /// <summary>
        ///     Gets the terms.
        /// </summary>
        /// <value>The terms.</value>
        public IEnumerable<IModelTerm> Terms {
            get {
                return ScannerStudy_Terms;
            }
        }

        #endregion Properties
    }
}