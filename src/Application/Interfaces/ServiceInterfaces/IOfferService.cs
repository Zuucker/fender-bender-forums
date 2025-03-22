using Application.Dtos.ModelDtos;
using Application.Dtos.RequestDtos;
using Domain.Models;

namespace Application.Interfaces.ServiceInterfaces
{
    public interface IOfferService
    {
        Offer AddOffer(AddOfferRequest offerRequest);

        ICollection<OfferDto> GetAllOffers();

        OfferRate AddOfferRating(AddOfferRatingRequest ratingRequest);
    }
}
