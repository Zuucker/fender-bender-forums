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
        public IEnumerable<Car> GetAllCars()
        {
            return _carRepository.GetAll();
        }

        public Car AddCar(AddCarRequest carRequest)
        {
            Car newCar = CarFactory.Create(carRequest);

            _carRepository.Add(newCar);

            return newCar;
        }
    }
}
