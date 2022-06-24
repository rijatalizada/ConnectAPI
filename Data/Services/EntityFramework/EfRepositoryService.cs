using Data.DAL;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Services.EntityFramework
{ 
    public class EfRepositoryService<T> : IRepositoryService<T> where T : class, BaseEntity
    {
        protected readonly AppDbContext _context;

        public EfRepositoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetOne(int id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(t => t.Id == id);
        }
        public async Task Create(T item)
        {
            await _context.Set<T>().AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public Task Create(List<T> items)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(int id)
        {
            T item = await GetOne(id);
            _context.Set<T>().Remove(item);
            await _context.SaveChangesAsync();
        }  
        public async Task Update(T item)
        {
            _context.Set<T>().Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>> expression, List<string> includeProperties = null)
        {
            IQueryable<T> contextItem = _context.Set<T>();

            if(includeProperties != null)
            {
                foreach (var property in includeProperties)
                {
                    contextItem = contextItem.Include(property);
                }
            }

            contextItem = contextItem.Where(expression);

            return await contextItem.ToListAsync();
            
        }
    }
}
