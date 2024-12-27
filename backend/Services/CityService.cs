using Backend.ApiModels.Requests;
using Data;
using Models;

namespace Backend.Services
{
    public class CityService
    {
        private readonly DataRepository _repository;

        public CityService(DataRepository repository)
        {
            _repository = repository;
        }

        //TODO add filtering
        public ICollection<City> GettAllCities()
        {
            return _repository.GetAll<City>();
        }

        public City AddCity(AddCityRequest cityRequest)
        {
            City newCity = new(cityRequest);

            _repository.Add(newCity);

            return newCity;
        }
    }
}

