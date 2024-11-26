using Application.Contracts;
using Domain;
using MediatR;

namespace Application.Command.CreateCommand
{
    internal class CreateInventoryCommandHandler : IRequestHandler<CreateInventoryCommand, bool>
    {
        private readonly IAsyncCommandRepository<InventoryItem> _inventoryItemRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreateInventoryCommandHandler(IAsyncCommandRepository<InventoryItem> inventoryItemRepository, IUnitOfWork unitOfWork)
        {
            _inventoryItemRepository = inventoryItemRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(CreateInventoryCommand request, CancellationToken cancellationToken)
        {
            bool result = false;
            try
            {
                var itemsToSave = request.Items.ToList().ConvertAll(itemDto => new InventoryItem
                {
                    Category = itemDto.Category,
                    ItemId = itemDto.ItemId,
                    ItemName = itemDto.ItemName,
                    Price = itemDto.Price,
                    Quantity = itemDto.Quantity,
                });


                await _inventoryItemRepository.AddRangeAsync(itemsToSave);
                await _unitOfWork.CommitAsync();

                result = true;

            }
            catch (Exception ex)
            {
                AddLogs($"{ex.Message}");
            }

            return result;
        }

        private void AddLogs(string message)
        {

            string filePath = @"c:\debug\general.txt";
            if (!File.Exists(filePath))
            {
                File.Create(filePath);
            }

            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                sw.WriteLine(message);
            }

        }

    }
}
