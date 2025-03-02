using Application.Interfaces;
using Domain.Models;

namespace Infrastructure.Persistance
{
    public class CarRepository : ICarRepository
    {
        private readonly DatabaseContext _context;

        public CarRepository(DatabaseContext context)
        {
            _context = context;
        }

        #region IRepository

        public Car Add(Car car)
        {
            try
            {
                _context.Cars.Add(car);
                _context.SaveChanges();

                return car;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public Car? GetById(int id)
        {
            try
            {
                return _context.Cars
                    .FirstOrDefault(c => c.CarId == id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public IEnumerable<Car> GetAll()
        {
            try
            {
                return _context.Cars
                    .ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public Car Update(Car car)
        {
            try
            {
                _context.Update(car);
                _context.SaveChanges();

                return car;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        public void Delete(Car car)
        {
            try
            {
                _context.Remove(car);
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
