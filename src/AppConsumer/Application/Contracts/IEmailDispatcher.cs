using AppConsumer.Application.Models;

namespace AppConsumer.Application.Contracts
{
    public interface IEmailDispatcher
    {
        void SendEmail(params EmailModel[] model);
    }
}
