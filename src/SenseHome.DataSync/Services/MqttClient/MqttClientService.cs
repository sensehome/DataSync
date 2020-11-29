using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;
using Serilog;
using StackExchange.Redis;

namespace SenseHome.DataSync.Services.MqttClient
{
    public class MqttClientService : IMqttClientService
    {
        private readonly IMqttClient mqttClient;
        private readonly IMqttClientOptions options;
        private readonly ILogger logger;
        private readonly IDatabase redisDb;

        public MqttClientService(ILogger logger, IDatabase redisDb, IMqttClientOptions options)
        {
            this.options = options;
            this.logger = logger;
            this.redisDb = redisDb;
            mqttClient = new MqttFactory().CreateMqttClient();
            ConfigureMqttClient();
        }

        private void ConfigureMqttClient()
        {
            mqttClient.ConnectedHandler = this;
            mqttClient.DisconnectedHandler = this;
            mqttClient.ApplicationMessageReceivedHandler = this;
        }

        public Task HandleApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs eventArgs)
        {
            return Task.Run(() =>
            {
                var payload = Encoding.ASCII.GetString(eventArgs.ApplicationMessage.Payload);
                logger.Information(payload);
            });
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
            await mqttClient.ConnectAsync(options);
            if (!mqttClient.IsConnected)
            {
                await mqttClient.ReconnectAsync();
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                var disconnectOption = new MqttClientDisconnectOptions
                {
                    ReasonCode = MqttClientDisconnectReason.NormalDisconnection,
                    ReasonString = "NormalDiconnection"
                };
                await mqttClient.DisconnectAsync(disconnectOption, cancellationToken);
            }
            await mqttClient.DisconnectAsync();
        }
    }
}
