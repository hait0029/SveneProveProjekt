namespace SvenePrøveProjekt.Interfaces
{
    public interface IOrderRepo
    {
        public Task<List<Order>> GetAllOrders();
        public Task<Order> GetOrderById(int orderId);
        public Task<Order> CreateOrder(Order order);
        public Task<Order> UpdateOrder(int orderId, Order order);
        public Task<Order> DeleteOrder(int orderId);
    }
}
