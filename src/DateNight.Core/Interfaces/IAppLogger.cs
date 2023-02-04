namespace DateNight.Core.Interfaces;

public interface IAppLogger<T>
{
    void LogError(string? message, params object[] args);

    void LogInformation(string? message, params object[] args);

    void LogTrace(string? message, params object[] args);

    void LogWarning(string? message, params object[] args);
}