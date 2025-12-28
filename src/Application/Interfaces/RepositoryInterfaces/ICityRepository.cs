using Domain.Models;

namespace Application.Interfaces.RepositoryInterfaces
{
    public interface ICityRepository : IRepository<City, int>
    {
        public IEnumerable<City> GetFromCountry(string countryCode);
    }
}
