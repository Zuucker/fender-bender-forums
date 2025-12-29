using Domain.Models;

namespace Application.Interfaces.RepositoryInterfaces
{
    public interface IOfferRepository : IRepository<Offer, int>
    {
        public IEnumerable<Offer> GetUsersOffers(Guid userId);
    }
}
