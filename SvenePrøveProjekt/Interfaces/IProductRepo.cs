namespace SvenePrøveProjekt.Interfaces
{
    public interface IProductRepo
    {
        public Task<List<Product>> GetAllProducts();
        public Task<Product> GetProductById(int productId);
        public Task<Product> CreateProduct(Product product);
        public Task<Product> UpdateProduct(int productId, Product product);
        public Task<Product> DeleteProduct(int productId);
    }
}
