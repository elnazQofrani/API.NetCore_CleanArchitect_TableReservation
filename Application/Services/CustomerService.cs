
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Application.Interface;

namespace Application.Services
{
    public class CustomerService : ICustomerService
    {


        private readonly ICustomerRepository customerRepository;

        public CustomerService(ICustomerRepository _repository)
        {

            customerRepository = _repository;
        }

        public async Task<Customer> CreatAsync(Customer customer)
        {

            await customerRepository.Create(customer);
            await customerRepository.Save();
            return customer;
        }

        public async Task<Customer> GetCustomerByIdAsync(int customerId)
        {

            var customer = await customerRepository.GetById(customerId);
            return customer;

        }

        public async Task<Customer> GetCustomerByNamePassword(string name, string password)
        {

            return await customerRepository.GetByCustomerByUserNamePassWord(name, password);
        }

    }
}
