using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;

namespace Infrastructure.Persistance.Data
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DatabaseContext _context;

        public CommentRepository(DatabaseContext context)
        {
            _context = context;
        }

        #region IRepository

        public Comment Add(Comment comment)
        {
            try
            {
                _context.Comments.Add(comment);
                _context.SaveChanges();

                return comment;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public Comment? GetById(int id)
        {
            try
            {
                return _context.Comments
                    .FirstOrDefault(c => c.CommentId == id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public IEnumerable<Comment> GetAll()
        {
            try
            {
                return _context.Comments
                    .ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public Comment Update(Comment comment)
        {
            try
            {
                _context.Update(comment);
                _context.SaveChanges();

                return comment;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        public void Delete(Comment comment)
        {
            try
            {
                _context.Remove(comment);
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
