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
using System.IO;


#endregion

namespace Lpp.Scanner.DataMart.Model.Processors.Common.Models {

    public class TempFile : IDisposable {
        #region Fields

        

        #endregion

        

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether [throw exception].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [throw exception]; otherwise, <c>false</c>.
        /// </value>
        private bool ThrowException { get; set; }

        /// <summary>
        ///     Gets the file information.
        /// </summary>
        /// <value>
        ///     The file information.
        /// </value>
        public FileInfo FileInfo { get; set; }


        #endregion

        #region Interface Implementation

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose() {
            delete();
        }

        #endregion

        /// <summary>
        ///     Creates the temporary file.
        /// </summary>
        /// <param name="throwException">if set to <c>true</c> [throw exception].</param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public static TempFile CreateTmpFile(bool throwException = true) {

            var tempFile = new TempFile();
            var fileName = Path.GetTempFileName();
            tempFile.ThrowException = throwException;
            tempFile.FileInfo = new FileInfo(fileName) {Attributes = FileAttributes.Temporary};
return tempFile;
        }

        /// <summary>
        ///     Creates the temporary r file.
        /// </summary>
        /// <param name="throwException">if set to <c>true</c> [throw exception].</param>
        /// <returns></returns>
        public static TempFile CreateTempRFile(bool throwException = true) {
            var tempFile = CreateTmpFile(throwException);

            tempFile.changeExtension(Extension.R);

            return tempFile;
        }

        /// <summary>
        /// 
        /// </summary>
        private enum Extension {

            TMP,
            R

        }

        /// <summary>
        /// Changes the extension.
        /// </summary>
        /// <param name="extension">The extension.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">extension;null</exception>
        private void changeExtension(Extension extension) {
            var extensionText = "tmp";
            switch (extension) {

                case Extension.TMP: {
                    break;
                }
                case Extension.R: {
                    extensionText = "R";
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException("extension", extension, null);
            }

            var oldValue = string.Format("{0}", FileInfo.Extension);
            var newValue = string.Format(".{0}", extensionText);
            var destFileName = FileInfo.FullName.Replace(oldValue, newValue);

            File.Move(FileInfo.FullName, destFileName);
            FileInfo = new FileInfo(destFileName);
            
        }

        /// <summary>
        ///     Deletes this instance.
        /// </summary>
        private void delete() {
            deleteTmpFile(FileInfo.FullName);
        }

        /// <summary>
        ///     Deletes the temporary file.
        /// </summary>
        /// <param name="tmpFile">The temporary file.</param>
        /// <exception cref="System.Exception"></exception>
        private void deleteTmpFile(string tmpFile) {
            try {
                // Delete the temp file (if it exists)
                if (File.Exists(FileInfo.FullName)) {
                    File.Delete(FileInfo.FullName);
                }
            } catch (Exception ex) {
                if (ThrowException) {
                    throw new Exception(string.Format("Error deleting TEMP file: {0}", ex.Message), ex);
                }
            }
        }

    }

}