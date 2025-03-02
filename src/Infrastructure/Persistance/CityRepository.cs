using Domain.Models;

namespace Infrastructure.Persistance
{
    public class CityRepository
    {
        private readonly DatabaseContext _context;

        public CityRepository(DatabaseContext context)
        {
            _context = context;
        }

        #region IRepository

        public City Add(City city)
        {
            try
            {
                _context.Cities.Add(city);
                _context.SaveChanges();

                return city;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public City? GetById(int id)
        {
            try
            {
                return _context.Cities
                    .FirstOrDefault(c => c.CityId == id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public IEnumerable<City> GetAll()
        {
            try
            {
                return _context.Cities
                    .ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public City Update(City cities)
        {
            try
            {
                _context.Update(cities);
                _context.SaveChanges();

                return cities;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        public void Delete(City cities)
        {
            try
            {
                _context.Remove(cities);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        #endregion
    }
}
