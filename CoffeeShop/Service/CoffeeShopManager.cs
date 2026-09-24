using CoffeeShop.Enums;
using CoffeeShop.Model;
using CoffeeShop.Repository;
using CoffeeShop.State;
using System.Text;

namespace CoffeeShop.Service;

public class CoffeeShopManager
{
    private UserRepository _userRepository;
    private OrderRepository _orderRepository;
    private List<CoffeeMachine> _coffeeMachines;
    private Queue<Order> _ordersToBeProcessed;
    private MenuRepository _menuRepository;
    public CoffeeShopManager(UserRepository userRepository, OrderRepository orderRepository, MenuRepository menuRepository)
    {
        this._userRepository = userRepository;
        this._orderRepository = orderRepository;
        this._menuRepository = menuRepository;
        this._ordersToBeProcessed = new Queue<Order>();
        this._coffeeMachines = new List<CoffeeMachine>()
        {
            new CoffeeMachine()
            {
                MachineID = Guid.NewGuid(),
                MachineStatus = Enums.MachineStatus.Available,
                Counter = "Counter 1",
            },
            new CoffeeMachine()
            {
                MachineID = Guid.NewGuid(),
                MachineStatus = Enums.MachineStatus.Available,
                Counter = "Counter 2",
            },
            new CoffeeMachine()
            {
                MachineID = Guid.NewGuid(),
                MachineStatus = Enums.MachineStatus.Available,
                Counter = "Counter 3",
            }
        };
    }

    public List<Beverage> GetMenuItems() => this._menuRepository.GetBeverages();

    public OperationResult Register(Customer customer)
    {
        OperationResult operationResult = new OperationResult();
        StringBuilder stringBuilder = new StringBuilder();
        ValidateCustomerInfo(customer, stringBuilder);
        if (this._userRepository.AddUser(customer))
        {
            operationResult.SuccessMessage = "Registration successful";
        }
        else
        {
            operationResult.ErrorMessage = "Registration failed. Please try again.";
        }
        return operationResult;
    }

    public OperationResult Login(Customer customer)
    {
        OperationResult operationResult = new OperationResult();
        List<Customer> customers = this._userRepository.GetCustomers();
        Customer? validCustomer = customers.Where(
            customer => customer.PhoneNumber.Equals(customer.PhoneNumber) && customer.Password.Equals(customer.Password))?.FirstOrDefault();
        if (validCustomer is null)
        {
            operationResult.ErrorMessage = "Login failed. Invalid user credentials";
            return operationResult;
        }

        CurrentUserLoggedIn.UserName = validCustomer.CustomerName;
        CurrentUserLoggedIn.IsLoggedIn = true;
        CurrentUserLoggedIn.UserID = validCustomer.ID;
        operationResult.SuccessMessage = $"Login success! {CurrentUserLoggedIn.UserName}";
        return operationResult;
    }

    public OperationResult PlaceOrder(Order order)
    {
        OperationResult operationResult = new OperationResult();
        if (!this.CheckStock(order.Beverage, order.Quantity, out int minutesNeeded))
        {
            operationResult.ErrorMessage = "Order cannot be placed due to insufficient stock";
            return operationResult;
        }
        order.OrderID = Guid.NewGuid();
        order.CustomerID = CurrentUserLoggedIn.UserID;
        order.TotalPrice = order.Beverage.Price * order.Quantity;
        order.OrderStatus = OrderStatus.Placed;
        this._orderRepository.AddOrder(order);
        this._ordersToBeProcessed.Enqueue(order);
        operationResult.SuccessMessage = "Order placed successfully";
        return operationResult;
    }

    public List<Order> GetOrdersByID(Guid customerID) => this._orderRepository.GetOrders().Where(order => order.CustomerID.Equals(customerID)).ToList();

    public bool CheckStock(Beverage beverage, int quantity, out int minutesNeeded)
    {
        bool isSufficient = true;
        foreach (KeyValuePair<IngredientType, int> keyValuePair in beverage.Ingredients)
        {
            if (keyValuePair.Key == IngredientType.CoffeeBeans
                && InventoryLevels.CoffeeBeans < keyValuePair.Value * quantity)
            {
                isSufficient = false;
            }
            if (keyValuePair.Key == IngredientType.Milk
                && InventoryLevels.Milk < keyValuePair.Value * quantity)
            {
                isSufficient = false;
            }
            if (keyValuePair.Key == IngredientType.Sugar
                && InventoryLevels.Sugar < keyValuePair.Value * quantity)
            {
                isSufficient = false;
            }
            if (keyValuePair.Key == IngredientType.TeaPowder
                && InventoryLevels.TeaPowder < keyValuePair.Value * quantity)
            {
                isSufficient = false;
            }
        }
        minutesNeeded = 5;
        return isSufficient;
    }

    public bool UpdateStock()
    {
        return true;
    }

    private static void ValidateCustomerInfo(Customer customer, StringBuilder stringBuilder)
    {
        if (!Validator.Validator.IsValidName(customer.CustomerName))
        {
            stringBuilder.Append("Invalid name");
        }
        if (!Validator.Validator.IsValidPassword(customer.Password))
        {
            stringBuilder.Append("Invalid password");
        }
        if (!Validator.Validator.IsValidPhoneNumber(customer.PhoneNumber))
        {
            stringBuilder.Append("invalid phone number");
        }
        if (stringBuilder.Length > 0)
        {
            throw new InvalidOperationException(stringBuilder.ToString());
        }
    }
}
