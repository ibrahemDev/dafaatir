using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;

namespace Dafaatir.Shared.Modules;

public interface IModule
{
    void Register(IServiceCollection services);
    void Use(IApplicationBuilder app);

}