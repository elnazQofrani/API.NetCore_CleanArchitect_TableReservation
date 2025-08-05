using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetById(int id);
        Task Create(T model);
        Task Remove(T model);
        Task<List<T>> GetAll();
        Task Save();
        void Update(T model);

    }
}
