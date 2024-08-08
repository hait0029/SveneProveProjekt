namespace SvenePrøveProjekt.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; } = 0;
        public string Name { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; } = 0;
        [JsonIgnore]
        public List<ProductList?> orderlists { get; set; } = new List<ProductList?>();
    }
}
