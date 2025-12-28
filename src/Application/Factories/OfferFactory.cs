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
                Title = request.Title,
                Color = request.Color,
                Condition = request.Condition,
                Date = DateTime.UtcNow,
                Fuel = request.Fuel,
                Mileage = request.Mileage,
                VIN = request.VIN,
                Price = request.Price,
                PartNumber = request.PartNumber,
                Tags = request.Tags,
                Type = request.Type,
                Contents = request.Contents
                    .Select(ac => ContentFactory.Create(ac))
                    .ToList()
            };
        }
    }
}
