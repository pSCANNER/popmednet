using Lpp.Dns.DataMart.Model.Settings;
using Lpp.Scanner.DataMart.Model.Processors.Common.Base;
using Lpp.Scanner.DataMart.Model.Processors.Common.Models;
using System;
using System.Collections.Generic;

namespace pSCANNER.DataMart.Model.processor.Analysis {

    [Serializable]
    public class ScannerAnalysisModelMetadata : MetaDataBase {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ScannerAnalysisModelMetadata"/> class.
        /// </summary>
        public ScannerAnalysisModelMetadata() : base(new Dictionary<string, bool> { { "IsSingleton", true }, { "RequiresConfig", false }, { "AddFiles", true }, { "CanRunAndUpload", true }, { "CanUploadWithoutRun", true } }, new Dictionary<string, string> { { "ServiceURL", string.Empty }, { Constants.Processor.Input.SettingsEnum.DanURL.ToString(), string.Empty }, { Constants.Processor.Input.SettingsEnum.rLocation.ToString(), string.Empty } }) { }

        #endregion Constructors

        #region Properties

        public override Guid ModelId {
            get {
                return new Guid("977DEE0B-212D-4350-B2D2-E3BFEFCF7672");
            }
        }

        /// <summary>
        ///     Returns the Model Name.
        /// </summary>
        public override string ModelName {
            get {
                return "Scanner Analysis";
            }
        }

        /// <summary>
        ///     List of the properties the processor needs.
        /// </summary>
        public override ICollection<ProcessorSetting> Settings {
            get {
                var settings = new List<ProcessorSetting> {new ProcessorSetting { // https://jira.mc.vanderbilt.edu/browse/PSCANNER-6
                        Title = "DAN URL", Key = Constants.Processor.Input.SettingsEnum.DanURL.ToString(), DefaultValue = "http://localhost:22599/DanDirect/DirectBroker/PostRequest", ValueType = typeof(string), Required = true},
                        new ProcessorSetting { // https://jira.mc.vanderbilt.edu/browse/PSCANNER-6
                            Title = "R BIN Location ", Key = Constants.Processor.Input.SettingsEnum.rLocation.ToString(), DefaultValue = "C:\\Program Files\\R\\R-3.3.1\\bin\\x64", ValueType = typeof(string), Required = true}};
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