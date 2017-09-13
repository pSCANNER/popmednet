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
using System.Linq;
using Lpp.Dns.DataMart.Model;
using Lpp.Dns.DataMart.Model.Settings;


#endregion

namespace Lpp.Scanner.DataMart.Model.Processors.Common.Base {

    public abstract class MetaDataBase : IModelMetadata {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="MetaDataBase" /> class.
        /// </summary>
        /// <param name="capabilities">The capabilities.</param>
        /// <param name="properties">The properties.</param>
        protected MetaDataBase(IDictionary<string, bool> capabilities, IDictionary<string, string> properties) {
            Capabilities = capabilities;
            Properties = properties;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Returns the Model Id.
        /// </summary>
        public abstract Guid ModelId { get; }

        /// <summary>
        ///     Returns the Model Name.
        /// </summary>
        public abstract string ModelName { get; }

        /// <summary>
        ///     List of the properties the processor needs.
        /// </summary>
        public abstract ICollection<ProcessorSetting> Settings { get; }

        /// <summary>
        ///     Gets the type of sql providers the processor supports.
        /// </summary>
        public virtual IEnumerable<SQLProvider> SQlProviders {
            get {
                return Enumerable.Empty<SQLProvider>();
            }
        }

        /// <summary>
        ///     Returns the Model Version.
        /// </summary>
        public abstract string Version { get; }

        /// <summary>
        ///     List of capabilities of the Model.
        /// </summary>
        public IDictionary<string, bool> Capabilities { get; private set; }

        /// <summary>
        ///     List of properties of the Model. These are properties whose values will be stored by the DataMartClient.
        /// </summary>
        public IDictionary<string, string> Properties { get; private set; }

        #endregion
    }

}