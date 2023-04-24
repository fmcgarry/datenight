using DateNight.Core.Entities.IdeaAggregate;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;

namespace IntegrationTests
{
    [TestClass]
    public class IdeasController
    {
        private static WebApplicationFactory<Program> _factory = new WebApplicationFactory<Program>();

        [ClassCleanup]
        public static void ClassCleanup()
        {
            _factory.Dispose();
        }

        [TestMethod]
        public async Task GetIdeas_WhenIdeasFound_ReturnsIdeas()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetFromJsonAsync<IEnumerable<Idea>>("ideas");

            // Assert
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Any());
        }

        [TestMethod]
        public async Task GetIdeas_WhenIdeasFound_ReturnsOk()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("ideas");

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task WhenAddIdeaMissingData_ReturnsBadRequest()
        {
            // Arrange
            var client = _factory.CreateClient();
            var content = JsonContent.Create(new { test = "test" });

            // Act
            var response = await client.PostAsync("ideas", content);

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}