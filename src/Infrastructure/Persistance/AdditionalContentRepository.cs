using Application.Interfaces;
using Domain.Models;

namespace Infrastructure.Persistance
{
    public class AdditionalContentRepository : IAdditionalContentRepository
    {
        private readonly DatabaseContext _context;

        public AdditionalContentRepository(DatabaseContext context)
        {
            _context = context;
        }

        #region IRepository

        public AdditionalContent Add(AdditionalContent additionalContent)
        {
            try
            {
                _context.AdditionalContents.Add(additionalContent);
                _context.SaveChanges();

                return additionalContent;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public AdditionalContent? GetById(int id)
        {
            try
            {
                return _context.AdditionalContents
                    .FirstOrDefault(ac => ac.AdditionalContentId == id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public IEnumerable<AdditionalContent> GetAll()
        {
            try
            {
                return _context.AdditionalContents
                    .ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public AdditionalContent Update(AdditionalContent additionalContent)
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
        public void Delete(AdditionalContent additionalContent)
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
