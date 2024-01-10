using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMQConsumer
{
    public static class FanoutExchangeConsumer
    {
        public static void Consume(IModel channel)
        {
            channel.ExchangeDeclare("demo-fanout-exchnage", ExchangeType.Fanout);

            channel.QueueDeclare(
                        "demo-fanout-queue-leason",
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);
            channel.QueueBind("demo-fanout-queue-leason", "demo-fanout-exchnage", string.Empty);
            channel.BasicQos(0, 10, false);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };
            channel.BasicConsume("demo-fanout-queue-leason", true, consumer);
            Console.ReadLine();
        }
    }
}
