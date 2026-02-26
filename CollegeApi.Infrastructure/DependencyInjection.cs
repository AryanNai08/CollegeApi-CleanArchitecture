using CollegeApi.Application.Interfaces;
using CollegeApi.Infrastructure.Data;
using CollegeApi.Infrastructure.Logging;
using CollegeApi.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CollegeApi.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // DbContext
            services.AddDbContext<CollegeDBContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("CollegeAppDBConnection"));
            });

            // Repositories
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped(typeof(ICollegeRepository<>), typeof(CollegeRepository<>));

            // Logging
            services.AddScoped<IMyLogger, LogToDB>();

            return services;
        }
    }
}
