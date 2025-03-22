using Application.Dtos.ModelDtos;
using Application.Dtos.RequestDtos;
using Application.Factories;
using Application.Interfaces.RepositoryInterfaces;
using Application.Interfaces.ServiceInterfaces;
using Domain.Models;

namespace Application.Services
{
    public class OfferService : IOfferService
    {
        private readonly IOfferRateRepository _offerRateRepository;
        private readonly IOfferRepository _offerRepository;

        public OfferService(IOfferRepository offerRepository, IOfferRateRepository offerRateRepository)
        {
            _offerRateRepository = offerRateRepository;
            _offerRepository = offerRepository;
        }

        public Offer AddOffer(AddOfferRequest offerRequest)
        {
            Offer newOffer = OfferFactory.Create(offerRequest);

            _offerRepository.Add(newOffer);

            return newOffer;
        }

        public ICollection<OfferDto> GetAllOffers()
        {
            var offers = _offerRepository
                .GetAll()
                .Select(o => new OfferDto(o))
                .ToList();

            return offers;
        }

        public OfferRate AddOfferRating(AddOfferRatingRequest ratingRequest)
        {
            OfferRate newRating = OfferRateFactory.Create(ratingRequest);

            _offerRateRepository.Add(newRating);

            return newRating;
        }
    }
}
