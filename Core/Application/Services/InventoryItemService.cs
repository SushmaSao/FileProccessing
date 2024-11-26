using Application.Command.CreateCommand;
using Application.Command.TransferFileIntoModel;
using Application.Contracts;
using MediatR;

namespace Application.Services
{
    public class InventoryItemService : IInventoryItemService
    {
        private readonly IMediator _mediator;
        private readonly IFileService _fileService;

        public InventoryItemService(IMediator mediator, IFileService fileService)
        {
            _mediator = mediator;
            _fileService = fileService;
        }

        public async Task SaveInventoryIntoDatabase(string filePath, string processedFilePath, string errorFilePath)
        {
            var itemDTOs = await _mediator.Send(new TransformFileCommand(filePath));

            if (itemDTOs != null)
            {
                var saveResult = await _mediator.Send(new CreateInventoryCommand(itemDTOs.ValidRecords));

                if (saveResult)
                {
                    // After processing, move the file to the "processed" folder
                    _fileService.MoveFile(filePath, processedFilePath);

                }
            }



        }
    }
}
