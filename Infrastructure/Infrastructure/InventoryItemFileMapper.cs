using Application.Command.TransferFileIntoModel;
using Application.Contracts;
using Application.SharedDTO;
using Polly;

namespace Infrastructure
{
    public class InventoryItemFileMapper : IFileMapper<InventoryItemDTO>
    {
        // Define the retry policy (up to 3 retries with a delay of 1 second)
        private readonly Policy _retryPolicy = Policy
            .Handle<Exception>()  // Retry on any Exception (can be more specific)
            .WaitAndRetry(3, retryAttempt => TimeSpan.FromSeconds(1));

        public FileMappingResultDTO<InventoryItemDTO> MapContent(int lineNumber, string fileContent)
        {
            FileMappingResultDTO<InventoryItemDTO> response = new();

            try
            {
                InventoryItemDTO inventoryItemDTO = _retryPolicy.Execute(() => ProcessLine(fileContent));

                if (inventoryItemDTO != null)
                {
                    response.ValidRecords = inventoryItemDTO;
                }
            }
            catch (Exception ex)
            {
                response.InValidRecords = new InvalidRecordDTO(lineNumber, fileContent, ex.Message);

                //TODO: Logs
            }

            return response;
        }

        private static InventoryItemDTO ProcessLine(string line)
        {
            string[]? fields = line.Split(',');

            try
            {
                // Map fields to InventoryItem model
                InventoryItemDTO item = new InventoryItemDTO
                {
                    Category = fields[0],
                    ItemId = int.Parse(fields[1]),
                    ItemName = fields[2],
                    Quantity = int.Parse(fields[3]),
                    Price = double.Parse(fields[4]),
                };
                return item;
            }
            catch
            {
                throw new Exception("Invalid data found");
            }
        }
    }
}
