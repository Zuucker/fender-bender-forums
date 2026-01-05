using Application.Common;
using Application.Dtos.ModelDtos;
using Application.Dtos.RequestDtos;
using Application.Factories;
using Application.Interfaces;
using Application.Interfaces.RepositoryInterfaces;
using Application.Interfaces.ServiceInterfaces;
using Domain.Errors;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace Application.Services
{
    public class OfferService : IOfferService
    {
        private readonly IOfferRepository _offerRepository;
        private readonly ILikeRepository _likeRepository;
        private readonly ICursorService _cursorService;
        private readonly IFileStorage _fileStorage;
        private readonly int _defaultPageSize = 10;


        public OfferService(IOfferRepository offerRepository,
            ILikeRepository likeRepository,
            ICursorService cursorService,
            IFileStorage fileStorage)
        {
            _offerRepository = offerRepository;
            _likeRepository = likeRepository;
            _cursorService = cursorService;
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

        public ServiceResult<(IEnumerable<Offer>, string?)> GetFilteredOffers(FiltersDto filters, string? cursor)
        {
            using var scope = new TransactionScope();

            try
            {
                var query = _offerRepository
                    .GetOffersQuery();


                // Filtering
                if (!string.IsNullOrWhiteSpace(filters.Title))
                    query = query.Where(x => x.Offer.Title.Contains(filters.Title));

                if (filters.MinPrice.HasValue)
                    query = query.Where(x => x.Offer.Price >= filters.MinPrice.Value);

                if (filters.MaxPrice.HasValue)
                    query = query.Where(x => x.Offer.Price <= filters.MaxPrice.Value);

                if (filters.CarId.HasValue)
                    query = query.Where(x => x.Offer.CarId == filters.CarId.Value);

                if (filters.CityId.HasValue)
                    query = query.Where(x => x.Offer.CityId == filters.CityId.Value);

                if (!string.IsNullOrWhiteSpace(filters.AuthorId))
                    query = query.Where(x => x.Offer.AuthorId == filters.AuthorId);

                if (!string.IsNullOrWhiteSpace(filters.Condition))
                    query = query.Where(x => x.Offer.Condition == filters.Condition);

                if (!string.IsNullOrWhiteSpace(filters.Fuel))
                    query = query.Where(x => x.Offer.Fuel == filters.Fuel);

                if (!string.IsNullOrWhiteSpace(filters.Color))
                    query = query.Where(x => x.Offer.Color == filters.Color);

                if (filters.Mileage.HasValue)
                    query = query.Where(x => x.Offer.Mileage <= filters.Mileage.Value);

                if (!string.IsNullOrWhiteSpace(filters.Tags))
                {
                    var tagTexts = filters.Tags.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    var jsonConditions = tagTexts.Select(tag => $"[{{\"Text\":\"{tag}\"}}]").ToList();

                    query = query.Where(x =>
                        jsonConditions.Any(jsonCondition =>
                            EF.Functions.JsonContains(x.Offer.Tags, jsonCondition)
                        )
                        || (x.Offer.Car != null && tagTexts.Any(tag =>
                            x.Offer.Car.Model.Contains(tag)
                            || x.Offer.Car.Manufacturer.Contains(tag)
                            || x.Offer.Car.Type.Contains(tag)
                        ))
                    );
                }

                if (!string.IsNullOrWhiteSpace(filters.PartNumber))
                    query = query.Where(x => x.Offer.PartNumber == filters.PartNumber);

                if (!string.IsNullOrWhiteSpace(filters.Type))
                    query = query.Where(x => x.Offer.Type == filters.Type);

                if (filters.CreationDate.HasValue)
                    query = query.Where(x => x.Offer.Date >= filters.CreationDate.Value);



                var decodedCursor =
                    _cursorService.DecodeCursor(cursor);

                query = query
                    .OrderByDescending(x => x.Offer.Date)
                    .ThenByDescending(o => o.LikeCount)
                    .ThenByDescending(x => x.Offer.OfferId);

                if (decodedCursor != null)
                {
                    query = query.Where(x =>
                       x.Offer.Date < decodedCursor.CreatedAt
                       || (x.Offer.Date == decodedCursor.CreatedAt && x.LikeCount < decodedCursor.LikeCount)
                       || (x.Offer.Date == decodedCursor.CreatedAt
                           && x.LikeCount == decodedCursor.LikeCount
                           && x.Offer.OfferId < decodedCursor.OfferId)
                   );
                }

                var items = query
                    .Take(decodedCursor?.Take ?? _defaultPageSize)
                    .AsNoTracking()
                    .ToList();

                var nextCursor = items.Count == (decodedCursor?.Take ?? _defaultPageSize)
                    ? _cursorService.EncodeCursor(new OfferCursorDto
                    {
                        CreatedAt = items[^1].Offer.Date,
                        LikeCount = items[^1].LikeCount,
                        OfferId = items[^1].Offer.OfferId
                    })
                    : null;


                var offers = items
                    .Select(i => i.Offer);


                return ServiceResult<(IEnumerable<Offer>, string?)>.Success((offers, nextCursor));
            }
            catch
            {
                scope.Dispose();
                throw;
            }
        }
    }
}
