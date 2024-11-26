using Application.Services;
using System.Diagnostics;

namespace FileProcessingService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider _serviceProvider;
        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {

                try
                {
                    string folderPath = ($@"C:\old_workspace\FileProcessing_POC\Files");
                    Stopwatch stopwatch2 = Stopwatch.StartNew();
                    var parallelOptions = new ParallelOptions
                    {
                        MaxDegreeOfParallelism = 4,
                        CancellationToken = stoppingToken
                    };
                    string[] originalFiles = Directory.GetFiles($"{folderPath}\\Original");
                    await Parallel.ForEachAsync(originalFiles, parallelOptions, async (filePath, token) =>
                    {
                        Console.WriteLine(filePath);

                        string processedFilePath = $"{folderPath}\\Processed\\{Path.GetFileName(filePath)}";

                        using (var scope = _serviceProvider.CreateScope())
                        {
                            var inventoryItemService = scope.ServiceProvider.GetRequiredService<IInventoryItemService>();
                            await inventoryItemService.SaveInventoryIntoDatabase(filePath, processedFilePath, string.Empty);
                        }
                        Console.WriteLine("===========================");
                    });

                    stopwatch2.Stop();
                    Console.WriteLine($"Elapsed Time Async with Parallel: {stopwatch2.ElapsedMilliseconds} ms");
                }
                catch (Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }

                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }

                await Task.Delay(10000, stoppingToken);
            }
        }

        // Optionally override StopAsync to handle service cleanup when the application shuts down
        public override Task StopAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Background service is stopping...");
            return base.StopAsync(stoppingToken);
        }
    }
}
