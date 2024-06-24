
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
            return order;
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
