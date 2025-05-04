using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;

namespace Infrastructure.Persistance.DataRepositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;

        public UserRepository(DatabaseContext context)
        {
            _context = context;
        }

        #region IRepository

        public ApplicationUser Add(ApplicationUser newUser)
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

        public ApplicationUser? GetById(string id)
        {
            try
            {
                return _context.Users
                    .FirstOrDefault(u => u.Id == id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            try
            {
                return _context.Users
                    .ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public ApplicationUser Update(ApplicationUser user)
        {
            try
            {
                _context.Update(user);
                _context.SaveChanges();

                return user;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        public void Delete(ApplicationUser user)
        {
            try
            {
                _context.Remove(user);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        #endregion


        #region IUserRepository

        public ApplicationUser? GetByUserName(string userName)
        {
            try
            {
                return _context.Users
                    .FirstOrDefault(u => u.UserName == userName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public ApplicationUser? GetByEmail(string email)
        {
            try
            {
                return _context.Users
                    .FirstOrDefault(u => u.Email == email);
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
