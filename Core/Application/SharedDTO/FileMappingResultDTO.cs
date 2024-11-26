namespace Application.SharedDTO
{
    /// <summary>
    /// Represents response format of file mapper
    /// </summary>
    /// <typeparam name="T">Domain Model</typeparam>
    public class FileMappingResultDTO<T>
        where T : class
    {
        public T? ValidRecords { get; set; }
        public InvalidRecordDTO? InValidRecords { get; set; }
    }
}