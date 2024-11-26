using Application.SharedDTO;

namespace Application.Command.TransferFileIntoModel
{
    public sealed class TransformFileCommandResponse
    {
        public IList<InventoryItemDTO>? ValidRecords { get; set; } = new List<InventoryItemDTO>();
        public IList<InvalidRecordDTO>? InValidRecords { get; set; } = new List<InvalidRecordDTO>();

    }
}