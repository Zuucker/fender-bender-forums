using Domain.Models;

namespace Application.Interfaces.RepositoryInterfaces
{
    public interface ISectionRepository : IRepository<Section, int>
    {
        public List<Section> GetAllSectionsWithSubSections();
    }
}
