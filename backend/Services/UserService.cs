using Data;
using Models;
using Utilities;

namespace Backend.Services
{
    public class UserService
    {
        private readonly DataRepository _repository;
        private readonly Authenticator _authenticator;

        public UserService(DataRepository repository, Authenticator authenticator)
        {
            _repository = repository;
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

            return _repository.AddUser(newUser);
        }
    }
}
