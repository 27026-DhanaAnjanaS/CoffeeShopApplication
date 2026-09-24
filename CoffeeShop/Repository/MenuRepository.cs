using CoffeeShop.Constants;
using CoffeeShop.Model;

namespace CoffeeShop.Repository;

public class MenuRepository
{
    private List<Beverage> _beverages;
    public MenuRepository()
    {
        this._beverages = SeedData.Beverages;
    }
    public List<Beverage> GetBeverages() => this._beverages;
}
