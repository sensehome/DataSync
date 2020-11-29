using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MQTTnet.Client.Options;
using SenseHome.DataSync.Configurations.Models;
using SenseHome.DataSync.Options;

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
                .WithCredentials(clientSettinigs.UserName, clientSettinigs.Password)
                .WithClientId(clientSettinigs.Id)
                .WithTcpServer(brokerSettings.Host, brokerSettings.Port);
            });
            return services;
        }

        private static IServiceCollection AddMqttClientServiceWithConfig(this IServiceCollection services, Action<AspCoreMqttClientOptionsBuilder> configure)
        {
            services.AddSingleton<IMqttClientOptions>(serviceProvider =>
            {
                var optionBuilder = new AspCoreMqttClientOptionsBuilder(serviceProvider);
                configure(optionBuilder);
                return optionBuilder.Build();
            });
            services.AddSingleton<MqttClientService>();
            services.AddSingleton<IHostedService>(serviceProvider =>
            {
                return serviceProvider.GetService<MqttClientService>();
            });
            services.AddSingleton<MqttClientServiceProvider>(serviceProvider =>
            {
                var mqttClientService = serviceProvider.GetService<MqttClientService>();
                var mqttClientServiceProvider = new MqttClientServiceProvider(mqttClientService);
                return mqttClientServiceProvider;
            });
            return services;
        }
    }
}
