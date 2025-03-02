using Domain.Models;

namespace Application.Interfaces
{
    public interface IUserRepository: IRepository<ApplicationUser, string>
    {
        public ApplicationUser? GetByLogin(string login);
    }
}
