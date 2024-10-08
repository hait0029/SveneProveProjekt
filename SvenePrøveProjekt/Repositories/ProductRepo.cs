﻿namespace SvenePrøveProjekt.Repositories
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
            if (newProduct.CategoryId.HasValue)
            {
                newProduct.category = await _context.Category.FirstOrDefaultAsync(e => e.CategoryID == newProduct.CategoryId);
            }

            _context.Product.Add(newProduct);
            await _context.SaveChangesAsync();

            return newProduct;
        }


        public async Task<List<Product>> GetAllProducts()
        {
            return await _context.Product.Include(e => e.category).ToListAsync();

        }

        public async Task<Product> GetProductById(int productId)
        {
            return await _context.Product.Include(e => e.category).FirstOrDefaultAsync(e => e.ProductID == productId);
        }

        public async Task<Product> UpdateProduct(int productId, Product updateProduct)
        {
      Product product = await GetProductById(productId);

            if (product != null)
            {
                product.ProductID = updateProduct.ProductID;
                product.Name = updateProduct.Name;
                product.Price = updateProduct.Price;
                product.CategoryId = updateProduct.CategoryId;
                if (updateProduct.category != null)
                {
                    product.category = await _context.Category.FirstOrDefaultAsync(e => e.CategoryID == updateProduct.category.CategoryID);
                }
                else
                {
                    product.category = null; // Clear the User if null is provided
                }

                _context.Entry(product).State = EntityState.Modified;


                await _context.SaveChangesAsync();
                return await GetProductById(productId);
            }
            return null;
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
