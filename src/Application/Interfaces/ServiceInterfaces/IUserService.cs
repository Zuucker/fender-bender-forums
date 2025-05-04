using Domain.Models;

namespace Application.Interfaces.ServiceInterfaces
{
    public interface IUserService
    {
        ApplicationUser RegisterUser(string username, string email, string password);

        ApplicationUser? UpdateUser(string userId, string? username, string? email);

        ApplicationUser? GetUserById(string userId);
    }
}
