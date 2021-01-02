using Newtonsoft.Json;

namespace SenseHome.DataSync.Models
{
    public static class MqttBroadCastMessageExtension
    {
        public static DomainModels.TemperatureHumidity ToTemperatureHumidityModel(this MqttBroadcastMessage message)
        {
            dynamic data = JsonConvert.DeserializeObject(message.Payload);

            return new DomainModels.TemperatureHumidity
            {
                Topic = message.Topic,
                Date = message.Date,
                Humidity = data["humidity"],
                Temperature = data["temperature"]
            };
        }

        public static DomainModels.MotionDetection ToMotionDetectionModel(this MqttBroadcastMessage message)
        {
            return new DomainModels.MotionDetection
            {
                Date = message.Date
            };
        }
    }
}
