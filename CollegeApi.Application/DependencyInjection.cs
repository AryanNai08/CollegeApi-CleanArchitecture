using CollegeApi.Application.Interfaces;
using CollegeApi.Application.Mappings;
using CollegeApi.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CollegeApi.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperConfig));
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
