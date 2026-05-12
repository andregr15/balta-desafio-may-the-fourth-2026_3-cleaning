using MaintenanceManager.Core.Services.Abstractions;

namespace MaintenanceManager.Api.Workers;

public class SendTasksWorker(
    ILogger<SendTasksWorker> logger,
    IServiceScopeFactory serviceScopeFactory
) : BackgroundService
{
    private readonly TimeSpan _scheduleTime = new(24, 0, 0); // Schedule to run once a day

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("SendTasksWorker started at: {time}", DateTimeOffset.Now);

        while (!stoppingToken.IsCancellationRequested)
        {
            var now = DateTime.Now;
            var nextRun = now.Date.AddDays(1).AddHours(0); // Schedule for the next day at 00:00

            // delay = nextRun - now;
            var delay = TimeSpan.FromSeconds(5); // Default delay if next run time is in the past

            logger.LogInformation("Current time: {now}, Next run time: {nextRun}", now, nextRun);

            try
            {
                await Task.Delay(delay, stoppingToken);
                logger.LogInformation("SendTasksWorker executing at: {time}", DateTimeOffset.Now);
                await DoWorkAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while sending tasks");
            }

            Task.Delay(_scheduleTime, stoppingToken).Wait(stoppingToken);
        }
    }

    private async Task DoWorkAsync(CancellationToken cancellationToken)
    {
        try
        {
            using var scope = serviceScopeFactory.CreateScope();
            var maintenanceManagerService = scope
                .ServiceProvider
                .GetRequiredService<IMaintenanceManagerService>();

            await maintenanceManagerService.SendTasksAsync(cancellationToken);
            logger.LogInformation("Tasks sent successfully at: {time}", DateTimeOffset.Now);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while sending tasks");
        }
    }
}
