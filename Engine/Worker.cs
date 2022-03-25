
namespace Engine
{
    public sealed class WindowsBackgroundService : BackgroundService
    {
        private readonly HangfireService _service;
        private readonly ILogger<WindowsBackgroundService> _logger;

        public WindowsBackgroundService(
            HangfireService jokeService,
            ILogger<WindowsBackgroundService> logger) =>
            (_service, _logger) = (jokeService, logger);

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _service.Reinstall();

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation($"Hangfire service heart beat {DateTime.Now}");
                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
        }
    }
}