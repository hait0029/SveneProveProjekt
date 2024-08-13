
using SvenePrøveProjekt.Models;

namespace SvenePrøveProjekt.Repositories
{
    public class OrderRepo : IOrderRepo
    {
        private DatabaseContext _context { get; set; }
        public OrderRepo(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<Order> CreateOrder(Order newOrder)
        {
            //This method checks if the our loginid property has a value, it also allows our value to be nullable
            if (newOrder.UserId.HasValue)
            {
                // This line asynchronously queries the Login table in the database to find the first record where the LoginID matches newUser.LoginId. If a match is found, it assigns the result to newUser.login.

                //FirstOrDefaultAsync returns the first element that satisfies the condition specified by the lambda expression or null if no such element is found.
                newOrder.user = await _context.User.FirstOrDefaultAsync(e => e.UserID == newOrder.UserId);
            }
          
            _context.Order.Add(newOrder);
            await _context.SaveChangesAsync();
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

                await _context.SaveChangesAsync();
            }
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
