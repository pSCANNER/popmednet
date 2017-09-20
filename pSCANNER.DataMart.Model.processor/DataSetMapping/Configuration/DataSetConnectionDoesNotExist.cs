using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pSCANNER.DataMart.Model.processor.DataSetMapping.Configuration {

    /// <summary>
    /// </summary>
    /// <seealso cref="System.ArgumentException"/>
    public class DataSetConnectionDoesNotExist : ArgumentException {

        /// <summary>
        ///     Initializes a new instance of the <see cref="DataSetConnectionDoesNotExist"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public DataSetConnectionDoesNotExist(string message) : base(message) {
        }
    }
}