using Domain.Models;

namespace Application.Interfaces.RepositoryInterfaces
{
    public interface IUserRepository : IRepository<ApplicationUser, string>
    {
        ApplicationUser? GetByUserName(string userName);

        ApplicationUser? GetByEmail(string email);

        public ApplicationUser? GetByIdWithPoints(string id);
    }
}
