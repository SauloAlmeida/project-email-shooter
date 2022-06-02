using AppConsumer.Application.Contracts;
using AppConsumer.Application.Models;

namespace AppConsumer.Infrastructure
{
    public class EmailDispatcher : IEmailDispatcher
    {
        public void SendEmail(params EmailModel[] model)
        {
            Console.WriteLine("EmailDispatcher - SendEmail");

            foreach (var item in model)
            {
                Console.WriteLine("Email sent to user!");
            }
        }
    }
}
