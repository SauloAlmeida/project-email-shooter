using Newtonsoft.Json;

namespace AppProducer.Application.Models
{
    public class EmailModel
    {
        protected EmailModel(string email, string subject, string body)
        {
            Email = email;
            Subject = subject;
            Body = body;
        }

        public string Email { get; private set; }
        public string Subject { get; private set; }
        public string Body { get; private set; }

        public static EmailModel Create(string email, string subject, string body) => new(email, subject, body);

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
