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
        private readonly CiPlatformContext _context;
       

        public Repository(CiPlatformContext context) {
            _context=context;
            
        }

        public async Task<bool> ExistAsync(string Email)
        {
            return await _context.AnyAsync(e => e.GetType().GetProperty("Email").GetValue(e).Equals(Email));
        }
    

       

        public async Task UserAddAsync(T entity)
        {
            await _context.AddAsync(entity);
        }

        public void UserDelete(T entity)
        {
            _context.Remove(entity);
        }

        public async Task<bool> UserExistsAsync(string Email, string Password)
        {
            return await _context.AnyAsync(e => e.GetType().GetProperty("Email").GetValue(e).Equals(Email));
            return await _context.AnyAsync(u => u.GetType().GetProperty("Password").GetValue(u).Equals(Password));
        }

        public async Task<IEnumerable<T>> UserGetAllAsync()
        {
            return (IEnumerable<T>)await _context.ToListAsync();
        }

        public async Task<T> UserGetByIdAsync(T entity)
        {
            return await _context.FindAsync(entity);
        }

        public async Task UserSaveChangesAsync(T entity)
        {
            await _context.SaveChangesAsync();
        }

        public async Task UserUpdateAsync(T entity)
        {
            _context.Update(entity);
        }

        Task IRepository<T>.UserDelete(T entity)
        {
            throw new NotImplementedException();
        }
    }

    

     
}
