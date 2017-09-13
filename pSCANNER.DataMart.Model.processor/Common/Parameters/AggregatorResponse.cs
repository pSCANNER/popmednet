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

#endregion

#region Using

using Lpp.Scanner.DataMart.Model.Processors.Common.Base;


#endregion

namespace Lpp.Scanner.DataMart.Model.Processors.Common.Parameters {

    public class AggregatorResponse : ResponseBase {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="AggregatorResponse" /> class.
        /// </summary>
        /// <param name="responseBase">The response base.</param>
        public AggregatorResponse(ResponseBase responseBase) : base(responseBase) {}

        /// <summary>
        ///     Initializes a new instance of the <see cref="AggregatorResponse" /> class.
        /// </summary>
        public AggregatorResponse() {}

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the final response.
        /// </summary>
        /// <value>
        ///     The final response.
        /// </value>
        public string FinalResponse { get; set; }

        #endregion
    }

}