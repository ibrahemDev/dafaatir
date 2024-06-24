
using System.Reflection;
using Microsoft.AspNetCore.Builder;

using Microsoft.Extensions.DependencyInjection;

namespace Dafaatir.Modules.Users.App;
public static class Extensions
{
    public static IServiceCollection AddUsersApp(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
       {
           cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
           //cfg.RegisterServicesFromAssemblies(assemblies);
           cfg.Lifetime = ServiceLifetime.Scoped;
           //

       });

        return services;
    }

    public static IApplicationBuilder UseUsersApp(this IApplicationBuilder app)
    {


        return app;
    }
}