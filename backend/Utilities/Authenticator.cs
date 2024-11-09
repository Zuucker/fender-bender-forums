using System.IdentityModel.Tokens.Jwt;
using System.Text;
using backend.apiModels;
using Data;
using Microsoft.IdentityModel.Tokens;

namespace utilities
{
    public class Authenticator
    {
        private readonly DataRepository _dataRepository;
        private readonly IConfiguration _configuration;

        public Authenticator(DataRepository repository, IConfiguration configuration)
        {
            _dataRepository = repository;
            _configuration = configuration;
        }

        public async Task<bool> IsValidUser(LoginRequest request)
        {
            var user = await _dataRepository.GetUser(request.Login);

            if (user == null)
                return false;

            return request.PasswordHash == user.PasswordHash;
        }

        public async Task<string> GenerateToken(string login)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var user = await _dataRepository.GetUser(login) ?? throw new Exception("User is null");

            var claims = new[]
            {
                new System.Security.Claims.Claim("userId", user.Guid = Guid.NewGuid().ToString()),
                new System.Security.Claims.Claim("userName", user.UserName!)
            };

            await _dataRepository.UpdateAsync(user);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddHours(20),
                signingCredentials: credentials,
                claims: claims
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
