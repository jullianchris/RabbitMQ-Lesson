using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQProducer;
using System.Text;

var factory = new ConnectionFactory
{
    Uri = new Uri("amqp://guest:guest@localhost:5672")
};

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();
channel.QueueDeclare(
    "demo-queue-leason",
    durable: true,
    exclusive: false ,
    autoDelete: false,
    arguments: null);

//QueueProducer.Publish(channel);
DirectExchangePublisher.Publish(channel);