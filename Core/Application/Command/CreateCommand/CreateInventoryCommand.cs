using Application.Command.TransferFileIntoModel;
using MediatR;

namespace Application.Command.CreateCommand
{
    public sealed record CreateInventoryCommand(IEnumerable<InventoryItemDTO> Items) : IRequest<bool>;

}
