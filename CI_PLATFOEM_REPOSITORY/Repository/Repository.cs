using CI_PLATFORM_REPOSITORY.DataDB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_PLATFOEM_REPOSITORY.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly CiplatformContext _context;
        private readonly DbSet<T> _dbset;
        private object _dbSet;

        public Repository(CiPlatformContext context) {
            _context= new CiplatformContext();
            _dbset =context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbset.AddAsync(entity);
        }

        public Task DeleteAsync(T entity)
        {
            _dbset.Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<bool> ExistsAsync(string id)
        {
            return await _dbset.AnyAsync(e => e.GetType().GetProperty("Id").GetValue(e).Equals(id));
        }
        

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbset.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {

            return await _dbset.FindAsync(id);
        }

        public async Task SaveChangesAsync(T entity)
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
             _dbset.Update(entity);
        }
    }

    

     
}
