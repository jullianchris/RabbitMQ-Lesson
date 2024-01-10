using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMQConsumer
{
    public static class HeaderExchangeConsumer
    {
        public static void Consume(IModel channel)
        {
            channel.ExchangeDeclare("demo-header-exchnage", ExchangeType.Headers);

            channel.QueueDeclare(
                        "demo-header-queue-leason",
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);
            var header = new Dictionary<string, object> { { "account","new"} };
            channel.QueueBind("demo-header-queue-leason", "demo-header-exchnage", string.Empty,header);
            channel.BasicQos(0, 10, false);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };
            channel.BasicConsume("demo-header-queue-leason", true, consumer);
            Console.ReadLine();
        }
    }
}
