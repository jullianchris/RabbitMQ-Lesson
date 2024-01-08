using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQConsumer
{
    public static class DirectExchangeConsumer
    {
        public static void Consume(IModel channel)
        {
            channel.ExchangeDeclare("demo-direct-exchnage", ExchangeType.Direct);

            channel.QueueDeclare(
                        "demo-queue-leason",
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);
            channel.QueueBind("demo-queue-leason", "demo-direct-exchnage", "account.init");
            channel.BasicQos(0, 10, false);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };
            channel.BasicConsume("demo-queue-leason", true, consumer);
            Console.ReadLine();
        }
    }
}
