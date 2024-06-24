using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Encodings.Web;
using Dafaatir.Shared.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Dafaatir.Shared;
namespace Dafaatir.Bootstrapper;


public class Startup
{

    private readonly IList<Assembly> _assemblies;
    private readonly IList<IModule> _modules;


    private readonly IConfiguration _configuration;


    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;


        _assemblies = ModuleLoader.LoadAssemblies(configuration, "Dafaatir.Modules.");
        _modules = ModuleLoader.LoadModules(_assemblies);

    }

    public void ConfigureServices(IServiceCollection services)
    {
        //services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

        //



        services.AddModularInfrastructure(_configuration, _assemblies, _modules);

        foreach (var module in _modules)
        {
            module.Register(services);
        }
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
    {



        //app.UseUrls($"https://0.0.0.0:{5999}");

        app.UseModularInfrastructure(env);
        foreach (var module in _modules)
        {
            module.Use(app);
        }

        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
       {
           endpoints.MapControllers();
           endpoints.MapGet("/", context => context.Response.WriteAsync("API GG"));

       });

    }
}