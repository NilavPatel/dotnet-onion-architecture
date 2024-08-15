using Microsoft.Extensions.DependencyInjection;
using MyApp.Application.Interfaces;
using MyApp.Application.Services;

namespace MyApp.Application
{
    public static class ServiceExtensions
    {
        public static void ConfigureApplication(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
        }
    }
}