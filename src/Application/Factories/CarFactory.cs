using Application.Dtos.RequestDtos;
using Domain.Models;

namespace Application.Factories
{
    public static class CarFactory
    {
        public static Car Create(AddCarRequest request)
        {
            return new Car()
            {
                Generation = request.Generation,
                Manufacturer = request.Manufacturer,
                Model = request.Model,
                Type = request.Type,
                Year = request.Year
            };
        }
    }
}
