using Domain.Entities;

namespace Application.Interface
{
    public interface ICustomerRepository :IRepository<Customer>
    {
      //  Task<Customer> CreatAsync(Customer customer);
   //     Task<Customer> GetById(int Id);
        Task<Customer?> GetByCustomerByUserNamePassWord(string name , string password);       
    }
}
