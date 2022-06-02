using Newtonsoft.Json;

namespace AppConsumer.Application.Models
{
    public class EmailModel
    {
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public override string ToString() 
            => JsonConvert.SerializeObject(this);
    }
}
