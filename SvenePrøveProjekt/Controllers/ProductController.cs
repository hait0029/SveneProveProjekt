namespace SvenePrøveProjekt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductRepo _productRepo;
        public ProductController(IProductRepo temp)
        {
            _productRepo = temp;
        }

        [HttpGet]
        public async Task<ActionResult> GetProducts()
        {
            try
            {
                var products = await _productRepo.GetAllProducts();

                if (products == null)
                {
                    return Problem("Nothing was returned from products, this is unexpected");
                }
                return Ok(products);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{productId}")]
        public async Task<ActionResult> GetProductsById(int productId)
        {
            try
            {
                var product = await _productRepo.GetProductById(productId);

                if (product == null)
                {
                    return NotFound($"Product with id {productId} was not found");
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        //Update Method
        [HttpPut("{productId}")]
        public async Task<ActionResult> PutProduct(int productId, Product product)
        {
            try
            {
                var productResult = await _productRepo.UpdateProduct(productId, product);

                if (product == null)
                {
                    return NotFound($"Product with id {productId} was not found");
                }

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
            return Ok(product);

        }

        //Create Method
        [HttpPost]
        public async Task<ActionResult> PostProduct(Product product)
        {
            try
            {
                var createProduct = await _productRepo.CreateProduct(product);

                if (createProduct == null)
                {
                    return StatusCode(500, "Product was not created. Something failed...");
                }
                return CreatedAtAction("PostProduct", new { productId = createProduct.ProductID }, createProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occured while creating the product {ex.Message}");
            }
        }

        //Delete Method
        [HttpDelete("{productId}")]
        public async Task<ActionResult> DeleteProduct(int productId)
        {
            try
            {
                var product = await _productRepo.DeleteProduct(productId);

                if (product == null)
                {
                    return NotFound($"product with id {productId} was not found");
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
