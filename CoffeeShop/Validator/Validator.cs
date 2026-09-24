using System.Text.RegularExpressions;

namespace CoffeeShop.Validator
{
    public static class Validator
    {
        public static bool IsValidName(string name) => Regex.IsMatch(name, @"^[A-Za-z]+$");

        public static bool IsValidPassword(string password) => Regex.IsMatch(password, @"\^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");

        public static bool IsValidPhoneNumber(string phoneNumber) => Regex.IsMatch(phoneNumber, @"^\+?\d{1,3}?[-.\s]?\(?\d{1,4}?\)?[-.\s]?\d{1,4}[-.\s]?\d{1,9}$");
    }
}
