using Dafaatir.Modules.Users.App;
using Dafaatir.Shared.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Dafaatir.Modules.Users.Api;
public class UsersModule : IModule
{
    public void Register(IServiceCollection services)
    {
        services.AddUsersApp();
    }

    public void Use(IApplicationBuilder app)
    {
        app.UseUsersApp();
    }
}
