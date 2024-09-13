
using SvenePrøveProjekt.Models;

namespace SvenePrøveProjekt.Repositories
{
    public class ProductListRepo : IProductListRepo
    {
        private DatabaseContext _context { get; set; }
        public ProductListRepo(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<ProductList> CreateProductOrderList(ProductList newProductOrderList)
        {
            // Check if OrderId is provided and associate with the related Order entity
            if (newProductOrderList.OrderId.HasValue)
            {
                var order = await _context.Order
                    .FirstOrDefaultAsync(e => e.OrderID == newProductOrderList.OrderId);

                if (order != null)
                {
                    newProductOrderList.Orders = order;
                }
                else
                {
                    // Handle case where OrderId is invalid (optional)
                    throw new ArgumentException($"Order with ID {newProductOrderList.OrderId} does not exist.");
                }
            }

            // Check if ProductId is provided and associate with the related Product entity
            if (newProductOrderList.ProductId.HasValue)
            {
                var product = await _context.Product
                    .FirstOrDefaultAsync(e => e.ProductID == newProductOrderList.ProductId);

                if (product != null)
                {
                    newProductOrderList.Products = product;
                }
                else
                {
                    // Handle case where ProductId is invalid (optional)
                    throw new ArgumentException($"Product with ID {newProductOrderList.ProductId} does not exist.");
                }
            }

            // Add the new ProductList entity to the context
            _context.ProductList.Add(newProductOrderList);

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return the newly created ProductList entity
            return newProductOrderList;
        }



        public async Task<List<ProductList>> GetProductOrderList()
        {
          return await _context.ProductList.Include(e => e.Products).Include(e => e.Orders).ToListAsync();
        }

        public async Task<ProductList> GetProductOrderListById(int productOrderListId)
        {
            return await _context.ProductList.FirstOrDefaultAsync(x => x.ProductOrderListID == productOrderListId);
        }

        public async Task<ProductList> UpdateProductOrderList(int productOrderListId, ProductList updateProductOrderList)
        {
            ProductList productList = await GetProductOrderListById(productOrderListId);
            if (productList != null)
            {
                productList.ProductOrderListID = updateProductOrderList.ProductOrderListID;
                productList.Quantity = updateProductOrderList.Quantity;

                if (updateProductOrderList.Products != null)
                {
                    productList.Products = await _context.Product.FirstOrDefaultAsync(e => e.ProductID == updateProductOrderList.Products.ProductID);
                }

                else
                {
                    productList.Products = null;
                }

                if (updateProductOrderList.Orders != null)
                {
                    productList.Orders = await _context.Order.FirstOrDefaultAsync(e => e.OrderID == updateProductOrderList.Orders.OrderID);
                }

                else
                {
                    productList.Orders = null;
                }

                await _context.SaveChangesAsync();
            }
            return productList;
        }
        public async Task<ProductList> DeleteProductOrderList(int productOrderListId)
        {
            ProductList productList = await GetProductOrderListById(productOrderListId);
            if(productList != null)
            {
                _context.ProductList.Remove(productList);
                await _context.SaveChangesAsync();
            }
            return productList;
        }
    }
}
