using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SvenePrøveProjekt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderRepo _orderRepo;
        public OrderController(IOrderRepo temp)
        {
            _orderRepo = temp;
        }

        [HttpGet]
        public async Task<ActionResult> GetOrders()
        {
            try
            {
                var orders = await _orderRepo.GetAllOrders();

                if (orders == null)
                {
                    return Problem("Nothing was returned from orders, this is unexpected");
                }
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{orderId}")]
        public async Task<ActionResult> GetOrdersById(int orderId)
        {
            try
            {
                var order = await _orderRepo.GetOrderById(orderId);

                if (order == null)
                {
                    return NotFound($"order with id {orderId} was not found");
                }
                return Ok(order);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        //Update Method
        [HttpPut("{orderId}")]
        public async Task<ActionResult> PutOrder(int orderId, Order order)
        {
            try
            {
                var orderResult = await _orderRepo.UpdateOrder(orderId, order);

                if (order == null)
                {
                    return NotFound($"Order with id {orderId} was not found");
                }

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
            return Ok(order);

        }

        //Create Method
        [HttpPost]
        public async Task<ActionResult> PostOrder(Order order)
        {
            try
            {
                var createOrder = await _orderRepo.CreateOrder(order);

                if (createOrder == null)
                {
                    return StatusCode(500, "Order was not created. Something failed...");
                }
                return CreatedAtAction("PostOrder", new { orderId = createOrder.OrderID }, createOrder);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occured while creating the order {ex.Message}");
            }
        }

        //Delete Method
        [HttpDelete("{cityId}")]
        public async Task<ActionResult> DeleteOrder(int orderId)
        {
            try
            {
                var order = await _orderRepo.DeleteOrder(orderId);

                if (order == null)
                {
                    return NotFound($"Order with id {orderId} was not found");
                }
                return Ok(order);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
