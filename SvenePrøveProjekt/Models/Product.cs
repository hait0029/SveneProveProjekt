namespace SvenePrøveProjekt.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; } = 0;
        public string Name { get; set; } = string.Empty;

        
        public int Price { get; set; } = 0;

        public int? CategoryId { get; set; } // Foreign key property
        public Category? category { get; set; }  // Navigation property
        [JsonIgnore]
        public List<ProductList?> orderlists { get; set; } = new List<ProductList?>();
    }
}
