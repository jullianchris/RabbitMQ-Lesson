using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQConsumer
{
    public  static class QueueConsumer
    {
        public static void Consume(IModel channel)
        {
            channel.QueueDeclare(
    "demo-queue-leason",
    durable: true,
    exclusive: false,
    autoDelete: false,
    arguments: null);
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
