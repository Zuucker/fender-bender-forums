using Application.Dtos.ModelDtos;
using Presentation.Responses;
using System.Net;
using System.Net.Http.Json;
using Tests.TestUtils;
using Domain.Models;
using Application.Dtos.RequestDtos;

namespace Tests.IntegrationTests
{
    public class PostControllerTests : IClassFixture<ApplicationFactory<Program>>
    {
        private readonly ApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public PostControllerTests(ApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        #region GetAll

        [Fact]
        public async Task GetPosts_WithValidData_ResultsOk()
        {
            TestFactoryHelper.AddJwtTokenToRequest(_factory, _client);


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
                Title = "Example post",
                Contents = [
                    new Content() {
                        ContentId = 1,
                        SubTitle = "Subsection 1",
                        Type = 1,
                        TextContent = "Example text",
                        Position = 1
                    },
                    new Content() {
                        ContentId = 2,
                        SubTitle = "Subsection 2",
                        Type = 2,
                        GalleryElements = [],
                        Position = 2
                    }]
            };


            TestFactoryHelper.AddToDb([
                examplePost
            ], _factory);

            var response = await _client.GetAsync("/api/posts/get");

            var result = await response.Content.ReadFromJsonAsync<ApiResponseDto<List<PostDto>>>();
            var retrivedExamplePost = result?.Data?.FirstOrDefault();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(1, result?.Data?.Count);
            Assert.NotNull(retrivedExamplePost);
            Assert.Equal(examplePost.Title, retrivedExamplePost.Title);
            Assert.Equal(examplePost.SectionId, retrivedExamplePost.SectionId);
            Assert.Equal(examplePost.Tags, retrivedExamplePost.Tags);
            Assert.True(examplePost.CreationDate < DateTime.Now);
            Assert.NotEmpty(examplePost.Contents);
            Assert.Equal(examplePost.Contents.FirstOrDefault()?.SubTitle,
                retrivedExamplePost.Contents.FirstOrDefault()?.SubTitle
                );
            Assert.Equal(examplePost.Contents.FirstOrDefault(c => c.ContentId == 1)?.Position,
                retrivedExamplePost.Contents.FirstOrDefault(c => c.Id == 1)?.Position
                );
            Assert.Equal(examplePost.Contents.FirstOrDefault(c => c.ContentId == 1)?.GalleryElements.Count,
                retrivedExamplePost.Contents.FirstOrDefault(c => c.Id == 1)?.GalleryElements.Count
                );
            Assert.Equal(examplePost.Contents.FirstOrDefault(c => c.ContentId == 1)?.TextContent,
                retrivedExamplePost.Contents.FirstOrDefault(c => c.Id == 1)?.TextContent
                );
            Assert.Equal(examplePost.Contents.FirstOrDefault(c => c.ContentId == 2)?.Position,
                retrivedExamplePost.Contents.FirstOrDefault(c => c.Id == 2)?.Position
                );
            Assert.Equal(examplePost.Contents.FirstOrDefault(c => c.ContentId == 2)?.GalleryElements.Count,
                retrivedExamplePost.Contents.FirstOrDefault(c => c.Id == 2)?.GalleryElements.Count
                );
            Assert.Equal(examplePost.Contents.FirstOrDefault(c => c.ContentId == 2)?.TextContent,
                retrivedExamplePost.Contents.FirstOrDefault(c => c.Id == 2)?.TextContent
                );
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

            var response = await _client.GetAsync("/api/posts/get/777a1777-c1de-49f1-b14f-0bddc2a02d75");

            var result = await response.Content.ReadFromJsonAsync<ApiResponseDto<List<PostDto>>>();
            var retrivedExamplePost = result?.Data?.FirstOrDefault();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(1, result?.Data?.Count);
            Assert.NotNull(retrivedExamplePost);
            Assert.Equal(examplePost1.Title, retrivedExamplePost.Title);
            Assert.Equal(examplePost1.SectionId, retrivedExamplePost.SectionId);
        }

        #endregion


        #region GetUsersPosts

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

            var response = await _client.GetAsync("/api/posts/get/391a1bf7-c1de-49f1-b14f-0bddc2a02d77");

            var result = await response.Content.ReadFromJsonAsync<ApiResponseDto<List<PostDto>>>();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Empty(result?.Data!);
        }

        #endregion


        #region Add

        [Fact]
        public async Task AddPosts_WithValidData_ResultsOk()
        {
            TestFactoryHelper.AddJwtTokenToRequest(_factory, _client);


            var exampleSection = new Section() { Name = "Section 1" };

            TestFactoryHelper.AddToDb([
                exampleSection
             ], _factory);


            AddPostRequest examplePostRequest = new()
            {
                AuthorId = "391a1bf7-c1de-49f1-b14f-0bddc2a02d72",
                SectionId = exampleSection.SectionId,
                Tags = "Tag1",
                Title = "Example post",
                Contents = [
                    new ContentDto() {
                            SubTitle = "Subsection 1",
                        Type = 1,
                        TextContent = "Example text",
                        Position = 1
                    },
                    new ContentDto() {
                            SubTitle = "Subsection 2",
                        Type = 2,
                        GalleryElements = [],
                        Position = 2
                    }]
            };


            var response = await _client.PostAsJsonAsync("/api/posts/add", examplePostRequest);

            var result = await response.Content.ReadFromJsonAsync<ApiResponseDto<PostDto>>();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result?.Data);
            Assert.Equal(examplePostRequest.Title, result?.Data.Title);
            Assert.Equal(examplePostRequest.SectionId, result?.Data.SectionId);
            Assert.Equal(examplePostRequest.Tags, result?.Data.Tags);
            Assert.True(examplePostRequest.CreationDate < DateTime.Now);
            Assert.NotEmpty(examplePostRequest.Contents);
            Assert.Equal(examplePostRequest.Contents.FirstOrDefault()?.SubTitle,
                examplePostRequest.Contents.FirstOrDefault()?.SubTitle
                );
            Assert.Equal(examplePostRequest.Contents.Count,
                examplePostRequest.Contents.Count
                );
        }

        #endregion
    }
}