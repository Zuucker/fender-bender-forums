using Application.Common;
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

        public ServiceResult<Offer> AddOffer(AddOfferRequest offerRequest)
        {
            Offer newOffer = OfferFactory.Create(offerRequest);

            _offerRepository.Add(newOffer);

            return ServiceResult<Offer>.Success(newOffer);
        }

        public ServiceResult<IEnumerable<Offer>> GetAllOffers()
        {
            var offers = _offerRepository
                .GetAll();

            return ServiceResult<IEnumerable<Offer>>.Success(offers);
        }

        public ServiceResult<OfferRate> AddOfferRating(AddOfferRatingRequest ratingRequest)
        {
            OfferRate newRating = OfferRateFactory.Create(ratingRequest);

            _offerRateRepository.Add(newRating);

            return ServiceResult<OfferRate>.Success(newRating);
        }
    }
}
