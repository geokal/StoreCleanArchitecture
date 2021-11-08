using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Store.ApplicationCore
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationsCore(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
        
    }
}