using OrcHestrador.UserStories.Interfaces;

namespace OrcHestrador.WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IWorkerEntryPoint _workerEntryPoint;

        public Worker(IWorkerEntryPoint workerEntryPoint,
            ILogger<Worker> logger)
        {
            this._workerEntryPoint = workerEntryPoint;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker Pronto para trabalhar");
            await _workerEntryPoint.exec(stoppingToken);
        }
    }
}