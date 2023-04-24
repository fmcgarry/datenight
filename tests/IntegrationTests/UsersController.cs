using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Http.Json;
using static DateNight.Api.Data.UserActions;

namespace IntegrationTests;

[TestClass]
public class UsersController
{
    private static WebApplicationFactory<Program> _factory = null!;
    private static IConfiguration config;

    public UsersController()
    {
        _factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        {
            var projectDir = Directory.GetCurrentDirectory();
            var configPath = Path.Combine(projectDir, "appsettings.json");

            builder.ConfigureAppConfiguration((context, configuration) =>
            {
                configuration.AddJsonFile(configPath);
                config = configuration.Build();
            });
        });
    }

    [ClassCleanup]
    public static void ClassCleanup()
    {
        _factory.Dispose();
    }

    [TestMethod]
    [DataRow("123")]
    [DataRow("word")]
    public async Task GetUser_WhenInvalidId_ReturnsBadRequest(string id)
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync($@"users\{id}");

        // Assert
        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [TestMethod]
    [DataRow("")]
    [DataRow(" ")]
    public async Task GetUser_WhenUserDoesNotExist_ReturnsNotFound(string id)
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync($@"users\{id}");

        // Assert
        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [TestMethod]
    public async Task WhenDuplicateCreateUserSubmitted_ReturnsBadRequest()
    {
        // Arrange
        var client = _factory.CreateClient();
        var content = new UserRegisterRequest("test", "test@test.com", "randompassword");

        // Act
        var response = await client.PostAsJsonAsync(@"users\register", content);

        // Assert
        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }
}