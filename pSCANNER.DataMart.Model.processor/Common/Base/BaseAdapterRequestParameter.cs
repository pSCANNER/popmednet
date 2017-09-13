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

using Lpp.Scanner.DataMart.Model.Processors.DataSetMapping;


namespace Lpp.Scanner.DataMart.Model.Processors.Common.Base {

    public abstract class BaseAdapterRequestParameter : BaseRequestParameter {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseAdapterRequestParameter" /> class.
        /// </summary>
        /// <param name="requestParameter">The request parameter.</param>
        protected BaseAdapterRequestParameter(BaseRequestParameter requestParameter) : base(requestParameter) {}

        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseAdapterRequestParameter" /> class.
        /// </summary>
        /// <param name="requestId">The request identifier.</param>
        /// <param name="requestFor">The request for.</param>
        /// <param name="dataSetConnection"></param>
        protected BaseAdapterRequestParameter(string requestId, RequestForEnum requestFor, BaseDataSetConnection dataSetConnection) : base(requestId, requestFor, dataSetConnection) {}

        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseAdapterRequestParameter" /> class.
        /// </summary>
        /// <param name="requestId">The request identifier.</param>
        /// <param name="requestFor">The request for.</param>
        protected BaseAdapterRequestParameter(string requestId, RequestForEnum requestFor) : base(requestId, requestFor) {}

        #endregion
    }

}