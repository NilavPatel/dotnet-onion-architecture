using Microsoft.Extensions.DependencyInjection;
using MyApp.Application.Interfaces.Services;
using MyApp.Application.Services;

namespace MyApp.Application.DependencyResolver
{
    public static class DependencyResolverService
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
        }
    }
}