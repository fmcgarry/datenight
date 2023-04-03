using DateNight.Core.Interfaces;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace DateNight.Infrastructure.Logging;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2254:Template should be a static expression", Justification = "Formatting does not change structure of log message")]
public class LoggerAdapter<T> : IAppLogger<T>
{
    private readonly ILogger<T> _logger;

    public LoggerAdapter(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<T>();
    }

    public void LogDebug(string? message, params object?[] args)
    {
        string? formattedMessage = FormatMessage(message);
        _logger.LogDebug(formattedMessage, args);
    }

    public void LogError(string? message, params object?[] args)
    {
        string? formattedMessage = FormatMessage(message);
        _logger.LogError(formattedMessage, args);
    }

    public void LogInformation(string? message, params object?[] args)
    {
        string formattedMessage = FormatMessage(message);
        _logger.LogInformation(formattedMessage, args);
    }

    public void LogTrace(string? message, params object?[] args)
    {
        string formattedMessage = FormatMessage(message);
        _logger.LogTrace(formattedMessage, args);
    }

    public void LogWarning(string? message, params object?[] args)
    {
        string formattedMessage = FormatMessage(message);
        _logger.LogWarning(formattedMessage, args);
    }

    private static string FormatMessage(string? message)
    {
        var method = new StackFrame(2)?.GetMethod()?.Name ?? string.Empty;

        return $"[{method}] {message ?? "Message was null"}";
    }
}