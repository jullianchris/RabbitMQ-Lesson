using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace RabbitMQProducer
{
    public static class FanoutExchangePublisher
    {
        public static void Publish(IModel channel)
        {
            var ttl = new Dictionary<string, object>
            {
                {"x-message-ttl",30000 }
            };
            channel.ExchangeDeclare("demo-fanout-exchnage", ExchangeType.Fanout, arguments: ttl);

            var count = 0;

            while (true)
            {
                var message = new { Name = "Producer", Message = $"Hello! Count: {count}" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                
                var properties = channel.CreateBasicProperties();
                properties.Headers = new Dictionary<string, object> { { "account", "new" } };
                
                channel.BasicPublish("demo-fanout-exchnage", string.Empty,properties, body);
                count++;
                Thread.Sleep(1000);

            }
        }
    }
}
