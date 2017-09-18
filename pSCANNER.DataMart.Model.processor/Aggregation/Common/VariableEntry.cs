namespace Lpp.Scanner.DataMart.Model.Processors.Aggregation.Common {

    /// <summary>
    /// </summary>
    /// <seealso cref="Lpp.Scanner.DataMart.Model.Processors.Common.Base.ProxyBase"/>
    public class VariableEntry {

        #region Fields

        /// <summary>
        ///     The variable description
        /// </summary>
        private string _variableDescription;

        #endregion Fields

        #region Properties

        /// <summary>
        ///     Gets or sets the default value.
        /// </summary>
        /// <value>The default value.</value>
        public string DefaultValue { get; set; }

        /// <summary>
        ///     Gets the variable description.
        /// </summary>
        /// <value>The variable description.</value>
        public string VariableDescription {
            get {
                return _variableDescription ?? VariableName;
            }
            set {
                _variableDescription = value;
            }
        }

        /// <summary>
        ///     Gets the name of the variable.
        /// </summary>
        /// <value>The name of the variable.</value>
        public string VariableName { get; set; }

        #endregion Properties

        /// <summary>
        ///     Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public override string ToString() {
            return VariableName;
        }
    }
}