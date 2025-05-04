using Application.Interfaces.RepositoryInterfaces;
using Application.Interfaces.ServiceInterfaces;
using Domain.Models;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticatorService _authenticator;

        public UserService(IUserRepository userRepository, IAuthenticatorService authenticator)
        {
            _userRepository = userRepository;
            _authenticator = authenticator;
        }

        public ApplicationUser RegisterUser(string username, string email, string password)
        {
            ApplicationUser newUser = new()
            {
                UserName = username,
                NormalizedUserName = username.ToUpper(),
                Email = email,
                NormalizedEmail = email.ToUpper(),
                PasswordHash = _authenticator.HashPassword(password)
            };

            return _userRepository.Add(newUser);
        }

        public ApplicationUser? UpdateUser(string userId, string? username, string? email)
        {
            var user = _userRepository.GetById(userId);

            if (user == null)
                return null;

            if (!string.IsNullOrEmpty(username))
            {
                user.UserName = username;
                user.NormalizedUserName = username.ToUpper();
            }

            if (!string.IsNullOrEmpty(email))
            {
                user.Email = email;
                user.NormalizedEmail = email.ToUpper();
            }

            _userRepository.Update(user);
            return user;
        }

        public ApplicationUser? GetUserById(string userId)
        {
            return _userRepository.GetById(userId);
        }
    }
}
