using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MQTTnet.Client.Options;
using MQTTnet.Extensions.ManagedClient;
using SenseHome.DataSync.Configurations.Models;
using SenseHome.DataSync.Options;
using SenseHome.DataSync.Services.MqttClient;

namespace SenseHome.DataSync.Configurations
{
    public static class HostedMqttClientConfiguration
    {
        public static IServiceCollection AddMqttClientHostedService(this IServiceCollection services, IConfiguration configuration)
        {
            var dataSyncSettings = new DataSyncSettings();
            configuration.GetSection(nameof(DataSyncSettings)).Bind(dataSyncSettings);
            services.AddSingleton(dataSyncSettings);

            services.AddMqttClientServiceWithConfig(aspOptionBuilder =>
            {
                var clientSettinigs = dataSyncSettings.ClientSettings;
                var brokerSettings = dataSyncSettings.BrokerSettings;

                aspOptionBuilder
                .WithAutoReconnectDelay(TimeSpan.FromSeconds(clientSettinigs.AutoReconnectInSec))
                .WithClientOptions(new MqttClientOptionsBuilder()
                    .WithClientId(clientSettinigs.Id)
                    .WithCredentials(clientSettinigs.UserName, clientSettinigs.Password)
                    .WithTcpServer(brokerSettings.Host, brokerSettings.Port)
                    .Build());
            });
            return services;
        }

        private static IServiceCollection AddMqttClientServiceWithConfig(this IServiceCollection services, Action<AspCoreManagedMqttClientOptionBuilder> configuration)
        {
            services.AddSingleton<IManagedMqttClientOptions>(serviceProvider =>
            {
                var optionsBuilder = new AspCoreManagedMqttClientOptionBuilder(serviceProvider);
                configuration(optionsBuilder);
                return optionsBuilder.Build();
            });
            services.AddSingleton<MqttClientService>();
            services.AddSingleton<IHostedService>(serviceProvider =>
            {
                return serviceProvider.GetService<MqttClientService>();
            });
            services.AddTransient<MqttClientServiceProvider>(serviceProvider =>
            {
                var mqttClientService = serviceProvider.GetService<MqttClientService>();
                var mqttClientServiceProvider = new MqttClientServiceProvider(mqttClientService);
                return mqttClientServiceProvider;
            });
            return services;
        }
    }
}
