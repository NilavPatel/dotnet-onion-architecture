using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyApp.Application.Core.Services;
using MyApp.Domain.Core.Repositories;
using MyApp.Infrastructure.Data;
using MyApp.Infrastructure.Repositories;
using MyApp.Infrastructure.Services;

namespace MyApp.Infrastructure
{
    public static class ServiceExtensions
    {
        public static void ConfigureInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<MyAppDbContext>(options =>
                options.UseSqlServer("name=ConnectionStrings:MyAppDatabase",
                x => x.MigrationsAssembly("MyApp.Infrastructure")));

            services.AddScoped(typeof(IBaseRepositoryAsync<>), typeof(BaseRepositoryAsync<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ILoggerService, LoggerService>();
        }

        public static void MigrateDatabase(this IServiceProvider serviceProvider)
        {
            var dbContextOptions = serviceProvider.GetRequiredService<DbContextOptions<MyAppDbContext>>();

            using (var dbContext = new MyAppDbContext(dbContextOptions))
            {
                dbContext.Database.Migrate();
            }
        }
    }
}