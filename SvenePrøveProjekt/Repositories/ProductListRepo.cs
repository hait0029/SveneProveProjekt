
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
