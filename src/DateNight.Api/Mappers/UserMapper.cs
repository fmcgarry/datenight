using User = DateNight.Core.Entities.UserAggregate.User;
using UserDTO = DateNight.Api.Data.User;

namespace DateNight.Api.Mappers;

public static class UserMapper
{
    public static UserDTO MapToDTO(this User user)
    {
        var userDTO = new UserDTO()
        {
            Id = user.Id.ToString(),
            Name = user.Name
        };

        return userDTO;
    }
}