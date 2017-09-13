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

using System.Linq;
using System.Text;


#endregion

namespace Lpp.Scanner.DataMart.Model.Processors.Common.Base {

    public abstract class BaseScriptBuilder {
        #region Fields

        /// <summary>
        ///     The _text
        /// </summary>
        private readonly StringBuilder _text = new StringBuilder();

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the text.
        /// </summary>
        /// <value>
        ///     The text.
        /// </value>
        public string Text {
            get {
                return _text.ToString();
            }
        }

        #endregion

        /// <summary>
        ///     Adds the line.
        /// </summary>
        /// <param name="line">The line.</param>
        public void AddLine(string line) {
            _text.AppendLine(line);
        }

        /// <summary>
        ///     Adds the line.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <param name="arguments">The arguments.</param>
        public void AddLine(string line, params object[] arguments) {
            AddLine(string.Format(line, arguments));
        }

        /// <summary>
        ///     Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        ///     A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() {
            return _text.ToString();
        }

    }

}