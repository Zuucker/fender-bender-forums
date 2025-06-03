using Application.Common;
using Application.Dtos.RequestDtos;

namespace Application.Interfaces.ServiceInterfaces
{
    public interface IAuthenticatorService
    {
        ServiceResult<bool?> IsValidUser(LoginRequest request);

        ServiceResult<string?> GenerateToken(string login);

        ServiceResult<string?> HashPassword(string password);

        ServiceResult<bool?> VerifyPassword(string password, string hash);
    }
}
