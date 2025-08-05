using Infrastructure.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using Application.Interface;

namespace Infrastructure.Repositories
{
    public class CustomerRepository : Repository<Customer> , ICustomerRepository
    {

        private readonly DbProjectContext dbContext;

        public CustomerRepository(DbProjectContext dbContext):base(dbContext) 
        {
            this.dbContext = dbContext;
        }
        //public async Task<Customer> CreatAsync(Customer customer)
        //{
        //    await dbContext.Customer.AddAsync(customer);
        //    await dbContext.SaveChangesAsync();

        //    return customer;
        //}

        public async Task<Customer?> GetByCustomerByUserNamePassWord(string name, string password)
        {
            var customer = await dbContext.Customer.FirstOrDefaultAsync(x => x.FirstName == name &&  x.Password == password);
            return customer;
        }

        //public async Task<Customer> GetById(int Id)
        //{

        //    var customer = await dbContext.Customer.FindAsync(Id);
        //    return customer;

        //}

    }
}
