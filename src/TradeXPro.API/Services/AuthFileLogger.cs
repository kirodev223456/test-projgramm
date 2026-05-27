namespace TradeXPro.API.Services;

/// <summary>
/// Writes authentication events to daily rotating text log files.
/// Format: logs/auth_YYYY-MM-DD.log
/// </summary>
public class AuthFileLogger : IAuthFileLogger
{
    private readonly string _logDirectory;

    public AuthFileLogger(IConfiguration configuration)
    {
        _logDirectory = configuration.GetValue<string>("Logging:AuthLogPath") ?? "logs";
        if (!Directory.Exists(_logDirectory))
            Directory.CreateDirectory(_logDirectory);
    }

    public async Task LogAsync(string action, string email, string ipAddress, string result)
    {
        var fileName = $"auth_{DateTime.UtcNow:yyyy-MM-dd}.log";
        var filePath = Path.Combine(_logDirectory, fileName);
        var logEntry = $"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} | {action,-6} | {email,-30} | {ipAddress,-15} | {result}";

        await File.AppendAllTextAsync(filePath, logEntry + Environment.NewLine);
    }
}

public interface IAuthFileLogger
{
    Task LogAsync(string action, string email, string ipAddress, string result);
}
