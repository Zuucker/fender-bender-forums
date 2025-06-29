using Application.Dtos.ModelDtos;
using Presentation.Responses;
using System.Net;
using System.Net.Http.Json;
using Tests.TestUtils;

namespace Tests.IntegrationTests
{
    public class UserControllerTests : IClassFixture<ApplicationFactory<Program>>
    {
        private readonly ApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public UserControllerTests(ApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        #region Login

        [Fact]
        public async Task GetUserData_WithValidData_ResultsOk()
        {
            var response = await _client.GetAsync("/api/user/get/391a1bf7-c1de-49f1-b14f-0bddc2a02d72");

            var result = await response.Content.ReadFromJsonAsync<ApiResponseDto<UserDto>>();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("391a1bf7-c1de-49f1-b14f-0bddc2a02d72", result?.Data?.Id);
            Assert.False(string.IsNullOrEmpty(result?.Data?.UserName));
            Assert.False(string.IsNullOrEmpty(result?.Data?.Avatar));
            Assert.False(string.IsNullOrEmpty(result?.Data?.Email));
        }

        [Fact]
        public async Task GetUserData_WithValidData2_ResultsOk()
        {
            TestFactoryHelper.AddJwtTokenToRequest(_factory, _client);

            var response = await _client.GetAsync("/api/user/get");

            var result = await response.Content.ReadFromJsonAsync<ApiResponseDto<UserDto>>();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("391a1bf7-c1de-49f1-b14f-0bddc2a02d72", result?.Data?.Id);
            Assert.False(string.IsNullOrEmpty(result?.Data?.UserName));
            Assert.False(string.IsNullOrEmpty(result?.Data?.Avatar));
            Assert.False(string.IsNullOrEmpty(result?.Data?.Email));
        }

        [Fact]
        public async Task Login_WithWrongUsername_ResultsNotFound()
        {
            var response = await _client.GetAsync("/api/user/get/");

            var result = await response.Content.ReadFromJsonAsync<ApiResponseDto<UserDto>>();

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        #endregion
    }
}
