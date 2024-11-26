using Application.Contracts;

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

        public async Task<IAsyncEnumerable<string>> ReadFileLinesAsync(string filePath)
        {
            // Open the file for asynchronous reading
            var streamReader = new StreamReader(filePath);

            // Use the IAsyncEnumerable pattern to ensure proper disposal
            return ReadLinesAsync(streamReader);
        }

        private async IAsyncEnumerable<string> ReadLinesAsync(StreamReader streamReader)
        {
            try
            {
                string? line;
                while ((line = await streamReader.ReadLineAsync()) != null)
                {
                    yield return line; // Yield each line asynchronously
                }
            }
            finally
            {
                // Dispose of the StreamReader after reading the lines
                streamReader.Dispose();
            }
        }
    }

}
