using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;

namespace Infrastructure.Persistance.DataRepositories
{
    public class ContentRepository : IContentRepository
    {
        private readonly DatabaseContext _context;

        public ContentRepository(DatabaseContext context)
        {
            _context = context;
        }

        #region IRepository

        public Content Add(Content additionalContent)
        {
            try
            {
                _context.Contents.Add(additionalContent);
                _context.SaveChanges();

                return additionalContent;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public Content? GetById(int id)
        {
            try
            {
                return _context.Contents
                    .FirstOrDefault(ac => ac.ContentId == id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public IEnumerable<Content> GetAll()
        {
            try
            {
                return _context.Contents
                    .ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public Content Update(Content additionalContent)
        {
            try
            {
                _context.Update(additionalContent);
                _context.SaveChanges();

                return additionalContent;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        public void Delete(Content additionalContent)
        {
            try
            {
                _context.Remove(additionalContent);
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
