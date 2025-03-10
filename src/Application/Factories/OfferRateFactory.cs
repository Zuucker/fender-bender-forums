using Application.Dtos.RequestDtos;
using Domain.Models;

namespace Application.Factories
{
    public static class OfferRateFactory
    {
        public static OfferRate Create(AddOfferRatingRequest request)
        {
            return new OfferRate()
            {
                UserId = request.UserId,
                OfferId = request.OfferId,
                Rating = request.Rating,
                Comment = request.Comment,
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}
