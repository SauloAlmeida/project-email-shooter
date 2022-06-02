using Newtonsoft.Json;

namespace AppConsumer.Application.Models
{
    public class MessageModel<T>
    {
        public string? Payload { get; set; }

        public T? Content => string.IsNullOrEmpty(Payload) is false ? JsonConvert.DeserializeObject<T>(Payload) : default;

        public DateTime CreatedAt;
    }
}
