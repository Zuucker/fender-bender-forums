using Application.Dtos.RequestDtos;
using Application.Interfaces.RepositoryInterfaces;
using Application.Interfaces.ServiceInterfaces;
using Domain.Models;

namespace Application.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;

        public CityService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        //TODO add filtering
        public IEnumerable<City> GettAllCities()
        {
            return _cityRepository.GetAll();
        }

        public City AddCity(AddCityRequest cityRequest)
        {
            City newCity = new()
            {
                Altitude = cityRequest.Altitude,
                Latitude = cityRequest.Latitude,
                Name = cityRequest.Name,
            };

            _cityRepository.Add(newCity);

            return newCity;
        }
    }
}

