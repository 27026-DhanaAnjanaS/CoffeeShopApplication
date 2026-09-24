using CoffeeShop.Enums;

namespace CoffeeShop.Model
{
    public class Order
    {
        public Guid OrderID { get; set; }

        public Guid CustomerID { get; set; }

        public Beverage Beverage { get; set; }

        public decimal TotalPrice { get; set; }

        public int Quantity { get; set; }

        public DateTime CreatedAt { get; set; }

        public OrderStatus OrderStatus { get; set; }
    }
}
