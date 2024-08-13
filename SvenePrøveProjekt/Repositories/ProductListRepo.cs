
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
      
            if (newProductOrderList.ProductId.HasValue)
            {
                newProductOrderList.Products = await _context.Product.FirstOrDefaultAsync(e => e.ProductID == newProductOrderList.ProductId);
            }
            else if (newProductOrderList.OrderId.HasValue)
            {
                newProductOrderList.Orders = await _context.Order.FirstOrDefaultAsync(e => e.OrderID == newProductOrderList.OrderId);
            }

            _context.ProductList.Add(newProductOrderList);
            await _context.SaveChangesAsync();

            return newProductOrderList;
        }


        public async Task<List<ProductList>> GetProductOrderList()
        {
          return await _context.ProductList.ToListAsync();
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
