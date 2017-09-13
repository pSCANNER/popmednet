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

using Lpp.QueryComposer;
using System;
using System.Collections.Generic;



#endregion

namespace Lpp.Scanner.DataMart.Model.Processors
{

    [Serializable]
    public class ScannerAnalysisModelTermProvider : IModelTermProvider
    {

        public static readonly Guid ScannerAnalysisTermID = new Guid("52F0EE10-20AB-47AE-B073-2FE994735AF6");
        public static readonly String ScannerAnalysis_Name = "";
        public static readonly String ScannerAnalysis_Description = "";

        public static readonly List<ModelTerm> ScannerAnalysis_Terms = new List<ModelTerm> { new ModelTerm { ID = ScannerAnalysisTermID, Name = ScannerAnalysis_Name, Description = ScannerAnalysis_Description, OID = null, ReferenceUrl = null } };

        #region Properties

        public Guid ModelID
        {
            get
            {
                return new Guid("977dee0b-212d-4350-b2d2-e3bfefcf7672");
            }
        }

        public string Processor
        {
            get
            {
                return "Scanner Analysis Model Processor";
            }
        }

        public Guid ProcessorID
        {
            get
            {
                return new Guid("9B30325D-1863-4A1F-A2B3-5C58B52D3CE0");
            }
        }

        public IEnumerable<IModelTerm> Terms
        {
            get
            {
                return ScannerAnalysis_Terms;
            }
        }

        #endregion
    }

}