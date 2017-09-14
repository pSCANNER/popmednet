using Lpp.Dns.DataMart.Model;
using System;

namespace pSCANNER.DataMart.Model.processor.Common.Base {

    /// <summary>
    ///     Container class to hold the document metadata and to hold a reference
    ///     to any files references for manually attached files.
    /// </summary>
    public class InternalDocument {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ScannerBase.InternalDocument" /> class.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <param name="path">The path.</param>
        /// <param name="size">The size.</param>
        public InternalDocument(Document document, string path, long size) {
            Document = document;
            Document.Size = Convert.ToInt32(size);
            Path = path;
            Size = size;
        }

        #endregion Constructors

        private Document _document;

        #region Properties

        /// <summary>
        ///     Gets the document.
        /// </summary>
        /// <value>
        ///     The document.
        /// </value>
        public Document Document {
            get {
                return _document;
            }
            set {
                _document = value;
            }
        }

        /// <summary>
        ///     Gets the path.
        /// </summary>
        /// <value>
        ///     The path.
        /// </value>
        public string Path { get; private set; }

        /// <summary>
        ///     Gets the size.
        /// </summary>
        /// <value>
        ///     The size.
        /// </value>
        public long Size { get; private set; }

        #endregion Properties
    }
}