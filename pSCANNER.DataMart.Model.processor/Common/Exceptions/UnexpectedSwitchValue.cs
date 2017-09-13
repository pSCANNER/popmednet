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


#endregion

namespace Lpp.Scanner.DataMart.Model.Processors.Common.Exceptions {

    public class UnexpectedSwitchValue : Exception {
        #region Constructors

        public UnexpectedSwitchValue(string name) : base(name) {}

        #endregion
    }

}