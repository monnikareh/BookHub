namespace BookHub.Middleware;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;


public class RequestLoggerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggerMiddleware> _logger;

    public RequestLoggerMiddleware(RequestDelegate next, ILogger<RequestLoggerMiddleware> logger)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task Invoke(HttpContext context)
    {
        var logText = $"[{DateTime.Now}] Request: {context.Request.Method} {context.Request.Path}\n";
        _logger.LogInformation($"{logText}");

        var logFilePath = "Logs/logs.txt";
        if (!File.Exists(logFilePath))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(logFilePath));
            await File.Create(logFilePath).DisposeAsync();
        }

        await File.AppendAllTextAsync(logFilePath, logText);
        await _next(context);
    }

}
