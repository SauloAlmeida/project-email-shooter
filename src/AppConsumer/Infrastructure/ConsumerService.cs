using AppConsumer.Application.Contracts;
using AppConsumer.Application.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace AppConsumer.Infrastructure
{
    public class ConsumerService : BackgroundService
    {
        private readonly IMessageBrokerSettings Configuration;
        private readonly IConnection Connection;
        private readonly IModel Channel;
        private readonly IServiceProvider ServiceProvider;

        const int MESSAGE_BROKER_RETRY_COUNTER = 5;

        public ConsumerService(IMessageBrokerSettings configuration, IServiceProvider serviceProvider)
        {
            Configuration = configuration;

            ServiceProvider = serviceProvider;

            var factory = new ConnectionFactory()
            {
                Uri = new Uri(Configuration.Url)
            };

            Connection = CreateServiceBusConnection(factory);

            Channel = Connection.CreateModel();

            Channel.QueueDeclare(queue: Configuration.Queue,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
        }

        static IConnection CreateServiceBusConnection(ConnectionFactory factory)
        {
            Console.WriteLine("Creating connection with message broker.");

            int retries = 0;

            while (true)
            {
                try
                {
                    return factory.CreateConnection();
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

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Execute Consumer Service");

            var consumer = new EventingBasicConsumer(Channel);

            consumer.Received += (sender, eventArgs) =>
            {
                Console.WriteLine("Received a message!");

                var emailModel = GetEmailModelByEvent(eventArgs);

                NotifyUser(emailModel);

                Channel.BasicAck(eventArgs.DeliveryTag, multiple: false);
            };

            Console.WriteLine("Ending the receivement!");

            Channel.BasicConsume(Configuration.Queue, autoAck: false, consumer);

            return Task.CompletedTask;
        }

        static EmailModel? GetEmailModelByEvent(BasicDeliverEventArgs eventArgs)
        {
            var contentArray = eventArgs.Body.ToArray();

            var contentString = Encoding.UTF8.GetString(contentArray);

            var messageModel = JsonConvert.DeserializeObject<MessageModel<EmailModel>>(contentString);

            return messageModel?.Content;
        }

        void NotifyUser(EmailModel? model)
        {
            if (model is null) return;

            using var scope = ServiceProvider.CreateScope();

            var emailDispatch = scope.ServiceProvider.GetRequiredService<IEmailDispatcher>();

            emailDispatch.SendEmail(model);
        }
    }
}
