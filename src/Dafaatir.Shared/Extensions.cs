using System.Reflection;
using Dafaatir.Shared.Api;
using Dafaatir.Shared.Api.Middleware;
using Dafaatir.Shared.Database;

using Dafaatir.Shared.Env;
using Dafaatir.Shared.MediatR;
using Dafaatir.Shared.Modules;

using Dafaatir.Shared.Serialization;
using Dafaatir.Shared.Storage;
using Dafaatir.Shared.Time;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Dafaatir.Shared;


public static class Extensions
{

    public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : new()
        => configuration.GetSection(sectionName).GetOptions<T>();

    public static T GetOptions<T>(this IConfigurationSection section) where T : new()
    {
        var options = new T();
        section.Bind(options);
        return options;
    }


    public static IServiceCollection AddModularInfrastructure(this IServiceCollection services,
        IConfiguration configuration, IList<Assembly> assemblies, IList<IModule> modules)
    {

        services.AddEnvData();


        var disabledModules = new List<string>();
        foreach (var (key, value) in configuration.AsEnumerable())
        {
            if (!key.Contains(":module:enabled"))
            {
                continue;
            }

            if (!bool.Parse(value ?? "true"))
            {
                disabledModules.Add(key.Split(":")[0]);
            }
        }
        services.AddCorsPolicy(configuration);


        // dev mode need //////////////////////
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(swagger =>
        {
            swagger.EnableAnnotations();
            swagger.CustomSchemaIds(x => x.FullName);
            swagger.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Modular API",
                Version = "v1"
            });
        });
        ////////////////////////

        services.AddMemoryCache();
        services.AddHttpClient();
        services.AddSingleton<IRequestStorage, RequestStorage>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddSingleton<IJsonSerializer, SystemTextJsonSerializer>();
        services.AddSingleton<IClock, UtcClock>();
        services.AddDatabase();
        //services.AddCustomMediatR([.. assemblies]);


        //services.AddAuth(configuration, modules);
        //services.AddErrorHandling();
        //services.AddContext();
        //services.AddSecurity(configuration);






        services.AddControllers()
            .ConfigureApplicationPartManager(manager =>
            {
                var removedParts = new List<ApplicationPart>();
                foreach (var disabledModule in disabledModules)
                {
                    var parts = manager.ApplicationParts.Where(x => x.Name.Contains(disabledModule,
                        StringComparison.InvariantCultureIgnoreCase));
                    removedParts.AddRange(parts);
                }

                foreach (var part in removedParts)
                {
                    manager.ApplicationParts.Remove(part);
                }

                manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
            });

        return services;

    }


    public static IApplicationBuilder UseModularInfrastructure(this IApplicationBuilder app, IWebHostEnvironment env)
    {

        app.UseMiddleware<RestrictDomainsMiddleware>();
        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.All
        });

        app.UseCorsPolicy();
        app.UseRequestId();

        //app.UseErrorHandling();

        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseReDoc(reDoc =>
            {
                reDoc.RoutePrefix = "docs";
                reDoc.SpecUrl("/swagger/v1/swagger.json");
                reDoc.DocumentTitle = "Modular API";
            });
        }



        //app.UseAuth();
        //app.UseContext();
        //app.UseLogging();
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();

        return app;
    }


    public static IApplicationBuilder UseRequestId(this IApplicationBuilder app)
        => app.Use((ctx, next) =>
        {
            ctx.Items.Add("RequestId", Guid.NewGuid());
            return next();
        });


}