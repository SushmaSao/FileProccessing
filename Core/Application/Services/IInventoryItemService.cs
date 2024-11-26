namespace Application.Services
{
    public interface IInventoryItemService
    {
        public Task SaveInventoryIntoDatabase(string filePath, string processedFilePath, string errorFilePath);
    }
}
