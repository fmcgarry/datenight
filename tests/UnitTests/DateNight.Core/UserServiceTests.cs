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
    public async Task DoSomething()
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