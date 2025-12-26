using Application.Common;
 using Application.Interfaces.RepositoryInterfaces;
using Application.Interfaces.ServiceInterfaces;
using Domain.Errors;
using Domain.Models;

 
namespace Application.Services
{
    public class SectionService : ISectionService
    {
        private readonly ISectionRepository _sectionRepository;

        public SectionService(ISectionRepository sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }

        public ServiceResult<List<Section>> GetSections()
        {
            var sections = _sectionRepository.GetAllSectionsWithSubSections();

            if (sections.Count() == 0)
                return ServiceResult<List<Section>>.Fail(ApiErrors.EmptyList);

            return ServiceResult<List<Section>>.Success(sections);
        }
    }
}

