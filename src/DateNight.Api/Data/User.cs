namespace DateNight.Api.Data;

public class User
{
    public required string Password { get; set; }
    public required string Email { get; set; }
    public required string Name { get; set; }
}