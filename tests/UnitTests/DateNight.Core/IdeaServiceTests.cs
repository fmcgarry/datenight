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
        var mockedUserService = new Mock<IUserService>();
        var sut = new IdeaService(mockedLogger.Object, mockedIdeaRepository.Object, mockedUserService.Object);

        // Act/Assert
        await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(async () => await sut.ActivateIdea(""));
    }

    [TestMethod]
    public async Task ActivateIdea_WhenIdIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        var mockedLogger = new Mock<IAppLogger<IdeaService>>();
        var mockedIdeaRepository = new Mock<IIdeaRepository>();
        var mockedUserService = new Mock<IUserService>();
        var sut = new IdeaService(mockedLogger.Object, mockedIdeaRepository.Object, mockedUserService.Object);

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
            .Setup(x => x.GetAllUserIdeasAsync(It.IsAny<string>()))
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

        var mockedUserService = new Mock<IUserService>();
        var sut = new IdeaService(mockedLogger.Object, mockedIdeaRepository.Object, mockedUserService.Object);

        await sut.ActivateIdea(newActiveIdea.Id);

        Assert.AreEqual(IdeaState.None, repositoryIdeas[0].State);
        Assert.AreEqual(IdeaState.Active, repositoryIdeas[1].State);
    }

    [TestMethod]
    public async Task AddIdeaAsync_WhenDuplicateIdeaID_ThrowsArgumentException()
    {
        // Arrange
        var id = "123";

        var idea = new Idea()
        {
            Id = id,
            Title = "Test Title",
            Description = "Test Description",
        };

        var repositoryIdeas = new List<Idea>()
        {
            new Idea() { Id = id }
        };

        var mockedIdeaRepository = new Mock<IIdeaRepository>();

        mockedIdeaRepository
            .Setup(x => x.GetByIdAsync(idea.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(repositoryIdeas[0]);

        var mockedLogger = new Mock<IAppLogger<IdeaService>>();
        var mockedUserService = new Mock<IUserService>();

        var sut = new IdeaService(mockedLogger.Object, mockedIdeaRepository.Object, mockedUserService.Object);

        // Assert
        await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await sut.AddIdeaAsync(idea));
    }

    [TestMethod]
    public async Task AddIdeaAsync_WhenNewIdea_ThenCreatedOnDateIsToday()
    {
        // Arrange
        var mockedLogger = new Mock<IAppLogger<IdeaService>>();
        var mockedIdeaRepository = new Mock<IIdeaRepository>();
        var mockedUserService = new Mock<IUserService>();
        var sut = new IdeaService(mockedLogger.Object, mockedIdeaRepository.Object, mockedUserService.Object);

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
    public async Task GetAllIdeasAsync_WhenIncludePartnerIdeasIsFalse_ReturnsUserIdeas()
    {
        bool includePartnerIdeas = false;
        int expectedCount = 1;
        await GetAllIdeasAsync_WhenIncludePartnerIdeasIsSet_ReturnsExpectedCount(includePartnerIdeas, expectedCount);
    }

    [TestMethod]
    public async Task GetAllIdeasAsync_WhenIncludePartnerIdeasIsTrue_ReturnsUserAndPartnerIdeas()
    {
        bool includePartnerIdeas = true;
        int expectedCount = 4;
        await GetAllIdeasAsync_WhenIncludePartnerIdeasIsSet_ReturnsExpectedCount(includePartnerIdeas, expectedCount);
    }

    [TestMethod]
    public async Task GetAllIdeasAsync_WhenNoIdeasInCollection_ReturnsEmpty()
    {
        // Arrange
        string userId = "123";

        var mockedLogger = new Mock<IAppLogger<IdeaService>>();

        var mockedIdeaRepository = new Mock<IIdeaRepository>();
        mockedIdeaRepository
            .Setup(x => x.GetAllUserIdeasAsync(userId))
            .ReturnsAsync(Enumerable.Empty<Idea>());

        var mockedUserService = new Mock<IUserService>();
        var sut = new IdeaService(mockedLogger.Object, mockedIdeaRepository.Object, mockedUserService.Object);

        // Act
        var results = await sut.GetAllUserIdeasAsync(userId, false);

        // Assert
        Assert.AreEqual(0, results.Count());
    }

    [TestMethod]
    public async Task GetTopIdeas_WhenDuplicateIdeaTitles_ThenOldestIsReturned()
    {
        var mockedLogger = new Mock<IAppLogger<IdeaService>>();
        string id = "This one";

        var repositoryIdeas = new List<Idea>()
        {
            new Idea() { Id = id, Title = "Go to the Movies", CreatedOn = DateTime.MinValue },
            new Idea() { Id = "Not this one", Title = "Go to the Movies", CreatedOn = DateTime.Now },
        };

        var mockedIdeaRepository = new Mock<IIdeaRepository>();

        mockedIdeaRepository
            .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(repositoryIdeas.AsEnumerable());

        var mockedUserService = new Mock<IUserService>();
        var sut = new IdeaService(mockedLogger.Object, mockedIdeaRepository.Object, mockedUserService.Object);

        var ideas = (await sut.GetTopIdeas(0, repositoryIdeas.Count)).ToList();

        Assert.AreEqual(id, ideas[0].Id);
    }

    [TestMethod]
    public async Task GetTopIdeas_WhenDuplicateIdeaTitlesWithDifferentCasing_ThenOldestIsReturned()
    {
        var mockedLogger = new Mock<IAppLogger<IdeaService>>();
        string id = "This one";

        var repositoryIdeas = new List<Idea>()
        {
            new Idea() { Id = id, Title = "Go to the Movies", CreatedOn = DateTime.MinValue },
            new Idea() { Id = "Not this one", Title = "go to the movies", CreatedOn = DateTime.Now },
        };

        var mockedIdeaRepository = new Mock<IIdeaRepository>();

        mockedIdeaRepository
            .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(repositoryIdeas.AsEnumerable());

        var mockedUserService = new Mock<IUserService>();
        var sut = new IdeaService(mockedLogger.Object, mockedIdeaRepository.Object, mockedUserService.Object);

        var ideas = (await sut.GetTopIdeas(0, repositoryIdeas.Count)).ToList();

        Assert.AreEqual(id, ideas[0].Id);
    }

    [TestMethod]
    public async Task GetTopIdeas_WhenStartIsGreaterThanEnd_ThrowsArgumentOutOfRangeException()
    {
        int start = 2;
        int end = 1;

        var mockedLogger = new Mock<IAppLogger<IdeaService>>();
        var mockedIdeaRepository = new Mock<IIdeaRepository>();
        var mockedUserService = new Mock<IUserService>();
        var sut = new IdeaService(mockedLogger.Object, mockedIdeaRepository.Object, mockedUserService.Object);

        await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(async () => await sut.GetTopIdeas(start, end));
    }

    [TestMethod]
    public async Task UpdateIdea_WhenIdeaDoesNotExist_ThrowsArgumentException()
    {
        // Arrange
        var mockedLogger = new Mock<IAppLogger<IdeaService>>();
        var mockedIdeaRepository = new Mock<IIdeaRepository>();
        var mockedUserService = new Mock<IUserService>();
        var sut = new IdeaService(mockedLogger.Object, mockedIdeaRepository.Object, mockedUserService.Object);

        var idea = new Idea();

        await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await sut.UpdateIdeaAsync(idea));
    }

    [TestMethod]
    public async Task UpdateIdea_WhenIdeaIdIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        var mockedLogger = new Mock<IAppLogger<IdeaService>>();
        var mockedIdeaRepository = new Mock<IIdeaRepository>();
        var mockedUserService = new Mock<IUserService>();
        var sut = new IdeaService(mockedLogger.Object, mockedIdeaRepository.Object, mockedUserService.Object);

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
        Idea? idea = new();

        var mockedLogger = new Mock<IAppLogger<IdeaService>>();
        var mockedIdeaRepository = new Mock<IIdeaRepository>();

        mockedIdeaRepository
            .Setup(repository => repository.GetByIdAsync(It.IsAny<string>(), default))
            .ReturnsAsync(idea);

        var mockedUserService = new Mock<IUserService>();
        var sut = new IdeaService(mockedLogger.Object, mockedIdeaRepository.Object, mockedUserService.Object);

        // Act
        await sut.UpdateIdeaAsync(idea);

        // Assert
        mockedIdeaRepository.Verify(repository => repository.UpdateAsync(It.Is<Idea>(x => x.Id == idea.Id), default));
    }

    private async Task GetAllIdeasAsync_WhenIncludePartnerIdeasIsSet_ReturnsExpectedCount(bool isIncludePartnerIdeasSet, int expectedCount)
    {
        // Arrange
        string userId = "user";
        string partnerOneId = "partnerOne";
        string partnerTwoId = "partnerTwo";

        var ideas = new List<Idea>()
        {
            new Idea() { CreatedBy = userId },
            new Idea() { CreatedBy = partnerOneId },
            new Idea() { CreatedBy = partnerOneId },
            new Idea() { CreatedBy = partnerTwoId },
            new Idea() { CreatedBy = "random1" },
            new Idea() { CreatedBy = "random2" },
        };

        var mockedLogger = new Mock<IAppLogger<IdeaService>>();

        var mockedIdeaRepository = new Mock<IIdeaRepository>();
        mockedIdeaRepository
            .Setup(x => x.GetAllUserIdeasAsync(userId))
            .ReturnsAsync(ideas.Where(x => x.CreatedBy.Equals(userId)));
        mockedIdeaRepository
            .Setup(x => x.GetAllUserIdeasAsync(partnerOneId))
            .ReturnsAsync(ideas.Where(x => x.CreatedBy.Equals(partnerOneId)));
        mockedIdeaRepository
            .Setup(x => x.GetAllUserIdeasAsync(partnerTwoId))
            .ReturnsAsync(ideas.Where(x => x.CreatedBy.Equals(partnerTwoId)));

        var mockedUserService = new Mock<IUserService>();
        mockedUserService
            .Setup(x => x.GetUserPartners(userId))
            .ReturnsAsync(new List<string>() { partnerOneId, partnerTwoId });

        var sut = new IdeaService(mockedLogger.Object, mockedIdeaRepository.Object, mockedUserService.Object);

        // Act
        var results = await sut.GetAllUserIdeasAsync(userId, isIncludePartnerIdeasSet);

        // Assert
        Assert.AreEqual(expectedCount, results.Count());
    }
}