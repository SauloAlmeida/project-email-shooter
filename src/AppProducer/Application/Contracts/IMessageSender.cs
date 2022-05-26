using AppProducer.Application.Models;

namespace AppProducer.Application.Contracts
{
    public interface IMessageSender
    {
        void Publish(params MessageModel[] model);
    }
}
