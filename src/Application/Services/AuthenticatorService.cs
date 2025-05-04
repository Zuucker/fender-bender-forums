using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Application.Interfaces.ServiceInterfaces;
using Application.Interfaces.RepositoryInterfaces;
using Microsoft.Extensions.Configuration;
using Application.Dtos.RequestDtos;
using System.Security.Claims;

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

        public bool IsValidUser(LoginRequest request)
        {
            var user = _userRepository.GetByUserName(request.Login)
                ?? _userRepository.GetByEmail(request.Login);

            if (user == null)
                return false;

            return VerifyPassword(request.Password, user.PasswordHash ?? string.Empty);
        }

        public string GenerateToken(string login)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var user = _userRepository.GetByUserName(login)
                ?? _userRepository.GetByEmail(login) 
                ?? throw new Exception("User is null");

            var claims = new[]
            {
                new Claim("userId", user.Id),
                new Claim("userName", user.UserName!)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddHours(20),
                signingCredentials: credentials,
                claims: claims
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public ClaimsPrincipal GetClaimsFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Convert.FromBase64String(_configuration["Jwt:Key"]!);

            try
            {
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidAudience = _configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);

                return principal;
            }
            catch (Exception ex)
            {
                throw new UnauthorizedAccessException("Invalid or expired token", ex);
            }
        }

        public string HashPassword(string password)
        {
            var passwordHasher = new PasswordHasher<object>();

            //user is not necesarry
            return passwordHasher.HashPassword(null!, password);
        }

        private bool VerifyPassword(string password, string hash)
        {
            var passwordHasher = new PasswordHasher<object>();

            return passwordHasher.VerifyHashedPassword(null!, hash, password) == PasswordVerificationResult.Success;
        }
    }
}
