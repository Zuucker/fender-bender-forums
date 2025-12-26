using Application.Dtos.ModelDtos;
using Domain.Models;
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

        #region GetUsersPosts

        [Fact]
        public async Task GetUsersPosts_WithValidData_ResultsOk()
        {
            var exampleSection = new Section() { Name = "Section 1" };

            TestFactoryHelper.AddToDb([
                exampleSection
             ], _factory);


            TestFactoryHelper.AddToDb([
                new ApplicationUser(){ Id = "777a1777-c1de-49f1-b14f-0bddc2a02d75",
                Email = "test2@email.com",
                PasswordHash = "testPassword",
                UserName = "testUser2"},
             ], _factory);

            Post examplePost1 = new()
            {
                AuthorId = "777a1777-c1de-49f1-b14f-0bddc2a02d75",
                CreationDate = DateTime.UtcNow,
                SectionId = exampleSection.SectionId,
                Tags = "Tag1",
                Title = "Example post1",
                Contents = [
                    new Content() {
                        SubTitle = "Subsection 1",
                        Type = 1,
                        TextContent = "Example text",
                        Position = 1
                    },
                    new Content() {
                        SubTitle = "Subsection 2",
                        Type = 2,
                        GalleryElements = [],
                        Position = 2
                    }]
            };

            Post examplePost2 = new()
            {
                AuthorId = "391a1bf7-c1de-49f1-b14f-0bddc2a02d73",
                CreationDate = DateTime.UtcNow,
                SectionId = exampleSection.SectionId,
                Tags = "Tag1",
                Title = "Example post2",
                Contents = [
                    new Content() {
                        SubTitle = "Subsection 1",
                        Type = 1,
                        TextContent = "Example text",
                        Position = 1
                    },
                    new Content() {
                        SubTitle = "Subsection 2",
                        Type = 2,
                        GalleryElements = [],
                        Position = 2
                    }]
            };


            TestFactoryHelper.AddToDb([
                examplePost1,
                examplePost2
            ], _factory);

            var response = await _client.GetAsync("/api/user/777a1777-c1de-49f1-b14f-0bddc2a02d75/posts");

            var result = await response.Content.ReadFromJsonAsync<ApiResponseDto<List<PostDto>>>();
            var retrivedExamplePost = result?.Data?.FirstOrDefault();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(1, result?.Data?.Count);
            Assert.NotNull(retrivedExamplePost);
            Assert.Equal(examplePost1.Title, retrivedExamplePost.Title);
            Assert.Equal(examplePost1.SectionId, retrivedExamplePost.SectionId);
        }


        [Fact]
        public async Task GetUsersPosts_WithInValidUserId_ResultsEmptyList()
        {
            var exampleSection = new Section() { Name = "Section 1" };

            TestFactoryHelper.AddToDb([
                exampleSection
             ], _factory);


            Post examplePost = new()
            {
                AuthorId = "391a1bf7-c1de-49f1-b14f-0bddc2a02d72",
                CreationDate = DateTime.UtcNow,
                SectionId = exampleSection.SectionId,
                Tags = "Tag1",
                Title = "Example post1",
                Contents = [
                    new Content() {
                        SubTitle = "Subsection 1",
                        Type = 1,
                        TextContent = "Example text",
                        Position = 1
                    },
                    new Content() {
                        SubTitle = "Subsection 2",
                        Type = 2,
                        GalleryElements = [],
                        Position = 2
                    }]
            };


            TestFactoryHelper.AddToDb([
                examplePost
            ], _factory);

            var response = await _client.GetAsync("/api/user/391a1bf7-c1de-49f1-b14f-0bddc2a02d77/posts");

            var result = await response.Content.ReadFromJsonAsync<ApiResponseDto<List<PostDto>>>();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Empty(result?.Data!);
        }

        #endregion
    }
}
