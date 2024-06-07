namespace SvenePrøveProjekt.Repositories
{
    public class ProductRepo : IProductRepo
    {
        private DatabaseContext _context { get; set; }
        public ProductRepo(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<Product> CreateProduct(Product newProduct)
        {
            _context.Product.Add(newProduct);
            await _context.SaveChangesAsync();
            return newProduct;
        }


        public async Task<List<Product>> GetAllProducts()
        {
            return await _context.Product.ToListAsync();

        }

        public async Task<Product> GetProductById(int productId)
        {
            return await _context.Product.FirstOrDefaultAsync(x => x.ProductID == productId);
        }

        public async Task<Product> UpdateProduct(int productId, Product updateProduct)
        {
            Product product = await GetProductById(productId);
            if (product != null && updateProduct != null)
            {
                
                product.ProductID = updateProduct.ProductID;
                product.Name = updateProduct.Name;
                product.Price = updateProduct.Price;
                await _context.SaveChangesAsync();
            }
            return product;
        }
        public async Task<Product> DeleteProduct(int productId)
        {
            Product product = await GetProductById(productId);
            if (product != null)
            {
                _context.Product.Remove(product);
                await _context.SaveChangesAsync();
            }
            return product;
        }
    }
}
