using CoffeeShop.Model;

namespace CoffeeShop.Repository
{
    public class OrderRepository
    {
        private List<Order> _orders;
        public OrderRepository()
        {
            _orders = new List<Order>();
        }

        public List<Order> GetOrders() => this._orders;

        public void AddOrder(Order order) => this._orders.Add(order);
    }
}
