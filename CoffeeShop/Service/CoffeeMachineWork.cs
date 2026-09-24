using CoffeeShop.Enums;
using CoffeeShop.Model;

namespace CoffeeShop.Service;

public class CoffeeMachineWork
{
    private readonly CoffeeMachine _machine;

    public CoffeeMachineWork(CoffeeMachine machine)
    {
        _machine = machine;
    }

    public async Task ProcessOrder(Order order)
    {
        _machine.MachineStatus = MachineStatus.Busy;
        _machine.CurrentOrder = order;
        order.OrderStatus = OrderStatus.Preparing;
        await Task.Delay(order.Beverage.PreparationTime * order.Quantity);
        order.OrderStatus = OrderStatus.Ready;
        _machine.CurrentOrder = null;
        _machine.MachineStatus = MachineStatus.Available;
    }
}
