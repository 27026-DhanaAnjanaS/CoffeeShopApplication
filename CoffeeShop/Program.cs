using CoffeeShop.Repository;
using CoffeeShop.Service;
using CoffeeShop.View;

namespace CoffeeShop;

public class Program
{
    public static void Main(string[] args)
    {
        MenuRepository menuRepository = new MenuRepository();
        UserRepository userRepository = new UserRepository();
        OrderRepository orderRepository = new OrderRepository();
        CoffeeShopManager manager = new CoffeeShopManager(userRepository, orderRepository, menuRepository);
        CoffeeShopView view = new CoffeeShopView(manager);
        view.ShowStartupMenu();
    }
}
