using AppProducer.Application.Contracts;
using AppProducer.Application.Models;

namespace AppProducer.Infrastructure
{
    public class EmailSender : IEmailSender
    {
        readonly IMessageSender MessageSender;

        public EmailSender(IMessageSender messageSender) => MessageSender = messageSender;

        public void SendEmail(params EmailModel[] model)
        {
            var messageModel = model.Select(emailModel => MessageModel.Create(emailModel.ToString()))
                                    .ToArray();

            MessageSender.Publish(messageModel);
        }
    }
}
