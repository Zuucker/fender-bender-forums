using Application.Common;
using Application.Dtos.RequestDtos;
using Domain.Models;

namespace Application.Interfaces.ServiceInterfaces
{
    public interface IOfferService
    {
        public ServiceResult<Offer?> AddOffer(AddOfferRequest offerRequest);

        public ServiceResult<IEnumerable<Offer>?> GetAllOffers();

        public ServiceResult<OfferRate?> AddOfferRating(AddOfferRatingRequest ratingRequest);
    }
}
