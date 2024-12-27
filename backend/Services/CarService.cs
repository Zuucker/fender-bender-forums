using Backend.ApiModels.Requests;
using Data;
using Models;

namespace Backend.Services
{
    public class CarService
    {
        private readonly DataRepository _repository;

        public CarService(DataRepository repository)
        {
            _repository = repository;
        }

        //TODO add filtering
        public ICollection<Car> GetAllCars()
        {
            return _repository.GetAll<Car>();
        }

        public Car AddCar(AddCarRequest carRequest)
        {
            Car newCar = new(carRequest);

            _repository.Add(newCar);

            return newCar;
        }
    }
}
