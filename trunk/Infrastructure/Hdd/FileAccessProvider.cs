using System.IO;
using System.Web;
using MHTools.Common;
using SDM.ApplicationCore;

namespace SDM.Infrastructure.Hdd
{
    /// <summary>
    /// Implements <see cref="IFileAccessProvider"/> to provide real access to local file on the server.
    /// </summary>
    public class FileAccessProvider : IFileAccessProvider
    {
        //**************************************************
        //
        // Dependencies
        //
        //**************************************************

        #region Dependencies

        /// <summary>
        /// Gets reference to the associated server.
        /// </summary>
        private readonly HttpServerUtilityBase _server;

        #endregion


        //**************************************************
        //
        // Constructors
        //
        //**************************************************

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public FileAccessProvider(HttpServerUtilityBase server)
        {
            _server = server;
        }

        #endregion


        #region Implementation of IFileAccessProvider

        /// <summary>
        /// Gets stream of given file.
        /// Returns NULL if there is no file.
        /// </summary>
        public Stream GetFileStream(string filePath)
        {
            // get real path
            string path = this.MapServerPath(filePath);

            // return result
            return File.Exists(path) ? File.Open(path, FileMode.Open, FileAccess.ReadWrite) : null;
        }

        /// <summary>
        /// Creates a new file stream from given file path.
        /// If the file exists, it will be overwritten.
        /// </summary>
        public Stream CreateNewFileStream(string filePath)
        {
            // get real path
            string path = this.MapServerPath(filePath);

            // return result
            IOHelper.PrepareFolderContainsFile(path);
            return File.Create(path);
        }

        #endregion


        //**************************************************
        //
        // Private methods
        //
        //**************************************************

        #region Private methods

        /// <summary>
        /// Maps relative path into server real path.
        /// </summary>
        private string MapServerPath(string relativePath)
        {
            return _server.MapPath(relativePath);
        }

        #endregion

    }
}
