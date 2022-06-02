namespace AppConsumer.Application.Contracts
{
    public interface IMessageBrokerSettings
    {
        public string Url { get; }
        public string Queue { get; }
    }
}
