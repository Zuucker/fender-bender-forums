using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance.DataRepositories
{
    public class OfferRepository : IOfferRepository
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
                    .Include(p => p.Contents)
                        .ThenInclude(c => c.GalleryElements)
 
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
                    .Include(p => p.City)
                    .FirstOrDefault(p => p.OfferId == id);
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
                    .Include(o => o.User)
                    .Include(o => o.Car)
                    .Include(o => o.City)
                    .Include(o => o.Contents)
                        .ThenInclude(c => c.GalleryElements)
                    .Include(o => o.Comments)
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


        public IEnumerable<Offer> GetUsersOffers(Guid userId)
        {
            try
            {
                return _context.Offers
                    .Where(p => p.AuthorId == userId.ToString())
                    .Include(p => p.User)
                    .Include(p => p.Contents)
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
