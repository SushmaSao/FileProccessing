using Application.SharedDTO;

namespace Application.Contracts
{
    public interface IFileMapper<T>
       where T : class
    {
        /// <summary>
        /// Maps raw file content to a domain model after validating its structure.
        /// </summary>
        /// <param name="fileContent">Raw file content.</param>
        /// <returns>Mapped domain model.</returns>
        public FileMappingResultDTO<T> MapContent(int lineNumber, string fileContent);
    }
}
