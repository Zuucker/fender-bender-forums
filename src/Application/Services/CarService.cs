using Application.Common;
using Application.Dtos.RequestDtos;
using Application.Factories;
using Application.Interfaces.RepositoryInterfaces;
using Application.Interfaces.ServiceInterfaces;
 using Domain.Models;
 
namespace Application.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        //TODO add filtering
        public ServiceResult<IEnumerable<Car>> GetAllCars()
        {
            return ServiceResult<IEnumerable<Car>>.Success(_carRepository.GetAll());
        }

        public ServiceResult<Car> AddCar(AddCarRequest carRequest)
        {
            Car newCar = CarFactory.Create(carRequest);

            _carRepository.Add(newCar);

            return ServiceResult<Car>.Success(newCar);
        }
    }
}
