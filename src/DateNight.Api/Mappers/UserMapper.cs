using User = DateNight.Core.Entities.UserAggregate.User;
using UserDTO = DateNight.Api.Data.User;

namespace DateNight.Api.Mappers;

public static class UserMapper
{
    public static UserDTO MapToDTO(this User user)
    {
        var userDTO = new UserDTO()
        {
            UserName = user.Name,
            Password = string.Empty
        };

        return userDTO;
    }
}