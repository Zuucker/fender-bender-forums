using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance.DataRepositories
{
    public class SectionRepository : ISectionRepository
    {
        private readonly DatabaseContext _context;

        public SectionRepository(DatabaseContext context)
        {
            _context = context;
        }

        #region IRepository

        public Section Add(Section section)
        {
            try
            {
                _context.Sections.Add(section);
                _context.SaveChanges();

                return section;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public Section? GetById(int id)
        {
            try
            {
                return _context.Sections
                    .FirstOrDefault(s => s.SectionId == id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public IEnumerable<Section> GetAll()
        {
            try
            {
                return _context.Sections
                    .ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public Section Update(Section section)
        {
            try
            {
                _context.Update(section);
                _context.SaveChanges();

                return section;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        public void Delete(Section section)
        {
            try
            {
                _context.Remove(section);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        #endregion

        public List<Section> GetAllSectionsWithSubSections()
        {
            try
            {
                return _context.Sections
                    .Where(s => s.ParentSectionId == null)
                    .Include(s => s.SubSections)
                    .ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}
