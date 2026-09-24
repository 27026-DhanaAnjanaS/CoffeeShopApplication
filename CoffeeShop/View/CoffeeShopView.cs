using CoffeeShop.Enums;
using CoffeeShop.Model;
using CoffeeShop.Service;
using CoffeeShop.State;
using ConsoleTables;

namespace CoffeeShop.View
{
    public class CoffeeShopView
    {
        private CoffeeShopManager _coffeeShopManager;

        public CoffeeShopView(CoffeeShopManager coffeeShopManager)
        {
            this._coffeeShopManager = coffeeShopManager;
        }

        public void ShowStartupMenu()
        {
            int option;
            do
            {
                Console.Clear();
                Console.WriteLine("\x1b[3J");
                DisplaySplitter();
                Console.WriteLine("Welcome to coffee shop!!!");
                DisplaySplitter();
                DisplayOptions<StartupMenuOptions>();
                int.TryParse(Console.ReadLine(), out option);
                switch (option)
                {
                    case (int)StartupMenuOptions.Exit:
                        Console.WriteLine("Thank you for coming to coffee shop");
                        break;
                    case (int)StartupMenuOptions.Register:
                        this.RegisterView();
                        break;
                    case (int)StartupMenuOptions.Login:
                        this.LoginView();
                        break;
                }
            }
            while (option != 0);
        }

        private static void DisplayOptions<TEnum>() where TEnum : struct, Enum
        {
            foreach (var menuOption in Enum.GetValues<TEnum>())
            {
                Console.WriteLine($"[{Convert.ToInt32(menuOption)}] {menuOption}");
            }
        }

        public void RegisterView()
        {

        }

        public void LoginView()
        {
            Console.WriteLine("User logged in successfully");
            this.ShowUserDashboard();
        }

        public void ShowUserDashboard()
        {
            int option;
            do
            {
                Console.Clear();
                Console.WriteLine("\x1b[3J");
                Console.WriteLine($"Welcome, {CurrentUserLoggedIn.UserName}");
                DisplayOptions<UserDashboardMenu>();
                Console.Write("Enter your option: ");
                int.TryParse(Console.ReadLine(), out option);
                switch (option)
                {
                    case (int)UserDashboardMenu.ViewCoffeeMenu:
                        this.ViewCoffeeMenu();
                        break;
                    case (int)UserDashboardMenu.PlaceOrder:
                        this.PlaceOrder();
                        break;
                    case (int)UserDashboardMenu.ViewAllOrders:
                        this.ViewOrders();
                        break;
                    case (int)UserDashboardMenu.CancelOrder:
                        this.CancelOrder();
                        break;
                    case (int)UserDashboardMenu.Logout:
                        this.LogoutView();
                        break;
                }
                PressForUserConfirmation();
            }
            while (option != 0);
        }

        private static void PressForUserConfirmation()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        public void ViewCoffeeMenu()
        {
            DisplaySplitter();
            Console.WriteLine("OUR MENU");
            DisplaySplitter();
            List<Beverage> menuBeverages = this._coffeeShopManager.GetMenuItems();
            ConsoleTable menuTable = new ConsoleTable();
            int counter = 0;
            menuTable.AddColumn(new string[] { "S.NO", nameof(Beverage.Name), nameof(Beverage.Price) });
            foreach (var beverage in menuBeverages)
            {
                menuTable.AddRow(++counter, beverage.Name, beverage.Price);
            }
            menuTable.Write();
        }

        public void PlaceOrder()
        {
            this.ViewCoffeeMenu();
            List<Beverage> beverages = this._coffeeShopManager.GetMenuItems();
            Console.WriteLine("Which one do you want to order [enter serial number of the beverage]?");
            bool isValidBeverage = int.TryParse(Console.ReadLine(), out int beverageNumber)
                && beverageNumber >= 1 && beverageNumber <= beverages.Count;
            if (!isValidBeverage)
            {
                Console.WriteLine("Invalid beverage selected.");
                return;
            }
            Beverage selectedBeverage = beverages.ElementAt(beverageNumber - 1);
            bool enterQuantity = int.TryParse(Console.ReadLine(), out int quantity);
            bool isStockAvailable = this._coffeeShopManager.CheckStock(selectedBeverage, quantity, out int minutesNeeded);
            if (!isStockAvailable)
            {
                Console.WriteLine("Stock is limited. Do you still want to place your order? [0 for YES and 1 for NO]");
                bool isValidConfirmation = int.TryParse(Console.ReadLine(), out int confirmationNumber);
                if (confirmationNumber == 1)
                {
                    return;
                }
            }
            Order order = new Order()
            {
                Beverage = selectedBeverage,
                Quantity = quantity,
            };
            if (this._coffeeShopManager.PlaceOrder(order).SuccessMessage != string.Empty)
            {
                Console.WriteLine("Order placed successfully");
            }
            else
            {
                Console.WriteLine("Order couldn't be placed. Please try again!");
            }
        }

        public void ViewOrders()
        {

        }

        public void CancelOrder()
        {

        }
        public void LogoutView()
        {

        }

        private static void DisplaySplitter()
        {
            Console.WriteLine(new string('-', 75));
        }
    }
}
