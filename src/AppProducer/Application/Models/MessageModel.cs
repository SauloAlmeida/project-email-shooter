using Newtonsoft.Json;
using System.Text;

namespace AppProducer.Application.Models
{
    public class MessageModel
    {
        protected MessageModel(string payload) => Payload = payload;

        public string Payload { get; private set; }

        public DateTime CreatedAt = DateTime.Now;

        public static MessageModel Create(string payload) => new(payload);

        public override string ToString() => JsonConvert.SerializeObject(this);

        public byte[] ToBytes() => Encoding.UTF8.GetBytes(ToString());
    }
}
