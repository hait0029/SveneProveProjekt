using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SvenePrøveProjekt.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; } = 0; // primary key
        public DateTime OrderDate { get; set; } = DateTime.Now;

        public int? UserId { get; set; } // Foreign key property
        public User? user { get; set; }  // Navigation property

        [JsonIgnore]
        public List<ProductList?> orderlists { get; set; } = new List<ProductList?>();
    }
}
