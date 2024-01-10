using RabbitMQ.Client;
using RabbitMQConsumer;

var factory = new ConnectionFactory
{
    Uri = new Uri("amqp://guest:guest@localhost:5672")
};

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();
//QueueConsumer.Consume(channel);
//DirectExchangeConsumer.Consume(channel);
//TopicExchangeConsumer.Consume(channel);
//HeaderExchangeConsumer.Consume(channel);
FanoutExchangeConsumer.Consume(channel);