using System;
using Newtonsoft.Json;

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
            return JsonConvert.SerializeObject(this);
        }

        public static MqttBroadcastMessage ParseFromJson(string jsonSource)
        {
            return JsonConvert.DeserializeObject<MqttBroadcastMessage>(jsonSource);
        }
    }
}
