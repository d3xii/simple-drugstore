using System.IO;

namespace SDM.ApplicationCore
{
    /// <summary>
    /// Defines methods to access local file on the server.
    /// </summary>
    public interface IFileAccessProvider
    {
        /// <summary>
        /// Gets stream of given file.
        /// Returns NULL if there is no file.
        /// </summary>
        Stream GetFileStream(string filePath);

        /// <summary>
        /// Creates a new file stream from given file path.
        /// If the file exists, it will be overwritten.
        /// </summary>
        Stream CreateNewFileStream(string filePath);
    }
}
