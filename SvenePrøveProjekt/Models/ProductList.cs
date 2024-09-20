using System.ComponentModel.DataAnnotations;

namespace SvenePrøveProjekt.Models
{
    public class ProductList
    {
        [Key]
        public int ProductOrderListID { get; set; } = 0;
        public int Quantity { get; set; } = 0;
        public int? OrderId { get; set; }
        public Order? Orders { get; set; }
        public int? ProductId { get; set; }
        public Product? Products { get; set; }
    }
}
