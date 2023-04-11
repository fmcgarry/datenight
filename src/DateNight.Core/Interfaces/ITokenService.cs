namespace DateNight.Core.Interfaces
{
    public interface ITokenService
    {
        public string GenerateToken(string username);
    }
}