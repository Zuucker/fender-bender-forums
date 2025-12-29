using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;

namespace Infrastructure.Persistance.DataRepositories
{
    public class LikeRepository : ILikeRepository
    {
        private readonly DatabaseContext _context;

        public LikeRepository(DatabaseContext context)
        {
            _context = context;
        }

        #region IRepository

        public Like Add(Like like)
        {
            try
            {
                _context.Likes.Add(like);
                _context.SaveChanges();

                return like;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public Like? GetById(int id)
        {
            try
            {
                return _context.Likes
                    .FirstOrDefault(l => l.LikeId == id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public IEnumerable<Like> GetAll()
        {
            try
            {
                return _context.Likes
                    .ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public Like Update(Like like)
        {
            try
            {
                _context.Update(like);
                _context.SaveChanges();

                return like;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        public void Delete(Like like)
        {
            try
            {
                _context.Remove(like);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        #endregion


        public Like? GetByPostAndUser(int postId, string userId)
        {
            try
            {
                return _context.Likes
                    .FirstOrDefault(l => l.PostId == postId && l.AuthorId == userId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public Like? GetByOfferAndUser(int offerId, string userId)
        {
            try
            {
                return _context.Likes
                    .FirstOrDefault(l => l.OfferId == offerId && l.AuthorId == userId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public Like? GetByCommentAndUser(int commentId, string userId)
        {
            try
            {
                return _context.Likes
                    .FirstOrDefault(l => l.CommentId == commentId && l.AuthorId == userId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}
