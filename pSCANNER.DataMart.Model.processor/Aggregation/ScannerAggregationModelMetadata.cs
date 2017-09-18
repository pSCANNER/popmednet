using Lpp.Dns.DataMart.Model.Settings;
using Lpp.Scanner.DataMart.Model.Processors.Common.Base;
using Lpp.Scanner.DataMart.Model.Processors.Common.Models;
using System;
using System.Collections.Generic;

namespace pSCANNER.DataMart.Model.processor.Aggregation {

    /// <summary>
    /// </summary>
    /// <seealso cref="Lpp.Scanner.DataMart.Model.Processors.Common.Base.MetaDataBase"/>
    [Serializable]
    public class ScannerAggregationModelMetadata : MetaDataBase {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ScannerAggregationModelMetadata"/> class.
        /// </summary>
        public ScannerAggregationModelMetadata() : base(new Dictionary<string, bool> { { "IsSingleton", true }, { "RequiresConfig", false }, { "AddFiles", true }, { "CanRunAndUpload", true }, { "CanUploadWithoutRun", true } }, new Dictionary<string, string> { { "ServiceURL", string.Empty }, { Constants.Aggregator.Input.SettingsEnum.rLocation.ToString(), string.Empty } }) { }

        #endregion Constructors

        #region Properties

        /// <summary>
        ///     Returns the Model Id.
        /// </summary>
        public override Guid ModelId {
            get {
                return Guid.Parse("1AF2DB2F-8F66-4123-8886-81D08B9A3685");
            }
        }

        /// <summary>
        ///     Returns the Model Name.
        /// </summary>
        public override string ModelName {
            get {
                return "Scanner Aggregation";
            }
        }

        /// <summary>
        ///     List of the properties the processor needs.
        /// </summary>
        public override ICollection<ProcessorSetting> Settings {
            get {
                var settings = new List<ProcessorSetting> { new ProcessorSetting { Title = "R Location", Key = Constants.Aggregator.Input.SettingsEnum.rLocation.ToString(), DefaultValue = "C:\\Program Files\\R\\R-3.3.1\\bin\\x64", ValueType = typeof(string), Required = true } };
                return settings;
            }
        }

        /// <summary>
        ///     Returns the Model Version.
        /// </summary>
        public override string Version {
            get {
                return "0.1";
            }
        }

        #endregion Properties
    }
}