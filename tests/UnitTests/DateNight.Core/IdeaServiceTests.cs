using DateNight.Core.Entities.IdeaAggregate;
using DateNight.Core.Interfaces;
using DateNight.Core.Services;
using Moq;

namespace UnitTests.DateNight.Core;

[TestClass]
public class IdeaServiceTests
{
    [TestMethod]
    public async Task ActivateIdea_WhenIdeaDoesNotExist_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var mockedLogger = new Mock<IAppLogger<IdeaService>>();
        var mockedIdeaRepository = new Mock<IIdeaRepository>();
        var sut = new IdeaService(mockedLogger.Object, mockedIdeaRepository.Object);

        // Act/Assert
        await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(async () => await sut.ActivateIdea(""));
    }

    [TestMethod]
    public async Task ActivateIdea_WhenIdIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        var mockedLogger = new Mock<IAppLogger<IdeaService>>();
        var mockedIdeaRepository = new Mock<IIdeaRepository>();
        var sut = new IdeaService(mockedLogger.Object, mockedIdeaRepository.Object);

        // Act/Assert
        await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await sut.ActivateIdea(null));
    }

    [TestMethod]
    public async Task ActivateIdea_WhenNewIdeaActivated_ThenCurrentActiveIdeaDeactivated()
    {
        // Arrange
        var mockedLogger = new Mock<IAppLogger<IdeaService>>();

        var currentlyActiveIdea = new Idea()
        {
            Id = "1",
            State = IdeaState.Active,
        };

        var newActiveIdea = new Idea()
        {
            Id = "2",
            State = IdeaState.None,
        };

        var repositoryIdeas = new List<Idea>()
        {
            currentlyActiveIdea,
            newActiveIdea
        };

        var mockedIdeaRepository = new Mock<IIdeaRepository>();

        mockedIdeaRepository
            .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(repositoryIdeas.AsEnumerable());

        mockedIdeaRepository
            .Setup(x => x.GetByIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(newActiveIdea);

        mockedIdeaRepository
            .Setup(x => x.UpdateAsync(It.IsAny<Idea>(), It.IsAny<CancellationToken>()))
            .Callback((Idea idea, CancellationToken _) =>
            {
                int index = repositoryIdeas.FindIndex(i => i.Id!.Equals(idea.Id));
                repositoryIdeas[index] = idea;
            });

        var sut = new IdeaService(mockedLogger.Object, mockedIdeaRepository.Object);

        await sut.ActivateIdea(newActiveIdea.Id);

        Assert.AreEqual(IdeaState.None, repositoryIdeas[0].State);
    }

    [TestMethod]
    public async Task AddIdeaAsync_WhenNewIdea_ThenCreatedOnDateIsToday()
    {
        // Arrange
        var mockedLogger = new Mock<IAppLogger<IdeaService>>();
        var mockedIdeaRepository = new Mock<IIdeaRepository>();
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

        var mockedIdeaRepository = new Mock<IIdeaRepository>();
        mockedIdeaRepository
            .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(Enumerable.Empty<Idea>());

        var sut = new IdeaService(mockedLogger.Object, mockedIdeaRepository.Object);

        // Act
        var results = await sut.GetAllUserIdeasAsync("123");

        // Assert
        Assert.AreEqual(0, results.Count());
    }

    [TestMethod]
    public async Task UpdateIdea_WhenIdeaDoesNotExist_ThrowsArgumentException()
    {
        // Arrange
        var mockedLogger = new Mock<IAppLogger<IdeaService>>();
        var mockedIdeaRepository = new Mock<IIdeaRepository>();
        var sut = new IdeaService(mockedLogger.Object, mockedIdeaRepository.Object);

        var idea = new Idea();

        await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await sut.UpdateIdeaAsync(idea));
    }

    [TestMethod]
    public async Task UpdateIdea_WhenIdeaIdIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        var mockedLogger = new Mock<IAppLogger<IdeaService>>();
        var mockedIdeaRepository = new Mock<IIdeaRepository>();
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
        var mockedIdeaRepository = new Mock<IIdeaRepository>();

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