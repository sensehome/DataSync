using System;
using MQTTnet.Extensions.ManagedClient;

namespace SenseHome.DataSync.Options
{
    public class AspCoreManagedMqttClientOptionBuilder : ManagedMqttClientOptionsBuilder
    {
        public IServiceProvider ServiceProvider { get; }

        public AspCoreManagedMqttClientOptionBuilder(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
    }
}
