using Domain.Models;

namespace Infrastructure.Persistance.Data
{
    public class OfferRepository
    {
        private readonly DatabaseContext _context;

        public OfferRepository(DatabaseContext context)
        {
            _context = context;
        }

        #region IRepository

        public Offer Add(Offer offer)
        {
            try
            {
                _context.Offers.Add(offer);
                _context.SaveChanges();

                return offer;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public Offer? GetById(int id)
        {
            try
            {
                return _context.Offers
                    .FirstOrDefault(o => o.OfferId == id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public IEnumerable<Offer> GetAll()
        {
            try
            {
                return _context.Offers
                    .ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public Offer Update(Offer offer)
        {
            try
            {
                _context.Update(offer);
                _context.SaveChanges();

                return offer;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        public void Delete(Offer offer)
        {
            try
            {
                _context.Remove(offer);
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
