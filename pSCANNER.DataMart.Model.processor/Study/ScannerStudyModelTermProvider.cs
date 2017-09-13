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
using Lpp.QueryComposer;


#endregion

namespace Lpp.Scanner.DataMart.Model.Processors.Study {

    [Serializable]
    public class ScannerStudyModelTermProvider : IModelTermProvider {

        public static readonly Guid ScannerStudyTermID = new Guid("503EEA4F-D0AD-4CDF-966F-D6A713C41881");
        public static readonly String ScannerStudy_Name = "";
        public static readonly String ScannerStudy_Description = "";

        public static readonly List<ModelTerm> ScannerStudy_Terms = new List<ModelTerm> {new ModelTerm {ID = ScannerStudyTermID, Name = ScannerStudy_Name, Description = ScannerStudy_Description, OID = null, ReferenceUrl = null}};

        #region Properties

        public Guid ModelID {
            get {
                return new Guid("D01D1941-240E-411B-BB26-BA063CD463BE");
            }
        }

        public string Processor {
            get {
                return "Scanner Study Model Processor";
            }
        }

        public Guid ProcessorID {
            get {
                return new Guid("594CFFDC-4C19-492E-9A85-147378B28405");
            }
        }

        public IEnumerable<IModelTerm> Terms {
            get {
                return ScannerStudy_Terms;
            }
        }

        #endregion
    }

}