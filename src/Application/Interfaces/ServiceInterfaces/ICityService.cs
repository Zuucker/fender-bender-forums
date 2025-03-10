using Application.Dtos.RequestDtos;
using Domain.Models;

namespace Application.Interfaces.ServiceInterfaces
{
    public interface ICityService
    {
        IEnumerable<City> GettAllCities();

        City AddCity(AddCityRequest cityRequest);
    }
}
