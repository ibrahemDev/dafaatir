using System.Text.Json;
using Dafaatir.Shared.Env;
using Microsoft.AspNetCore.Http;


namespace Dafaatir.Shared.Api.Middleware;

/// <summary>
/// Middleware to restrict access to the application based on the request's host domain.
/// </summary>
public class RestrictDomainsMiddleware(RequestDelegate next, EnvData envData)
{
    private readonly RequestDelegate _next = next;
    private readonly EnvData _envData = envData;

    /// <summary>
    /// Invokes the middleware.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    public async Task Invoke(HttpContext context)
    {
        var host = context.Request.Host.Host.ToLower(); // Get the host without the port
        var domains = _envData.RestrictedDomains;
        var path = context.Request.Path.Value?.ToLower() ?? ""; // Get the request path

        int count = domains.Count;



        // TODO: Add logging to log allowed and denied requests.

        if (count > 0 && !domains.Contains(host))
        {
            if (path.StartsWith("/api/"))
            {
                // Return a JSON error response for API requests.
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";
                var errorResponse = new
                {
                    ok = false,
                    message = "Access denied."
                };
                await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
            }
            else
            {
                // Return a plain text error response for non-API requests.
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Access denied.");
            }

            return;
        }
        await _next(context);
    }
}
