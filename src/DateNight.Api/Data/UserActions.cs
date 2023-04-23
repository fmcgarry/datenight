namespace DateNight.Api.Data;

public class UserActions
{
    public record UserLoginRequest(string Email, string Password);
    public record UserLoginResponse(string Token);
    public record UserRegisterRequest(string Name, string Email, string Password);
    public record UserRegisterResponse(string Name, string Email);
    public record GetUserResponse(string Id, string Name, string Email);
}