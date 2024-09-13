using SvenePrøveProjekt.Models;

namespace SvenePrøveProjekt.Repositories
{
    public class OrderRepo : IOrderRepo
    {
        private DatabaseContext _context { get; set; }
        private IProductListRepo _productListRepo { get; set; }  // Add this to handle ProductList operations

        public OrderRepo(DatabaseContext context, IProductListRepo productListRepo)
        {
            _context = context;
            _productListRepo = productListRepo;
        }

        public async Task<Order> CreateOrder(Order newOrder)
        {
            // Check if UserId has a value and assign the User object accordingly
            if (newOrder.UserId.HasValue)
            {
                newOrder.user = await _context.User.FirstOrDefaultAsync(e => e.UserID == newOrder.UserId);
            }


            // Add the order to the database
            _context.Order.Add(newOrder);
            await _context.SaveChangesAsync(); // Save changes to get the OrderID

            // Add associated ProductList entries
            if (newOrder.orderlists != null && newOrder.orderlists.Count > 0)
            {
                foreach (var productListItem in newOrder.orderlists)
                {
                    productListItem.OrderId = newOrder.OrderID; // Set the OrderID to the created order
                    await _productListRepo.CreateProductOrderList(productListItem);
                }
            }

            return newOrder;
        }

        public async Task<List<Order>> GetAllOrders()
        {
            return await _context.Order.ToListAsync();
        }

        public async Task<Order> GetOrderById(int orderId)
        {
            return await _context.Order.FirstOrDefaultAsync(x => x.OrderID == orderId);
        }

        public async Task<Order> UpdateOrder(int orderId, Order updateOrder)
        {
            Order order = await GetOrderById(orderId);
            if (order != null && updateOrder != null)
            {
                order.OrderID = updateOrder.OrderID;
                order.OrderDate = updateOrder.OrderDate;

                // Update User
                if (updateOrder.user != null)
                {
                    order.user = await _context.User.FirstOrDefaultAsync(e => e.UserID == updateOrder.user.UserID);
                }
                else
                {
                    order.user = null;
                }

                _context.Entry(order).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            return await GetOrderById(orderId);
        }

        public async Task<Order> DeleteOrder(int orderId)
        {
            Order order = await GetOrderById(orderId);
            if (order != null)
            {
                _context.Order.Remove(order);
                await _context.SaveChangesAsync();
            }
            return order;
        }
    }
}
