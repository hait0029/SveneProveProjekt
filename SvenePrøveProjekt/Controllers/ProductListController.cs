using SvenePrøveProjekt.Models;

namespace SvenePrøveProjekt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductListController : ControllerBase
    {
        private IProductListRepo _productRepo;
        public ProductListController(IProductListRepo temp)
        {
            _productRepo = temp;
        }

        [HttpGet]
        public async Task<ActionResult> GetProductLists()
        {
            try
            {
                var productlist = await _productRepo.GetProductOrderList();

                if (productlist == null)
                {
                    return Problem("Nothing was returned from productlist, this is unexpected");
                }
                return Ok(productlist);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{productlistId}")]
        public async Task<ActionResult> GetProductsById(int productlistId)
        {
            try
            {
                var productlist = await _productRepo.GetProductOrderListById(productlistId);

                if (productlist == null)
                {
                    return NotFound($"productlist with id {productlistId} was not found");
                }
                return Ok(productlist);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        //Update Method
        [HttpPut("{cityId}")]
        public async Task<ActionResult> PutProductlist(int productlistId, ProductList productlist)
        {
            try
            {
                var productlistResult = await _productRepo.UpdateProductOrderList(productlistId,productlist);

                if (productlist == null)
                {
                    return NotFound($"productlist with id {productlistId} was not found");
                }

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
            return Ok(productlist);

        }

        //Create Method
        [HttpPost]
        public async Task<ActionResult> PostProductList(ProductList productList)
        {
            try
            {
                var createProductList = await _productRepo.CreateProductOrderList(productList);

                if (createProductList == null)
                {
                    return StatusCode(500, "productList was not created. Something failed...");
                }
                return CreatedAtAction("PostProductList", new { productId = createProductList.ProductId }, createProductList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occured while creating the productList {ex.Message}");
            }
        }

        //Delete Method
        [HttpDelete("{productListId}")]
        public async Task<ActionResult> DeleteproductList(int productListId)
        {
            try
            {
                var productList = await _productRepo.DeleteProductOrderList(productListId);

                if (productList == null)
                {
                    return NotFound($"productList with id {productListId} was not found");
                }
                return Ok(productList);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
