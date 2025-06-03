using Application.Common;
using Application.Dtos.ModelDtos;
using Application.Interfaces.RepositoryInterfaces;
using Application.Interfaces.ServiceInterfaces;
using Domain.Errors;
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

        public ServiceResult<ApplicationUser?> RegisterUser(string username, string email, string password)
        {
            var hashResult = _authenticator.HashPassword(password);

            if (hashResult.HasFailed())
                return ServiceResult<ApplicationUser?>.Failure(hashResult.Error!.Value);

            ApplicationUser newUser = new()
            {
                UserName = username,
                NormalizedUserName = username.ToUpper(),
                Email = email,
                NormalizedEmail = email.ToUpper(),
                PasswordHash = hashResult.Data,
            };

            _userRepository.Add(newUser);

            return ServiceResult<ApplicationUser?>.Success(newUser);
        }

        public ServiceResult<ApplicationUser?> UpdateUser(ApplicationUser user, UserDto editedUser)
        {
            var isUniqueResult = IsUsernameOrEmailAreInUse(user, editedUser.UserName, editedUser.Email);

            if(isUniqueResult.HasFailed())
                return ServiceResult<ApplicationUser?>.Failure(isUniqueResult.Error!.Value);


            user.UserName = editedUser.UserName;
            user.NormalizedUserName = editedUser.UserName.ToUpper();

            user.Email = editedUser.Email;
            user.NormalizedEmail = editedUser.Email.ToUpper();

            if (!string.IsNullOrEmpty(editedUser.NewPassword) && !string.IsNullOrEmpty(editedUser.ConfirmPassword))
            {
                var hashResult = _authenticator.HashPassword(editedUser.NewPassword);

                if (hashResult.HasFailed())
                    return ServiceResult<ApplicationUser?>.Failure(hashResult.Error!.Value);

                user.PasswordHash = hashResult.Data;
            }

            _userRepository.Update(user);

            return ServiceResult<ApplicationUser?>.Success(user);
        }

        public ServiceResult<ApplicationUser?> GetUserById(string userId)
        {
            var user = _userRepository.GetById(userId);

            if (user == null)
                return ServiceResult<ApplicationUser?>.Failure(ApiErrors.UserNotFound);

            return ServiceResult<ApplicationUser?>.Success(user);
        }

        public ServiceResult<bool?> IsUsernameOrEmailAreInUse(ApplicationUser user, string userName, string email)
        {
            var userByUsername = _userRepository.GetByUserName(userName);
            var userByEmail = _userRepository.GetByEmail(email);

            if (userByUsername != null && userByUsername.Id != user.Id)
                return ServiceResult<bool?>.Failure(ApiErrors.UsernameAlreadyInUse);

            if (userByEmail != null && userByEmail.Id != user.Id)
                return ServiceResult<bool?>.Failure(ApiErrors.EmailAlreadyInUse);

            return ServiceResult<bool?>.Success(false);
        }
    }
}
