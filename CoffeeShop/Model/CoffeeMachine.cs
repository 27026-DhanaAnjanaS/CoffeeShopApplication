using CoffeeShop.Enums;

namespace CoffeeShop.Model;

public class CoffeeMachine
{
    public Guid MachineID { get; set; }
    public MachineStatus MachineStatus { get; set; }

    public string Counter { get; set; }

    public Order? CurrentOrder { get; set; }
    public DateTime BusyUntil { get; set; }
}
