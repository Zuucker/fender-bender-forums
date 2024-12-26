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

        public List<T> GetAll<T>()
            where T : class
        {
            var query = _context.Set<T>().AsQueryable();

            foreach (var property in typeof(T).GetProperties())
            {
                if (
                    property.GetMethod?.IsVirtual == true
                    && (
                        typeof(IEnumerable<object>).IsAssignableFrom(property.PropertyType)
                        || !property.PropertyType.IsValueType
                            && property.PropertyType != typeof(string)
                    )
                )
                {
                    query = query.Include(property.Name);
                }
            }

            return query.ToList();
        }

        public T? GetById<T>(int id)
            where T : class
        {
            return _context.Set<T>().Find(id);
        }

        public void Add<T>(T entity)
            where T : class
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public void AddList<T>(List<T> entities)
            where T : class
        {
            _context.Set<T>().AddRange(entities);
            _context.SaveChanges();
        }

        public void Update<T>(T entity)
            where T : class
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }

        public void Delete<T>(int id)
            where T : class
        {
            var entity = _context.Set<T>().Find(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                _context.SaveChanges();
            }
        }

        #endregion


        #region USER

        public ApplicationUser? GetUser(string login)
        {
            try
            {
                return _context.Users
                    .FirstOrDefault(u => u.UserName == login);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public ApplicationUser AddUser(ApplicationUser newUser)
        {
            try
            {
                _context.Users.Add(newUser);
                _context.SaveChanges();

                return newUser;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        #endregion
    }
}
