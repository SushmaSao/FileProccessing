using Application.Contracts;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DBContextWrite>(options =>
              options.UseNpgsql(configuration.GetConnectionString("FileProcessingServiceConnectionString")).EnableSensitiveDataLogging(false));

            services.AddScoped(typeof(IAsyncCommandRepository<InventoryItem>), typeof(AsyncCommandRepository<InventoryItem, DBContextWrite>));

            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork<DBContextWrite>));

            return services;
        }
    }
}
