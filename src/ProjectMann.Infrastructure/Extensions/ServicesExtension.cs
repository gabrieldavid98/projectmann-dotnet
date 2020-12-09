using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProjectMann.Infrastructure.Data;
using ProjectMann.Core.Domain;

namespace ProjectMann.Infrastructure.Extensions
{
    public static class ServicesExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, string connSring)
        {
            services.AddDbContext<ProjectMannDbContext>(options =>
                options.UseNpgsql(connSring)
            );
        }
            
    }
}