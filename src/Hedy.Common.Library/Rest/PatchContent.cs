namespace Hedy.Core.Infrastructure.Rest
{
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;

    public class PatchContent : StringContent
    {
        public PatchContent(object value)
            : base(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json-patch+json")
        {
        }
    }
}