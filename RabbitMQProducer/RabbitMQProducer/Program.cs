using Newtonsoft.Json;
using RabbitMQ.Client;
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
Console.WriteLine("started!");
var message = new { Name = "Producer", Message = "Hello!" };
var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
channel.BasicPublish("", "demo-queue-leason", null, body);
Console.ReadLine();