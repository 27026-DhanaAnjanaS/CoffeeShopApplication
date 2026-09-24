using CoffeeShop.Model;

namespace CoffeeShop.Repository
{
    public class UserRepository
    {
        private List<Customer> _customers;
        public UserRepository()
        {
            _customers = new List<Customer>();
        }

        public bool AddUser(Customer Customer)
        {
            _customers.Add(Customer);
            return true;
        }

        public List<Customer> GetCustomers() => this._customers;
    }
}
