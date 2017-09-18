using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pSCANNER.DataMart.Model.processor.Analysis {

    /// <summary>
    /// </summary>
    public class Extension {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Extension"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public Extension(string name, string value) {
            Name = name;
            Value = value;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        ///     Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; private set; }

        /// <summary>
        ///     Gets the value.
        /// </summary>
        /// <value>The value.</value>
        public string Value { get; private set; }

        #endregion Properties
    }
}