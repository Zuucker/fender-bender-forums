using Application.Dtos.RequestDtos;
using Application.Interfaces.RepositoryInterfaces;
using Application.Services;
using Application.Common;
using Domain.Errors;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Moq;
using System.IdentityModel.Tokens.Jwt;

namespace Tests.UnitTests
{
    public class AuthenticatorServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly IConfiguration _configuration;
        private readonly AuthenticatorService _authService;

        public AuthenticatorServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();

            var inMemorySettings = new Dictionary<string, string>
            {
                { "Jwt:Key", "very_secret_test_key_12345678901" },
                { "Jwt:Issuer", "test-issuer" },
                { "Jwt:Audience", "test-audience" }
            };

            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings!)
                .Build();

            _authService = new AuthenticatorService(_userRepositoryMock.Object, _configuration);
        }

        [Fact]
        public void IsValidUser_Fails_WhenUserNotFound()
        {
            var request = new LoginRequest
            {
                Login = "missing",
                Password = "password"
            };

            _userRepositoryMock.Setup(r => r.GetByUserName(It.IsAny<string>()))
                .Returns((ApplicationUser?)null);
            _userRepositoryMock.Setup(r => r.GetByEmail(It.IsAny<string>()))
                .Returns((ApplicationUser?)null);

            var result = _authService.IsValidUser(request);

            Assert.True(result.HasFailed());
            Assert.Equal(ApiErrors.UserNotFound, result.Error);
        }

        [Fact]
        public void IsValidUser_Fails_WhenPasswordIsInvalid()
        {
            var user = new ApplicationUser
            {
                UserName = "test",
                PasswordHash = _authService.HashPassword("correct").Data
            };

            _userRepositoryMock.Setup(r => r.GetByUserName("test"))
                .Returns(user);

            var request = new LoginRequest
            {
                Login = "test",
                Password = "wrong"
            };

            var result = _authService.IsValidUser(request);

            Assert.True(result.HasFailed());
            Assert.Equal(ApiErrors.PasswordsDontMatch, result.Error);
        }

        [Fact]
        public void IsValidUser_Succeeds_WhenPasswordIsValid()
        {
            var hash = _authService.HashPassword("password").Data;

            var user = new ApplicationUser
            {
                Id = "1",
                UserName = "test",
                PasswordHash = hash
            };

            _userRepositoryMock.Setup(r => r.GetByUserName("test"))
                .Returns(user);

            var request = new LoginRequest
            {
                Login = "test",
                Password = "password"
            };

            var result = _authService.IsValidUser(request);

            Assert.False(result.HasFailed());
            Assert.True(result.Data);
        }

        [Fact]
        public void GenerateToken_Fails_WhenUserNotFound()
        {
            _userRepositoryMock.Setup(r => r.GetByUserName(It.IsAny<string>()))
                .Returns((ApplicationUser?)null);
            _userRepositoryMock.Setup(r => r.GetByEmail(It.IsAny<string>()))
                .Returns((ApplicationUser?)null);

            var result = _authService.GenerateToken("missing");

            Assert.True(result.HasFailed());
            Assert.Equal(ApiErrors.UserNotFound, result.Error);
        }

        [Fact]
        public void GenerateToken_ReturnsValidJwt_WhenUserExists()
        {
            var user = new ApplicationUser
            {
                Id = "42",
                UserName = "testuser"
            };

            _userRepositoryMock.Setup(r => r.GetByUserName("testuser"))
                .Returns(user);

            var result = _authService.GenerateToken("testuser");

            Assert.False(result.HasFailed());
            Assert.False(string.IsNullOrWhiteSpace(result.Data));

            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(result.Data);

            Assert.Equal("test-issuer", jwt.Issuer);
            Assert.Contains(jwt.Claims, c => c.Type == Constants.ClaimsConstants.UserIdClaim && c.Value == "42");
            Assert.Contains(jwt.Claims, c => c.Type == Constants.ClaimsConstants.UserNameClaim && c.Value == "testuser");
        }

        [Fact]
        public void HashPassword_ReturnsNonEmptyHash()
        {
            var result = _authService.HashPassword("password");

            Assert.False(result.HasFailed());
            Assert.False(string.IsNullOrWhiteSpace(result.Data));
            Assert.NotEqual("password", result.Data);
        }

        [Fact]
        public void VerifyPassword_Fails_WhenHashDoesNotMatch()
        {
            var hash = _authService.HashPassword("password").Data;

            var result = _authService.VerifyPassword("wrong", hash);

            Assert.True(result.HasFailed());
            Assert.Equal(ApiErrors.PasswordsDontMatch, result.Error);
        }

        [Fact]
        public void VerifyPassword_Succeeds_WhenHashMatches()
        {
            var hash = _authService.HashPassword("password").Data;

            var result = _authService.VerifyPassword("password", hash!);

            Assert.False(result.HasFailed());
            Assert.True(result.Data);
        }
    }
}
