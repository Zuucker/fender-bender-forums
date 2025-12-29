using Application.Common;
using Application.Dtos.ModelDtos;
using Application.Dtos.RequestDtos;
using Application.Factories;
using Application.Interfaces;
using Application.Interfaces.RepositoryInterfaces;
using Application.Interfaces.ServiceInterfaces;
using Domain.Errors;
using Domain.Models;
using System.Transactions;

namespace Application.Services
{
    public class OfferService : IOfferService
    {
        private readonly IOfferRateRepository _offerRateRepository;
        private readonly IOfferRepository _offerRepository;
        private readonly ILikeRepository _likeRepository;
        private readonly IFileStorage _fileStorage;


        public OfferService(IOfferRepository offerRepository,
            IOfferRateRepository offerRateRepository,
            ILikeRepository likeRepository,
            IFileStorage fileStorage)
        {
            _offerRateRepository = offerRateRepository;
            _offerRepository = offerRepository;
            _likeRepository = likeRepository;
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

        public ServiceResult<IEnumerable<Offer>> GetUsersOffers(Guid userId)
        {
            var offers = _offerRepository
                .GetUsersOffers(userId);

            return ServiceResult<IEnumerable<Offer>>.Success(offers);
        }

        public ServiceResult<Offer> GetById(int postId)
        {
            var offer = _offerRepository
                .GetById(postId);

            if (offer == null)
                return ServiceResult<Offer>.Fail(ApiErrors.OfferNotFound);

            return ServiceResult<Offer>.Success(offer);
        }

        public ServiceResult<Like> InteractWithOffer(InteractWithOfferRequest interactionRequest, ApplicationUser user)
        {
            using var scope = new TransactionScope();

            try
            {
                var exisitingInteraction = _likeRepository
                    .GetByOfferAndUser(interactionRequest.OfferId, user.Id);

                if (exisitingInteraction == null)
                {

                    Like newLike = LikeFactory.Create(interactionRequest);

                    _likeRepository.Add(newLike);

                    scope.Complete();

                    return ServiceResult<Like>.Success(newLike);
                }
                else
                {
                    switch ((interactionRequest.Upvoted, interactionRequest.DownVoted))
                    {
                        case (false, false):
                            _likeRepository.Delete(exisitingInteraction);
                            scope.Complete();

                            return ServiceResult<Like>.Success(null!);

                        case (true, false):
                            exisitingInteraction.Up = true;
                            _likeRepository.Update(exisitingInteraction);

                            break;

                        case (false, true):
                            exisitingInteraction.Up = false;
                            _likeRepository.Update(exisitingInteraction);

                            break;

                        case (true, true):
                            return ServiceResult<Like>.Fail(ApiErrors.WrongInteraction);
                    }

                    scope.Complete();
                    return ServiceResult<Like>.Success(exisitingInteraction);
                }
            }
            catch
            {
                scope.Dispose();
                throw;
            }
        }
    }
}
