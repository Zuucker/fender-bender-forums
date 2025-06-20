using Application.Common;
using Domain.Models;


namespace Application.Interfaces.ServiceInterfaces
{
    public interface ISectionService
    {
        ServiceResult<List<Section>> GetSections();
    }
}

