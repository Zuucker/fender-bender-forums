using Application.Common;
using Application.Dtos.RequestDtos;
using Domain.Models;

namespace Application.Interfaces.ServiceInterfaces
{
    public interface ICityService
    {
        public ServiceResult<IEnumerable<City>?> GettAllCities();

        public ServiceResult<City?> AddCity(AddCityRequest cityRequest);
    }
}
