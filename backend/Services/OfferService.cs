using Backend.ApiModels.Dtos;
using Backend.ApiModels.Requests;
using Data;
using Models;

namespace Backend.Services
{
    public class OfferService
    {
        private readonly DataRepository _repository;

        public OfferService(DataRepository repository)
        {
            _repository = repository;
        }

        public Offer AddOffer(AddOfferRequest offerRequest)
        {
            Offer newOffer = new(offerRequest);

            newOffer.AdditionalContents = offerRequest.AdditionalContents
                .Select(ac => new AdditionalContent(ac))
                .ToList();


            _repository.Add(newOffer);

            return newOffer;
        }

        public ICollection<OfferDto> GetAllOffers()
        {
            var offers = _repository
                .GetOfferList()
                .Select(o => new OfferDto(o))
                .ToList();

            return offers;
        }

        public OfferRate AddOfferRating(AddOfferRatingRequest ratingRequest)
        {
            OfferRate newRating = new(ratingRequest);

            _repository.Add(newRating);

            return newRating;
        }
    }
}
