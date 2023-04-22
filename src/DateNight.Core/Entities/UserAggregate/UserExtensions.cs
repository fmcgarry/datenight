namespace DateNight.Core.Entities.UserAggregate;

public static class UserExtensions
{
    public static void GenerateNewId(this User user)
    {
        user.Id = Guid.NewGuid().ToString("N");
    }
}