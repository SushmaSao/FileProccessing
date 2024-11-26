using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class InventoryItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Category { get; set; }
        [Required]
        public int ItemId { get; set; }
        [Required]
        public string ItemName { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double Price { get; set; }
    }
}
