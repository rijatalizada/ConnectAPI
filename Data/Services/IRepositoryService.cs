using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Services
{
    public interface IRepositoryService<T> where T : class, BaseEntity
    {
        Task<List<T>> GetAll();
        Task<T> GetOne(int id);
        Task Create(T item);
        Task Create(List<T> items);
        Task Update(int id, T item);
        Task Delete(int id);

    }
}
