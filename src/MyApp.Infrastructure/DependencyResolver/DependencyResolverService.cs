using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyApp.Domain.Core.Repositories;
using MyApp.Domain.Core.Services;
using MyApp.Infrastructure.Data;
using MyApp.Infrastructure.Repositories;
using MyApp.Infrastructure.Services;

namespace MyApp.Infrastructure.DependencyResolver
{
    public static class DependencyResolverService
    {
        public static void Register(IServiceCollection services, IConfiguration Configuration)
        {
            string connString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<MyAppDbContext>(options => options.UseSqlServer(connString));

            services.AddScoped(typeof(IBaseRepositoryAsync<>), typeof(BaseRepositoryAsync<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ILoggerService, LoggerService>();
        }
    }
}