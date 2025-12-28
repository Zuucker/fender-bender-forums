using Application.Common;
using Application.Dtos.ModelDtos;
using Application.Dtos.RequestDtos;
using Application.Factories;
using Application.Interfaces;
using Application.Interfaces.RepositoryInterfaces;
using Application.Interfaces.ServiceInterfaces;
using Domain.Models;
using System.Transactions;

namespace Application.Services
{
    public class OfferService : IOfferService
    {
        private readonly IOfferRateRepository _offerRateRepository;
        private readonly IOfferRepository _offerRepository;
        private readonly IFileStorage _fileStorage;


        public OfferService(IOfferRepository offerRepository,
            IOfferRateRepository offerRateRepository,
            IFileStorage fileStorage)
        {
            _offerRateRepository = offerRateRepository;
            _offerRepository = offerRepository;
            _fileStorage = fileStorage;
        }

        public ServiceResult<Offer> AddOffer(AddOfferRequest offerRequest)
        {
            using var scope = new TransactionScope();

            try
            {
                foreach (GalleryElementDto ge in offerRequest.Contents.SelectMany(c => c.GalleryElements))
                {
                    string path = _fileStorage
                        .WriteImageFile(ge.Base64Data.Split(",")[1]);

                    ge.Path = path;
                };

                Offer newOffer = OfferFactory.Create(offerRequest);

                _offerRepository.Add(newOffer);

                scope.Complete();

                return ServiceResult<Offer>.Success(newOffer);
            }
            catch
            {
                scope.Dispose();
                throw;
            }
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
