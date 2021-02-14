using Microsoft.EntityFrameworkCore;
using Recipes.Service.Core.Abstract;
using Recipes.Service.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Service.Data.EntityFramework
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        protected readonly DbContext _context;
        public Repository(RecipeContext context)
        {
            _context = context;
        }
        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().AnyAsync(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
        {
            return await (predicate == null ? _context.Set<T>().CountAsync() : _context.Set<T>().CountAsync(predicate));
        }

        public async Task DeleteAsync(T entity)
        {
            await Task.Run(() => { _context.Set<T>().Remove(entity); });
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null,
            params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> queryResult = _context.Set<T>();

            if (predicate != null)
            {
                queryResult = queryResult.Where(predicate);
            }

            if (includeProperties.Any())
            {
                foreach (var item in includeProperties)
                {
                    queryResult = queryResult.Include(item);
                }
            }

            return await queryResult.ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> queryResult = _context.Set<T>();
            queryResult = queryResult.Where(predicate);

            if (includeProperties.Any())
            {
                foreach (var item in includeProperties)
                {
                    queryResult = queryResult.Include(item);
                }
            }

            return await queryResult.SingleOrDefaultAsync();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            await Task.Run(() => { _context.Set<T>().Update(entity); });
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
