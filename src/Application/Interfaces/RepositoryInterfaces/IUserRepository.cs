using Domain.Models;

namespace Application.Interfaces.RepositoryInterfaces
{
    public interface IUserRepository : IRepository<ApplicationUser, string>
    {
        ApplicationUser GetByUserName(string userName);
    }
}
