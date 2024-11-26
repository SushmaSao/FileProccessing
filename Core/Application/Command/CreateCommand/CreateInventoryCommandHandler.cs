using Application.Contracts;
using Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Command.CreateCommand
{
    internal class CreateInventoryCommandHandler : IRequestHandler<CreateInventoryCommand, bool>
    {
        private readonly IAsyncCommandRepository<InventoryItem> _inventoryItemRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateInventoryCommandHandler> _logger;
        public CreateInventoryCommandHandler(IAsyncCommandRepository<InventoryItem> inventoryItemRepository,
            IUnitOfWork unitOfWork,
            ILogger<CreateInventoryCommandHandler> logger)
        {
            _inventoryItemRepository = inventoryItemRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
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
                _logger.LogError(ex.Message);
            }

            return result;
        }
    }
}
