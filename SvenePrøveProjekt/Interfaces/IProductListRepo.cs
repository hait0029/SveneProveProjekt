namespace SvenePrøveProjekt.Interfaces
{
    public interface IProductListRepo
    {
        public Task<List<ProductList>> GetProductOrderList();
        public Task<ProductList> GetProductOrderListById(int productOrderListId);
        public Task<ProductList> CreateProductOrderList(ProductList productOrderList);
        public Task<ProductList> UpdateProductOrderList(int productOrderListId, ProductList ProductOrderList);
        public Task<ProductList> DeleteProductOrderList(int productOrderListId);
    }
}
