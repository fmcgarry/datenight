using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace IntegrationTests;

[TestClass]
public class UsersController
{
    private static WebApplicationFactory<Program> _factory = null!;

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
}