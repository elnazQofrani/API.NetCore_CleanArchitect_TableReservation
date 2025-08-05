using Domain.Entities;

namespace Application.Interface
{
    public interface ICustomerService
    {
        Task<Customer> GetCustomerByIdAsync(int customerId);

        Task<Customer> CreatAsync(Customer customer);

        Task<Customer> GetCustomerByNamePassword(string name, string password);
    }
}
