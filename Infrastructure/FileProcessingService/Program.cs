using Application;
using Infrastructure;
using Persistence;

namespace FileProcessingService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "Background service";

            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureService();
            builder.Services.AddPersistenceService(builder.Configuration);

            builder.Services.AddHostedService<Worker>();

            var host = builder.Build();
            host.Run();
        }
    }
}