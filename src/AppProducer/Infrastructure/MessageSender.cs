using AppProducer.Application.Contracts;
using AppProducer.Application.Models;
using RabbitMQ.Client;

namespace AppProducer.Infrastructure
{
    public class MessageSender : IMessageSender
    {
        readonly string MESSAGE_BROKER_QUEUE_DEFAULT;
        readonly string MESSAGE_BROKER_URL;
        readonly int MESSAGE_BROKER_RETRY_COUNTER = 5;

        private readonly ConnectionFactory Factory;

        public MessageSender()
        {
            MESSAGE_BROKER_QUEUE_DEFAULT = Environment.GetEnvironmentVariable(nameof(MESSAGE_BROKER_QUEUE_DEFAULT)) ?? throw new ArgumentNullException(nameof(MESSAGE_BROKER_QUEUE_DEFAULT));
            MESSAGE_BROKER_URL = Environment.GetEnvironmentVariable(nameof(MESSAGE_BROKER_URL)) ?? throw new ArgumentNullException(nameof(MESSAGE_BROKER_URL));

            Factory = new ConnectionFactory()
            {
                Uri = new Uri(@MESSAGE_BROKER_URL)
            };
        }

        public void Publish(params MessageModel[] models)
        {
            try
            {
                using IConnection connection = CreateServiceBusConnection();
                using IModel channel = connection.CreateModel();

                ConfigureQueue(channel);

                foreach (var item in models)
                {
                    var byteMessage = item.ToBytes();

                    channel.BasicPublish(exchange: string.Empty,
                                         routingKey: MESSAGE_BROKER_QUEUE_DEFAULT,
                                         basicProperties: null,
                                         body: byteMessage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"MessageSender > Publish | Error: {ex.Message}!");
                throw;
            }
        }

        private IConnection CreateServiceBusConnection()
        {
            Console.WriteLine("Creating connection with message broker.");

            int retries = 0;

            while (true)
            {
                try
                {
                    return Factory.CreateConnection();
                }
                catch (Exception)
                {
                    if (MESSAGE_BROKER_RETRY_COUNTER == retries) throw;

                    Console.WriteLine("Not connected yet!");

                    retries++;

                    Thread.Sleep(TimeSpan.FromSeconds(5));
                }
            }
        }

        public void ConfigureQueue(IModel channel)
        {
            channel.QueueDeclare(queue: MESSAGE_BROKER_QUEUE_DEFAULT,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
        }
    }
}
