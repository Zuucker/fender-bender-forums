using Application.Dtos.RequestDtos;
using Domain.Models;

namespace Application.Factories
{
    public static class OfferFactory
    {
        public static Offer Create(AddOfferRequest request)
        {
            return new Offer()
            {
                AuthorId = request.AuthorId,
                CarId = request.CarId,
                CityId = request.CityId,
                Color = request.Color,
                Condition = request.Condition,
                Date = request.Date,
                Fuel = request.Fuel,
                Mileage = request.Mileage,
                Price = request.Price,
                Tags = request.Tags,
                AdditionalContents = request.AdditionalContents
                    .Select(ac => AdditionalContentFactory.Create(ac))
                    .ToList()
            };
        }
    }
}
