namespace Application.Command.TransferFileIntoModel
{
    public record InventoryItemDTO
    {
        public string Category { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
