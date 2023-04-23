using DateNight.Core.Entities.UserAggregate;
using DateNight.Core.Exceptions;
using DateNight.Core.Interfaces;
using DateNight.Core.Services;
using Moq;

namespace UnitTests.DateNight.Core;

[TestClass]
public class UserServiceTests
{
    [TestMethod]
    public async Task AddUserPartnerAsync_WhenMultiplePartnersWithPartnerId_ThrowsArgumentException()
    {
        string userId = "user";
        string partnerId = "partner";
        string similarPartnerId = "partnerSimilar";

        var users = new List<User>()
        {
            new User() { Id = userId },
            new User() { Id = partnerId },
            new User() { Id = similarPartnerId },
        };

        var mockedLogger = new Mock<IAppLogger<UserService>>();
        var mockedUserRepository = new Mock<IUserRepository>();
        var mockedTokenService = new Mock<ITokenService>();

        mockedUserRepository
            .Setup(x => x.GetByIdAsync(userId, default))
            .ReturnsAsync(users.First(user => user.Id.Equals(userId)));

        mockedUserRepository
            .Setup(x => x.GetUsersByPartialIdAsync(partnerId))
            .ReturnsAsync(users.Where(user => user.Id.StartsWith(partnerId)));

        var sut = new UserService(mockedLogger.Object, mockedUserRepository.Object, mockedTokenService.Object);

        await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await sut.AddUserPartnerAsync(userId, partnerId));
    }

    [TestMethod]
    public async Task AddUserPartnerAsync_WhenUserPartnerAdded_ThenUserIsAddedAsPartnersPartner()
    {
        var user = new User() { Id = "user" };
        var partner = new User() { Id = "partner" };

        var users = new List<User>()
        {
            user,
            partner,
        };

        var mockedLogger = new Mock<IAppLogger<UserService>>();
        var mockedUserRepository = new Mock<IUserRepository>();
        var mockedTokenService = new Mock<ITokenService>();

        mockedUserRepository
            .Setup(x => x.GetByIdAsync(user.Id, default))
            .ReturnsAsync(user);

        mockedUserRepository
            .Setup(x => x.GetUsersByPartialIdAsync(partner.Id))
            .ReturnsAsync(users.Where(user => user.Id.StartsWith(partner.Id)));

        var sut = new UserService(mockedLogger.Object, mockedUserRepository.Object, mockedTokenService.Object);

        await sut.AddUserPartnerAsync(user.Id, partner.Id);

        Assert.AreEqual(1, user.Partners.Count);
        Assert.AreEqual(1, partner.Partners.Count);
        Assert.AreEqual(user.Id, partner.Partners[0]);
        Assert.AreEqual(partner.Id, user.Partners[0]);
    }

    [TestMethod]
    public async Task CreateUserAsync_WhenUserIdGenerationFails_ThrowsUserCreationFailedException()
    {
        // Arrange
        string name = "foo";
        string email = "foo@test.com";
        string passwordText = "bar12345";

        var notEmptyUserArray = new List<User>()
        {
            new User()
        };

        var mockedLogger = new Mock<IAppLogger<UserService>>();
        var mockedUserRepository = new Mock<IUserRepository>();
        var mockedTokenService = new Mock<ITokenService>();

        mockedUserRepository
            .Setup(x => x.GetUsersByPartialIdAsync(It.IsAny<string>()))
            .ReturnsAsync(() => notEmptyUserArray.AsEnumerable());

        var sut = new UserService(mockedLogger.Object, mockedUserRepository.Object, mockedTokenService.Object);

        // Act/Assert
        await Assert.ThrowsExceptionAsync<UserCreationFailedException>(async () => await sut.CreateUserAsync(name, email, passwordText));
    }
}