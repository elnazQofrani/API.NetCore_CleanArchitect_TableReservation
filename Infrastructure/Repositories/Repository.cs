using Infrastructure.Data;
using Domain.Entities;
using Application.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbProjectContext dbContext;
        DbSet<T> dbSet;

        public Repository(DbProjectContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<T>();
        }

        public async Task Create(T model) => await dbSet.AddAsync(model);

        public async Task<T?> GetById(int id) => await dbSet.FindAsync(id);

        public async Task Remove(T model) => dbSet.Remove(model);

        public async Task Save() => await dbContext.SaveChangesAsync();

        public  void Update(T model) =>  dbSet.Update(model);

        public async Task<List<T>> GetAll() => await dbSet.ToListAsync();

    }
}
