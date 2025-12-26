using Tests.TestUtils;
using Application.Dtos.RequestDtos;
using System.Net.Http.Json;
using System.Net;
using Presentation.Responses;
using Domain.Errors;

namespace Tests.IntegrationTests
{
    public class AuthControllerTests : IClassFixture<ApplicationFactory<Program>>
    {
        private readonly ApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public AuthControllerTests(ApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        #region Login

        [Fact]
        public async Task Login_WithValidData_ResultsOk()
        {
            LoginRequest payload = new() { Login = "testUser", Password = "Password" };

            var response = await _client.PostAsJsonAsync("/api/auth/login", payload);

            var result = await response.Content.ReadFromJsonAsync<ApiResponseDto<string>>();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.False(string.IsNullOrEmpty(result?.Data));
        }


        [Fact]
        public async Task Login_WithWrongPassword_ResultsBadRequest()
        {
            LoginRequest payload = new() { Login = "testUser", Password = "Password1" };

            var response = await _client.PostAsJsonAsync("/api/auth/login", payload);

            var result = await response.Content.ReadFromJsonAsync<ApiResponseDto<bool>>();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal(ApiErrors.PasswordsDontMatch.ToString(), result?.Error);
        }


        [Fact]
        public async Task Login_WithWrongUsername_ResultsBadRequest()
        {
            LoginRequest payload = new() { Login = "testUser1", Password = "Password" };

            var response = await _client.PostAsJsonAsync("/api/auth/login", payload);

            var result = await response.Content.ReadFromJsonAsync<ApiResponseDto<bool>>();

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Equal(ApiErrors.UserNotFound.ToString(), result?.Error);
        }

        #endregion



        #region Register

        [Fact]
        public async Task Register_WithValidData_ResultsOk()
        {
            RegisterRequest payload = new()
            {
                Email = "user2@test.com",
                Username = "user2",
                Password = "Password",
                ConfirmPassword = "Password"
            };

            var response = await _client.PostAsJsonAsync("/api/auth/register", payload);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }


        [Fact]
        public async Task Register_WithWrongPasswords_ResultsBadRequest()
        {
            RegisterRequest payload = new()
            {
                Email = "user2@test.com",
                Username = "user2",
                Password = "Password1",
                ConfirmPassword = "Password2"
            };

            var response = await _client.PostAsJsonAsync("/api/auth/register", payload);

            var result = await response.Content.ReadFromJsonAsync<ApiResponseDto<object>>();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal(ApiErrors.PasswordsDontMatch.ToString(), result?.Error);
        }

        #endregion
    }
}
