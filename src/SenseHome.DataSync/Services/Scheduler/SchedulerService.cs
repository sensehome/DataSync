using System;
using System.Threading;
using System.Threading.Tasks;
using SenseHome.DataSync.Services.DbSync;

namespace SenseHome.DataSync.Services.Scheduler
{
    public class SchedulerService : ISchedulerService
    {
        private Timer timer;
        private readonly IDbSyncService dbSyncService;

        public SchedulerService(IDbSyncService dbSyncService)
        {
            this.dbSyncService = dbSyncService;
        }

        public void Dispose()
        {
            timer?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(
                async (object state) =>
                {
                    await dbSyncService.Execute(state);
                },
                null,
                TimeSpan.FromSeconds(5), //will start after 5sec
                TimeSpan.FromMinutes(10) //looping every 10min
                );

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }


    }
}
