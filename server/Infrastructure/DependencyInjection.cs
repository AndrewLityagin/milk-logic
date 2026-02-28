using Domain;
using Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Interfaces;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = Environment.GetEnvironmentVariable("DefaultConnection");
        if (string.IsNullOrEmpty(connectionString)) {
            connectionString = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(connectionString)) {
                throw new ArgumentNullException("Connection string 'DefaultConnection' is missing");
            }
        }
        
        services.AddScoped<ISensorDataRepository, SensorDataRepository>();
        services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(connectionString);    
            }
        );

        return services;
    }
}