using System.Net.Http;
using System.Threading.Tasks;

// https://tahirnaushad.com/2017/09/04/consuming-asp-net-core-2-0-web-api-using-httpclient/
namespace Hedy.Core.Infrastructure.Rest
{
    public static class HttpRequestFactory
    {
        public static async Task<HttpResponseMessage> Get(string requestUri)
            => await Get(requestUri, string.Empty);

        public static async Task<HttpResponseMessage> Get(string requestUri, string bearerToken)
        {
            var builder = new HttpRequestBuilder()
                                .AddMethod(HttpMethod.Get)
                                .AddRequestUri(requestUri)
                                .AddBearerToken(bearerToken);

            return await builder.SendAsync();
        }

        public static async Task<HttpResponseMessage> Post(string requestUri, object value)
            => await Post(requestUri, value, string.Empty);

        public static async Task<HttpResponseMessage> Post(
            string requestUri, object value, string bearerToken)
        {
            var builder = new HttpRequestBuilder()
                                .AddMethod(HttpMethod.Post)
                                .AddRequestUri(requestUri)
                                .AddContent(new JsonContent(value))
                                .AddBearerToken(bearerToken);

            return await builder.SendAsync();
        }

        public static async Task<HttpResponseMessage> Put(string requestUri, object value)
            => await Put(requestUri, value, string.Empty);

        public static async Task<HttpResponseMessage> Put(
            string requestUri, object value, string bearerToken)
        {
            var builder = new HttpRequestBuilder()
                                .AddMethod(HttpMethod.Put)
                                .AddRequestUri(requestUri)
                                .AddContent(new JsonContent(value))
                                .AddBearerToken(bearerToken);

            return await builder.SendAsync();
        }

        public static async Task<HttpResponseMessage> Patch(string requestUri, object value)
            => await Patch(requestUri, value, string.Empty);

        public static async Task<HttpResponseMessage> Patch(
            string requestUri, object value, string bearerToken)
        {
            var builder = new HttpRequestBuilder()
                                .AddMethod(new HttpMethod("PATCH"))
                                .AddRequestUri(requestUri)
                                .AddContent(new PatchContent(value))
                                .AddBearerToken(bearerToken);

            return await builder.SendAsync();
        }

        public static async Task<HttpResponseMessage> Delete(string requestUri)
            => await Delete(requestUri, string.Empty);

        public static async Task<HttpResponseMessage> Delete(
            string requestUri, string bearerToken)
        {
            var builder = new HttpRequestBuilder()
                                .AddMethod(HttpMethod.Delete)
                                .AddRequestUri(requestUri)
                                .AddBearerToken(bearerToken);

            return await builder.SendAsync();
        }

        public static async Task<HttpResponseMessage> PostFile(
            string requestUri,
            string filePath,
            string apiParamName)
        {
            return await PostFile(requestUri, filePath, apiParamName, string.Empty);
        }

        public static async Task<HttpResponseMessage> PostFile(
            string requestUri,
            string filePath,
            string apiParamName,
            string bearerToken)
        {
            var builder = new HttpRequestBuilder()
                                .AddMethod(HttpMethod.Post)
                                .AddRequestUri(requestUri)
                                .AddContent(new FileContent(filePath, apiParamName))
                                .AddBearerToken(bearerToken);

            return await builder.SendAsync();
        }
    }
}