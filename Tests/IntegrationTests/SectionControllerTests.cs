using Tests.TestUtils;
using System.Net.Http.Json;
using System.Net;
using Presentation.Responses;
using Domain.Models;

namespace Tests.IntegrationTests
{
    public class SectionControllerTests : IClassFixture<ApplicationFactory<Program>>
    {
        private readonly ApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public SectionControllerTests(ApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        #region GetSections

        [Fact]
        public async Task GetSections_WithValidData_ResultsOk()
        {
            TestFactoryHelper.AddToDb([
                    new Section(){ Name = "Section 1" },
                    new Section(){ Name = "Section 2" }
                ], _factory);

            var response = await _client.GetAsync("/api/section/get-list");

            var result = await response.Content.ReadFromJsonAsync<ApiResponseDto<List<Section>>>();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotEmpty(result?.Data ?? []);
        }

        #endregion
    }
}
