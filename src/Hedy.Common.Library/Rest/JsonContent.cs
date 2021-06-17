namespace Hedy.Core.Infrastructure.Rest
{
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;

    public class JsonContent : StringContent
    {
        public JsonContent(object value)
            : base(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json")
        {
        }

        public JsonContent(object value, string mediaType)
            : base(JsonSerializer.Serialize(value), Encoding.UTF8, mediaType)
        {
        }
    }
}