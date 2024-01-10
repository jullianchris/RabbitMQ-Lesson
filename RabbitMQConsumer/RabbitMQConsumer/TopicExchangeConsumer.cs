using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMQConsumer
{
    public static class TopicExchangeConsumer
    {
        public static void Consume(IModel channel)
        {
            channel.ExchangeDeclare("demo-topic-exchnage", ExchangeType.Topic);

            channel.QueueDeclare(
                        "demo-topic-queue-leason",
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);
            channel.QueueBind("demo-topic-queue-leason", "demo-topic-exchnage", "account.");
            channel.BasicQos(0, 10, false);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };
            channel.BasicConsume("demo-topic-queue-leason", true, consumer);
            Console.ReadLine();
        }
    }
}
