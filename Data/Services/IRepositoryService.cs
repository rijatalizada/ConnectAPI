using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Services
{
    public interface IRepositoryService<T> where T : class, BaseEntity
    {
        Task<List<T>> GetAll();
        Task<List<T>> GetAll(Expression<Func<T, bool>> expression, List<String> includeProperties = null);
        Task<T> GetOne(int id);
        Task Create(T item);
        Task Create(List<T> items);
        Task Update(T item);
        Task Delete(int id);

    }
}
