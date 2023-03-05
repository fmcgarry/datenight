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
    public async Task GetAllIdeasAsync_WhenCalled_ReturnsOnlyUserIdeas()
    {
        // Arrange
        var mockedLogger = new Mock<IAppLogger<IdeaService>>();

        var repositoryIdeas = new List<Idea>()
        {
            new Idea()
            {
                Title = "TestTitle",
                Description = "TestDescription",
            },
            new Idea()
            {
                Title = "TestTitle2",
                Description = "TestDescription2",
            }
        };

        var mockedIdeaRepository = new Mock<IRepository<Idea>>();
        mockedIdeaRepository
            .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(repositoryIdeas.AsEnumerable());

        var sut = new IdeaService(mockedLogger.Object, mockedIdeaRepository.Object);

        // Act
        var results = await sut.GetAllIdeasAsync();

        // Assert
        Assert.AreEqual(repositoryIdeas.Count, results.Count());
    }

    [TestMethod]
    public async Task GetAllIdeasAsync_WhenNoIdeasInCollection_ReturnsEmpty()
    {
        // Arrange
        var mockedLogger = new Mock<IAppLogger<IdeaService>>();

        var mockedIdeaRepository = new Mock<IRepository<Idea>>();
        mockedIdeaRepository
            .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(Enumerable.Empty<Idea>());

        var sut = new IdeaService(mockedLogger.Object, mockedIdeaRepository.Object);

        // Act
        var results = await sut.GetAllIdeasAsync();

        // Assert
        Assert.AreEqual(0, results.Count());
    }

    [TestMethod]
    public async Task UpdateIdea_WhenIdeaDoesNotExist_ThrowsArgumentException()
    {
        // Arrange
        var mockedLogger = new Mock<IAppLogger<IdeaService>>();
        var mockedIdeaRepository = new Mock<IRepository<Idea>>();
        var sut = new IdeaService(mockedLogger.Object, mockedIdeaRepository.Object);

        var idea = new Idea();

        await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await sut.UpdateIdeaAsync(idea));
    }

    [TestMethod]
    public async Task UpdateIdea_WhenIdeaIdIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        var mockedLogger = new Mock<IAppLogger<IdeaService>>();
        var mockedIdeaRepository = new Mock<IRepository<Idea>>();
        var sut = new IdeaService(mockedLogger.Object, mockedIdeaRepository.Object);

        var idea = new Idea
        {
            Id = null
        };

        // Act
        Task action() => sut.UpdateIdeaAsync(idea);

        // Assert
        await Assert.ThrowsExceptionAsync<ArgumentNullException>(action);
    }

    [TestMethod]
    public async Task UpdateIdea_WhenValidIdea_ThenRepositoryUpdateIdeaIsCalled()
    {
        // Arrange
        var mockedLogger = new Mock<IAppLogger<IdeaService>>();
        var mockedIdeaRepository = new Mock<IRepository<Idea>>();

        Idea? idea = new();

        mockedIdeaRepository
            .Setup(repository => repository.GetByIdAsync(It.IsAny<string>(), default))
            .ReturnsAsync(idea);

        var sut = new IdeaService(mockedLogger.Object, mockedIdeaRepository.Object);

        // Act
        await sut.UpdateIdeaAsync(idea);

        // Assert
        mockedIdeaRepository.Verify(repository => repository.UpdateAsync(It.Is<Idea>(x => x.Id == idea.Id), default));
    }
}