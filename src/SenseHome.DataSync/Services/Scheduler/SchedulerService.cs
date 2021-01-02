using System;
using System.Threading;
using System.Threading.Tasks;
using SenseHome.DataSync.Configurations.Models;
using SenseHome.DataSync.Services.DbSync;

namespace SenseHome.DataSync.Services.Scheduler
{
    public class SchedulerService : ISchedulerService
    {
        private Timer timer;
        private readonly IDbSyncService dbSyncService;
        private readonly SyncSchedulerSettings syncSchedulerSettings;

        public SchedulerService(IDbSyncService dbSyncService, DataSyncSettings dataSyncSettings)
        {
            this.dbSyncService = dbSyncService;
            syncSchedulerSettings = dataSyncSettings.SyncSchedulerSettings;
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
                TimeSpan.FromSeconds(syncSchedulerSettings.StartServiceInSec), //will start after X sec
                TimeSpan.FromMinutes(syncSchedulerSettings.LoopServiceInMin) //looping every X min
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
