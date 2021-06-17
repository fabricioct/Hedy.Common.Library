namespace Hedy.Core.Infrastructure.Rest
{
    using System.Net.Http;
    using System.Text.Json;

    public static class HttpResponseExtensions
    {
        public static T ContentAsType<T>(this HttpResponseMessage response)
        {
            var data = response.Content.ReadAsStringAsync().Result;
            return string.IsNullOrEmpty(data) ?
                            default(T) :
                            JsonSerializer.Deserialize<T>(data);
        }

        public static string ContentAsJson(this HttpResponseMessage response)
        {
            var data = response.Content.ReadAsStringAsync().Result;
            return JsonSerializer.Serialize(data);
        }

        public static string ContentAsString(this HttpResponseMessage response)
        {
            return response.Content.ReadAsStringAsync().Result;
        }
    }
}