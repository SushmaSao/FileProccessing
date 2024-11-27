using Application.Contracts;
using System.Runtime.CompilerServices;

namespace Infrastructure
{
    public class FileService : IFileService
    {
        public bool MoveFile(string sourcePath, string destinationPath)
        {
            bool result = true;
            try
            {
                File.Move(sourcePath, destinationPath);
            }
            catch (Exception ex) { result = false; }

            return result;
        }

        public async IAsyncEnumerable<string> ReadFileLinesAsync(string filePath, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            // Open the file for asynchronous reading
            using var streamReader = new StreamReader(filePath); //Synchronous disposal - for async disposal need to try await using

            // Use the IAsyncEnumerable pattern to ensure proper disposal
            string? line;
            while ((line = await streamReader.ReadLineAsync().ConfigureAwait(false)) != null)
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return line; // Yield each line asynchronously
            }
        }


    }

}

