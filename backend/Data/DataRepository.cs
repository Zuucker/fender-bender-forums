using Microsoft.EntityFrameworkCore;
using Models;

namespace Data
{
    public class DataRepository
    {
        private readonly DatabaseContext _context;

        public DataRepository(DatabaseContext context)
        {
            _context = context;
        }

        #region GENERIC

        public async Task<List<T>> GetAllAsync<T>()
            where T : class
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync<T>(int id)
            where T : class
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task AddAsync<T>(T entity)
            where T : class
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync<T>(T entity)
            where T : class
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync<T>(int id)
            where T : class
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        #endregion


        #region USER

        public async Task<ApplicationUser?> GetUser(string login)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == login);
        }

        #endregion
    }
}
