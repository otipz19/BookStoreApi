using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace WebApi
{
    public static class ServiceRegistrationExtensions
    {
        public static IServiceCollection AddAppDbContext(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"),
                    x => x.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName));
            });

            return services;
        }
    }
}
