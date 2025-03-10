using Application.Dtos.RequestDtos;

namespace Application.Interfaces.ServiceInterfaces
{
    public interface IAuthenticatorService
    {
        bool IsValidUser(LoginRequest request);

        string GenerateToken(string login);

        string HashPassword(string password);
    }
}
