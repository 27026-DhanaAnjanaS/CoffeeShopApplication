using CoffeeShop.Enums;

namespace CoffeeShop.Model
{
    public class Beverage
    {
        public CoffeeType Name { get; set; }

        public int PreparationTime { get; set; }

        public decimal Price { get; set; }

        public Dictionary<IngredientType, int> Ingredients { get; set; }
    }
}
