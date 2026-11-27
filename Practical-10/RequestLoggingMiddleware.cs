using System.Diagnostics;

namespace StudentManagement.API.Middleware;

/// <summary>
/// Custom middleware that demonstrates the ASP.NET Core request pipeline.
/// Every HTTP request is logged on the way IN (method, path) and again on
/// the way OUT (status code, elapsed time) to the application console.
///
/// Middleware concept recap:
///   - Middleware are components assembled into a pipeline that handle
///     requests and responses.
///   - Each middleware can run code BEFORE calling the next one
///     (request side) and AFTER it returns (response side).
///   - If a middleware does not call _next(), the pipeline is short-circuited
///     (e.g. UseHttpsRedirection can short-circuit plain-HTTP requests).
///
/// Registered in Program.cs via the extension method: app.UseRequestLogging();
/// </summary>
public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    // RequestDelegate represents the NEXT middleware in the pipeline.
    // ASP.NET Core injects it plus the ILogger automatically.
    public RequestLoggingMiddleware(RequestDelegate next,
        ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    // InvokeAsync is the entry point called by the framework per request.
    public async Task InvokeAsync(HttpContext context)
    {
        // ---------- BEFORE: request side ----------
        var stopwatch = Stopwatch.StartNew();
        _logger.LogInformation(
            "--> Request: {Method} {Path}{QueryString}",
            context.Request.Method,
            context.Request.Path,
            context.Request.QueryString);

        // Pass control to the next middleware in the pipeline.
        await _next(context);

        // ---------- AFTER: response side ----------
        stopwatch.Stop();
        _logger.LogInformation(
            "<-- Response: {Method} {Path} => {StatusCode} in {Elapsed} ms",
            context.Request.Method,
            context.Request.Path,
            context.Response.StatusCode,
            stopwatch.ElapsedMilliseconds);
    }
}

/// <summary>
/// Extension method that gives Program.cs a clean registration name:
///   app.UseRequestLogging();
/// </summary>
public static class RequestLoggingMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestLoggingMiddleware>();
    }
}
