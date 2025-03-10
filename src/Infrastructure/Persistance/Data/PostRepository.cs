using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;

namespace Infrastructure.Persistance.Data
{
    public class PostRepository : IPostRepository
    {
        private readonly DatabaseContext _context;

        public PostRepository(DatabaseContext context)
        {
            _context = context;
        }

        #region IRepository

        public Post Add(Post post)
        {
            try
            {
                _context.Posts.Add(post);
                _context.SaveChanges();

                return post;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public Post? GetById(int id)
        {
            try
            {
                return _context.Posts
                    .FirstOrDefault(p => p.Id == id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public IEnumerable<Post> GetAll()
        {
            try
            {
                return _context.Posts
                    .ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public Post Update(Post post)
        {
            try
            {
                _context.Update(post);
                _context.SaveChanges();

                return post;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        public void Delete(Post post)
        {
            try
            {
                _context.Remove(post);
                _context.SaveChanges();
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
