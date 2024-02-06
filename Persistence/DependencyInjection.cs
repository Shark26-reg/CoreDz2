using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;


namespace Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddDbContext<StoreContext>(
            options => options
                .UseSqlServer(configuration
                .GetConnectionString("StoreConnection")));
        
        return services;
    }

}
