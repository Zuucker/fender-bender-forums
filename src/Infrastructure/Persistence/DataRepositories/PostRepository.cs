using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance.DataRepositories
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
                    .Include(p => p.Contents)
                        .ThenInclude(c => c.GalleryElements)
                    .Include(p => p.Section)

                    .Include(p => p.Comments)
                        .ThenInclude(c => c.User)
                    .Include(p => p.Comments)
                        .ThenInclude(c => c.Likes)

                    .Include(p => p.Comments)
                        .ThenInclude(c => c.SubComments)
                            .ThenInclude(sc => sc.User)
                    .Include(p => p.Comments)
                        .ThenInclude(c => c.SubComments)
                            .ThenInclude(sc => sc.Likes)

                    .Include(p => p.Comments)
                        .ThenInclude(c => c.SubComments)
                            .ThenInclude(sc => sc.SubComments)
                                .ThenInclude(sc => sc.User)
                    .Include(p => p.Comments)
                        .ThenInclude(c => c.SubComments)
                            .ThenInclude(sc => sc.SubComments)
                                .ThenInclude(sc => sc.Likes)

                    .Include(p => p.Comments)
                        .ThenInclude(c => c.SubComments)
                            .ThenInclude(sc => sc.SubComments)
                                .ThenInclude(scc => scc.SubComments)
                                    .ThenInclude(scc => scc.User)
                    .Include(p => p.Comments)
                        .ThenInclude(c => c.SubComments)
                            .ThenInclude(sc => sc.SubComments)
                                .ThenInclude(scc => scc.SubComments)
                                    .ThenInclude(scc => scc.Likes)

                    .Include(p => p.Comments)
                        .ThenInclude(c => c.SubComments)
                            .ThenInclude(sc => sc.SubComments)
                                .ThenInclude(scc => scc.SubComments)
                                    .ThenInclude(sccc => sccc.SubComments)
                                        .ThenInclude(sccc => sccc.User)
                    .Include(p => p.Comments)
                        .ThenInclude(c => c.SubComments)
                            .ThenInclude(sc => sc.SubComments)
                                .ThenInclude(scc => scc.SubComments)
                                    .ThenInclude(sccc => sccc.SubComments)
                                        .ThenInclude(sccc => sccc.Likes)

                    .Include(p => p.Likes)
                    .Include(p => p.User)
                    .Include(p => p.Car)
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
                    .Include(p => p.User)
                    .Include(p => p.Contents)
                        .ThenInclude(c => c.GalleryElements)
                    .Include(p => p.Section)
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

        public IEnumerable<Post> GetUsersPosts(Guid userId)
        {
            try
            {
                return _context.Posts
                    .Where(p => p.AuthorId == userId.ToString())
                    .Include(p => p.User)
                    .Include(p => p.Contents)
                    .Include(p => p.Section)
                    .ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}
