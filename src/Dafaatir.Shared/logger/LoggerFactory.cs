using Microsoft.Extensions.Logging;
namespace Dafaatir.Shared.Logger;
public class AppLoggerFactory
{
    public static ILoggerFactory LoggerFactory { get; private set; } = Microsoft.Extensions.Logging.LoggerFactory.Create(builder =>
            {
                builder
                    .ClearProviders()
                    .AddConsole()
                    .AddDebug();
            });
}