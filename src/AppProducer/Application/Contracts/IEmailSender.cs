using AppProducer.Application.Models;

namespace AppProducer.Application.Contracts
{
    public interface IEmailSender
    {
        void SendEmail(params EmailModel[] model);
    }
}
