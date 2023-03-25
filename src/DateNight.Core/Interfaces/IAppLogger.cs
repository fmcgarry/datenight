namespace DateNight.Core.Interfaces;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S2326:Unused type parameters should be removed", Justification = "Used in inherited classes to instantiate an ILogger<T>")]
public interface IAppLogger<T>
{
    void LogDebug(string? message, params object?[] args);

    void LogError(string? message, params object?[] args);

    void LogInformation(string? message, params object?[] args);

    void LogTrace(string? message, params object?[] args);

    void LogWarning(string? message, params object?[] args);
}