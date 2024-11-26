using Application.Command.TransferFileIntoModel;
using Application.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services)
        {
            services.AddSingleton<IFileService, FileService>();
            services.AddSingleton<IFileMapper<InventoryItemDTO>, InventoryItemFileMapper>();

            return services;
        }
    }
}
