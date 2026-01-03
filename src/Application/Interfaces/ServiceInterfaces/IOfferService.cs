using Application.Common;
using Application.Dtos.RequestDtos;
using Domain.Models;

namespace Application.Interfaces.ServiceInterfaces
{
    public interface IOfferService
    {
        public ServiceResult<Offer> AddOffer(AddOfferRequest offerRequest);

        public ServiceResult<IEnumerable<Offer>> GetAllOffers();

        public ServiceResult<OfferRate> AddOfferRating(AddOfferRatingRequest ratingRequest);

        public ServiceResult<IEnumerable<Offer>> GetUsersOffers(Guid userId);

        public ServiceResult<Offer> GetById(int postId);

        public ServiceResult<Like> InteractWithOffer(InteractWithOfferRequest interactionRequest, ApplicationUser user);

        public ServiceResult<(IEnumerable<Offer>, string?)> GetFilteredOffers(FiltersDto filters, string? cursor);
    }
}
