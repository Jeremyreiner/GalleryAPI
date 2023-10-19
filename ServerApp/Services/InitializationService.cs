namespace GalleryAPI.Services
{
    public class InitializationService : IHostedService
    {
        readonly ILogger<InitializationService> _Logger;

        public InitializationService(ILogger<InitializationService> logger)
        {
            _Logger = logger;
        }

        public async Task StartAsync(CancellationToken ct)
        {
            _Logger.LogInformation("Initializing application");
            _Logger.LogInformation("Application Initialized");
        }

        public async Task StopAsync(CancellationToken ct)
        {
            _Logger.LogInformation("Application terminated");

        }
    }
}
