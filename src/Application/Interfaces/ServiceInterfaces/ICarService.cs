using Application.Dtos.RequestDtos;
using Domain.Models;

namespace Application.Interfaces.ServiceInterfaces
{
    public interface ICarService
    {
        public IEnumerable<Car> GetAllCars();

        public Car AddCar(AddCarRequest carRequest);
    }
}
