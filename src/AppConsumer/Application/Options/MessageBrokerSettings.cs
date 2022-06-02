using AppConsumer.Application.Contracts;

namespace AppConsumer.Application.Options
{
    public class MessageBrokerSettings : IMessageBrokerSettings
    {
        const string ENV_QUEUE_NAME = "MESSAGE_BROKER_QUEUE_DEFAULT";
        const string ENV_URL_NAME = "MESSAGE_BROKER_URL";

        public string Queue => Environment.GetEnvironmentVariable(ENV_QUEUE_NAME) ?? throw new ArgumentNullException(ENV_QUEUE_NAME);
        public string Url => Environment.GetEnvironmentVariable(ENV_URL_NAME) ?? throw new ArgumentNullException(ENV_URL_NAME);
    }
}
