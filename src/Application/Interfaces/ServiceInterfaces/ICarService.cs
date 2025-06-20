using Application.Common;
using Application.Dtos.RequestDtos;
using Domain.Models;

namespace Application.Interfaces.ServiceInterfaces
{
    public interface ICarService
    {
        public ServiceResult<IEnumerable<Car>> GetAllCars();

        public ServiceResult<Car> AddCar(AddCarRequest carRequest);
    }
}
