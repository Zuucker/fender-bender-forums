using Application.Common;
using Application.Dtos.RequestDtos;
using Application.Interfaces.RepositoryInterfaces;
using Application.Interfaces.ServiceInterfaces;
using Domain.Errors;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services
{
    public class AuthenticatorService : IAuthenticatorService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthenticatorService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public ServiceResult<bool> IsValidUser(LoginRequest request)
        {
            var user = _userRepository.GetByUserName(request.Login)
                ?? _userRepository.GetByEmail(request.Login);

            if (user == null)
                return ServiceResult<bool>.Fail(ApiErrors.UserNotFound);



            var verifyResult = VerifyPassword(request.Password, user.PasswordHash ?? string.Empty);

            if (verifyResult.HasFailed())
                return ServiceResult<bool>.Fail(verifyResult.Error.GetValueOrDefault());


            return ServiceResult<bool>.Success(verifyResult.Data);
        }

        public ServiceResult<string> GenerateToken(string login)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var user = _userRepository.GetByUserName(login)
                ?? _userRepository.GetByEmail(login);

            if (user == null)
                return ServiceResult<string>.Fail(ApiErrors.UserNotFound);

            var claims = new[]
            {
                new Claim(Constants.ClaimsConstants.UserIdClaim, user.Id),
                new Claim(Constants.ClaimsConstants.UserNameClaim, user.UserName!)
            };

            var tokenData = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddHours(20),
                signingCredentials: credentials,
                claims: claims
            );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenData);

            return ServiceResult<string>.Success(token);
        }

        public ServiceResult<string> HashPassword(string password)
        {
            var passwordHasher = new PasswordHasher<ApplicationUser>();

            //user is not necesarry
            var hashedPassword = passwordHasher.HashPassword(null!, password);

            return ServiceResult<string>.Success(hashedPassword);
        }

        public ServiceResult<bool> VerifyPassword(string password, string hash)
        {
            var passwordHasher = new PasswordHasher<ApplicationUser>();

            var result = passwordHasher
                .VerifyHashedPassword(null!, hash, password) == PasswordVerificationResult.Success;

            if (!result)
                return ServiceResult<bool>.Fail(ApiErrors.PasswordsDontMatch);

            return ServiceResult<bool>.Success(result);
        }
    }
}
