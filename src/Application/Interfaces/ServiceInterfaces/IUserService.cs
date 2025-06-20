using Application.Common;
using Application.Dtos.ModelDtos;
using Domain.Models;

namespace Application.Interfaces.ServiceInterfaces
{
    public interface IUserService
    {
        ServiceResult<ApplicationUser> RegisterUser(string username, string email, string password);

        ServiceResult<ApplicationUser> UpdateUser(ApplicationUser user, UserDto userDto);

        ServiceResult<ApplicationUser> GetUserById(string userId);
    }
}
