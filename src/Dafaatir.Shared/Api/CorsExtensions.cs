using Dafaatir.Shared.Env;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Dafaatir.Shared.Api;

static public class CorsExtensions
{



    public static IServiceCollection AddCorsPolicy(this IServiceCollection services, IConfiguration configuration)
    {
        //var logger = services.BuildServiceProvider().GetRequiredService<ILogger<IStartup>>();
        var envData = services.BuildServiceProvider().GetRequiredService<EnvData>();

        var allowedOrigins = envData.AllowedOrigins;
        var allowedHeaders = envData.AllowedHeaders;
        var allowedMethods = envData.AllowedMethods;
        var exposedHeaders = envData.ExposedHeaders;
        var allowCredentials = envData.AllowCredentials;





        services.AddCors(cors =>
        {
            cors.AddPolicy("CorsPolicy", corsBuilder =>
               {
                   var origins = allowedOrigins.ToArray();
                   if (allowCredentials && origins.FirstOrDefault() != "*")
                   {

                       corsBuilder.AllowCredentials();
                   }
                   else
                   {
                       corsBuilder.DisallowCredentials();
                   }

                   corsBuilder.WithHeaders(allowedHeaders.ToArray())
                       .WithMethods(allowedMethods.ToArray())
                       .WithOrigins(origins.ToArray())
                       .WithExposedHeaders(exposedHeaders.ToArray());
               });
        });

        return services;
    }



    public static IApplicationBuilder UseCorsPolicy(this IApplicationBuilder app)
    {
        app.UseCors("CorsPolicy");


        return app;
    }
}
