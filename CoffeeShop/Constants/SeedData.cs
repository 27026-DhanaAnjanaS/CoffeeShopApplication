using CoffeeShop.Enums;
using CoffeeShop.Model;

namespace CoffeeShop.Constants;

public static class SeedData
{
    public static List<Beverage> Beverages = new List<Beverage>()
    {
        new Beverage()
        {
            Name = CoffeeType.Milk,
            Price = 200,
            PreparationTime = 3000,
            Ingredients = new Dictionary<IngredientType, int>()
            {
                { IngredientType.Milk, 100 }
            },
        },

        new Beverage()
        {
            Name = CoffeeType.Water,
            Price = 10,
            PreparationTime = 1000,
            Ingredients = new Dictionary<IngredientType, int>()
            {
                { IngredientType.Water, 100 }
            }
        },

        new Beverage()
        {
            Name = CoffeeType.Coffee,
            Price = 300,
            PreparationTime = 5000,
            Ingredients = new Dictionary<IngredientType, int>()
            {
                {IngredientType.Milk, 100 },
                {IngredientType.Water, 50 },
                {IngredientType.CoffeeBeans, 20 },
                {IngredientType.Sugar, 20 },
            }
        }
    };
}
