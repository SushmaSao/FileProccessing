namespace Application.Contracts
{
    public interface IFileService
    {
        /// <summary>
        /// Reads a file from an external source asynchronously.
        /// </summary>
        /// <param name="filePath">Path of the file to read.</param>
        /// <returns>File content as a string.</returns>
        public IAsyncEnumerable<string> ReadFileLinesAsync(string filePath, CancellationToken cancellationToken);


        public bool MoveFile(string sourcePath, string destinationPath);
    }
}
