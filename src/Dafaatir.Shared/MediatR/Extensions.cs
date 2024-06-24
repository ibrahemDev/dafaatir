using System.Reflection;

using MediatR;
using Microsoft.Extensions.DependencyInjection;


namespace Dafaatir.Shared.MediatR;
public static class Extensions
{
    public static IServiceCollection AddCustomMediatR(this IServiceCollection services, params Assembly[] assemblies)
    {
        //services.AddMediatR(cfg => { cfg.RegisterServicesFromAssemblies(assemblies); });
        Console.WriteLine($"--->{assemblies.Length}<---");
        services.AddMediatR(cfg =>
       {
           cfg.RegisterServicesFromAssemblies(assemblies);
           cfg.Lifetime = ServiceLifetime.Scoped;
           //cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

       });
        //services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        // services.AddMediatR(assemblies);
        //services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        //services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        //services.AddScoped(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));

        return services;
    }
}