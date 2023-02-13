using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;

namespace IntegrationTests
{
    [TestClass]
    public class IdeasController
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private static WebApplicationFactory<Program> _factory;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        [ClassCleanup]
        public static void ClassCleanup()
        {
            _factory.Dispose();
        }

        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
            _factory = new WebApplicationFactory<Program>();
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