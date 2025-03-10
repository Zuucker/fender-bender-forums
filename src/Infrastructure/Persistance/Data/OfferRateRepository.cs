using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;

namespace Infrastructure.Persistance.Data
{
    public class OfferRateRepository : IOfferRateRepository
    {
        private readonly DatabaseContext _context;

        public OfferRateRepository(DatabaseContext context)
        {
            _context = context;
        }

        #region IRepository

        public OfferRate Add(OfferRate offerRate)
        {
            try
            {
                _context.OfferRates.Add(offerRate);
                _context.SaveChanges();

                return offerRate;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public OfferRate? GetById(int id)
        {
            try
            {
                return _context.OfferRates
                    .FirstOrDefault(or => or.OfferRateId == id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public IEnumerable<OfferRate> GetAll()
        {
            try
            {
                return _context.OfferRates
                    .ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public OfferRate Update(OfferRate offerRate)
        {
            try
            {
                _context.Update(offerRate);
                _context.SaveChanges();

                return offerRate;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        public void Delete(OfferRate offerRate)
        {
            try
            {
                _context.Remove(offerRate);
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
