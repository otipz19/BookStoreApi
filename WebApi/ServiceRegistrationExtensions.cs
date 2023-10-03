using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Service.Validators;
using WebApi.Mapping;

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

        public static IServiceCollection AddMappings(this IServiceCollection services)
        {
            return services.AddAutoMapper(typeof(MappingProfile));
        }

        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            return services.AddValidatorsFromAssemblyContaining<AuthorDtoValidator>();
        }
    }
}
