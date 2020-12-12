using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SenseHome.DataSync.Models
{
    public class MqttBroadcastMessage
    {
        public string Payload { get; set; }
        public string Topic { get; set; }
        public string ClientId { get; set; }
        public DateTime Date { get; set; }

        public string ToJsonString()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                },
                Formatting = Formatting.Indented
            });
        }

        public static MqttBroadcastMessage ParseFromJson(string jsonSource)
        {
            return JsonConvert.DeserializeObject<MqttBroadcastMessage>(jsonSource);
        }
    }
}
