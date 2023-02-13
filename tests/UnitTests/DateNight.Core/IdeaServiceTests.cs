using DateNight.Core.Entities.IdeaAggregate;
using DateNight.Core.Interfaces;
using DateNight.Core.Services;
using Moq;

namespace UnitTests.DateNight.Core;

[TestClass]
public class IdeaServiceTests
{
    [TestMethod]
    public async Task AddIdeaAsync_WhenNewIdea_ThenCreatedOnDateIsToday()
    {
        // Arrange
        var mockedLogger = new Mock<IAppLogger<IdeaService>>();
        var mockedIdeaRepository = new Mock<IRepository<Idea>>();
        var sut = new IdeaService(mockedLogger.Object, mockedIdeaRepository.Object);

        var idea = new Idea()
        {
            Title = "Test Title",
            Description = "Test Description",
        };

        // Act
        await sut.AddIdeaAsync(idea);

        // Assert
        Assert.AreEqual(DateTime.UtcNow.Date, idea.CreatedOn.Date);
    }

    [TestMethod]
    public async Task GetAllIdeasAsync_WhenNoIdeasInCollection_ReturnsEmpty()
    {
        // Arrange
        var mockedLogger = new Mock<IAppLogger<IdeaService>>();

        var mockedIdeaRepository = new Mock<IRepository<Idea>>();
        mockedIdeaRepository.Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(Enumerable.Empty<Idea>()));

        var sut = new IdeaService(mockedLogger.Object, mockedIdeaRepository.Object);

        // Act
        var results = await sut.GetAllIdeasAsync();

        // Assert
        Assert.AreEqual(0, results.Count());
    }
}