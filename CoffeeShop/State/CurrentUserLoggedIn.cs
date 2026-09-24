using CoffeeShop.Enums;

namespace CoffeeShop.State
{
    public static class CurrentUserLoggedIn
    {
        public static string UserName { get; set; } = "Dummy User";

        public static Guid UserID { get; set; } = Guid.NewGuid();

        public static bool IsLoggedIn { get; set; } = true;

        public static UserRole UserRole { get; set; } = UserRole.Customer;
    }
}
