using AppProducer.Application.Contracts;
using AppProducer.Application.Models;

namespace AppProducer.Infrastructure
{
    public class Seed
    {
        private readonly IEmailSender EmailSender;

        public Seed(IEmailSender emailSender)
        {
            EmailSender = emailSender;
        }

        public void Execute()
        {
            Console.WriteLine("Send emails to queue!");

            var emailsToSend = Enumerable.Range(0, 1000)
                                         .Select(i => EmailModel.Create($"email{i}.google.com.br",
                                                                        $"Subject - {i}",
                                                                        $"{i} - Lorem Ipsum is simply dummy text of the printing and typesetting industry. " +
                                                                        $"Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, " +
                                                                        $"when an unknown printer took a galley of type and scrambled it to make a type specimen book "))
                                         .ToArray();

            EmailSender.SendEmail(emailsToSend);
        }
    }
}