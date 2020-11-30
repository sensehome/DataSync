using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Extensions.ManagedClient;
using SenseHome.DataSync.Services.RedisCache;
using Serilog;

namespace SenseHome.DataSync.Services.MqttClient
{
    public class MqttClientService : IMqttClientService
    {
        private readonly IManagedMqttClient mqttClient;
        private readonly IManagedMqttClientOptions options;
        private readonly ILogger logger;
        private readonly IRedisCacheService redisCacheService;

        public MqttClientService(ILogger logger, IRedisCacheService redisCacheService, IManagedMqttClientOptions options)
        {
            this.options = options;
            this.logger = logger;
            this.redisCacheService = redisCacheService;
            mqttClient = new MqttFactory().CreateManagedMqttClient();
            ConfigureMqttClient();
        }

        private void ConfigureMqttClient()
        {
            mqttClient.ConnectedHandler = this;
            mqttClient.DisconnectedHandler = this;
            mqttClient.ApplicationMessageReceivedHandler = this;
        }

        public async Task HandleApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs eventArgs)
        {
            var payload = Encoding.ASCII.GetString(eventArgs.ApplicationMessage.Payload);
            await redisCacheService.PushBackAsync(eventArgs.ApplicationMessage.Topic, payload);
            logger.Information(payload);
        }

        public async Task HandleConnectedAsync(MqttClientConnectedEventArgs eventArgs)
        {
            System.Console.WriteLine("connected");
            await mqttClient.SubscribeAsync("#");
        }

        public Task HandleDisconnectedAsync(MqttClientDisconnectedEventArgs eventArgs)
        {
            throw new System.NotImplementedException();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await mqttClient.StartAsync(options);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await mqttClient.StopAsync();
        }
    }
}
