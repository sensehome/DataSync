using System;
using MQTTnet.Client.Options;

namespace SenseHome.DataSync.Options
{
    public class AspCoreMqttClientOptionsBuilder : MqttClientOptionsBuilder
    {
        public IServiceProvider ServiceProvider { get; }

        public AspCoreMqttClientOptionsBuilder(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
    }
}
