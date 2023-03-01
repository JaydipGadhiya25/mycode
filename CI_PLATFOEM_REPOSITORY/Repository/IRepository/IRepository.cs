using CI_PLATFORM_REPOSITORY.DataDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_PLATFOEM_REPOSITORY.Repository
{
    public interface IRepository<T> where T : class
    {
       
        Task UserAddAsync(T entity);
        Task UserDelete(T entity);
        Task UserUpdateAsync(T entity);
        Task UserSaveChangesAsync(T entity);
        Task<T> UserGetByIdAsync(T entity);
        Task<IEnumerable<T>> UserGetAllAsync();
        Task<bool> ExistAsync(string Email);
        Task<bool> UserExistsAsync(string Email, string Password);

    }
}
