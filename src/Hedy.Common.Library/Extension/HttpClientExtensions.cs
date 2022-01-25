using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Hedy.Common.Library.Extension
{
    public static class HttpClientExtensions
    {
        #region - Read/Write JSON Responses -

        /// <summary>
        /// HttpClientExtensions for Dotnet Core
        /// see: https://stackoverflow.com/questions/40027299/where-is-postasjsonasync-method-in-asp-net-core
        ///
        /// Related Nuget Package:
        /// https://github.com/bolorundurowb/aspnetcore-httpclientextensions
        /// </summary>
        public static Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient httpClient, string url, T data)
        {
            var dataAsString = JsonSerializer.Serialize(data);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return httpClient.PostAsync(url, content);
        }

        /// <summary>
        /// HttpClientExtensions for Dotnet Core
        /// see: https://stackoverflow.com/questions/40027299/where-is-postasjsonasync-method-in-asp-net-core
        ///
        /// Related Nuget Package:
        /// https://github.com/bolorundurowb/aspnetcore-httpclientextensions
        /// </summary>
        public static Task<HttpResponseMessage> PutAsJsonAsync<T>(this HttpClient httpClient, string url, T data)
        {
            var dataAsString = JsonSerializer.Serialize(data);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return httpClient.PutAsync(url, content);
        }

        /// <summary>
        /// HttpClientExtensions for Dotnet Core
        /// see: https://stackoverflow.com/questions/40027299/where-is-postasjsonasync-method-in-asp-net-core
        ///
        /// Related Nuget Package:
        /// https://github.com/bolorundurowb/aspnetcore-httpclientextensions
        /// </summary>
        public static async Task<T> ReadAsJsonAsync<T>(this HttpContent content)
        {
            var dataAsString = await content.ReadAsStringAsync().ConfigureAwait(false);

            return JsonSerializer.Deserialize<T>(dataAsString);
        }

        #endregion - Read/Write JSON Responses -

        #region - HeadAsync -

        // HeadAsync:
        // https://github.com/microsoft/Git-Credential-Manager-Core/blob/master/src/shared/Microsoft.Git.CredentialManager/HttpClientExtensions.cs

        public static Task<HttpResponseMessage> HeadAsync(this HttpClient client, string requestUri)
        {
            return client.HeadAsync(new Uri(requestUri));
        }

        public static Task<HttpResponseMessage> HeadAsync(this HttpClient client, Uri requestUri)
        {
            return client.HeadAsync(requestUri, HttpCompletionOption.ResponseContentRead);
        }

        public static Task<HttpResponseMessage> HeadAsync(
            this HttpClient client,
            string requestUri,
            HttpCompletionOption completionOption
        )
        {
            return client.HeadAsync(new Uri(requestUri), completionOption);
        }

        public static Task<HttpResponseMessage> HeadAsync(
            this HttpClient client,
            Uri requestUri,
            HttpCompletionOption completionOption)
        {
            return client.HeadAsync(requestUri, completionOption, CancellationToken.None);
        }

        public static Task<HttpResponseMessage> HeadAsync(
            this HttpClient client,
            string requestUri,
            CancellationToken cancellationToken)
        {
            return client.HeadAsync(new Uri(requestUri), cancellationToken);
        }

        public static Task<HttpResponseMessage> HeadAsync(
            this HttpClient client,
            Uri requestUri,
            CancellationToken cancellationToken)
        {
            return client.HeadAsync(requestUri, HttpCompletionOption.ResponseContentRead, cancellationToken);
        }

        public static Task<HttpResponseMessage> HeadAsync(
            this HttpClient client,
            string requestUri,
            HttpCompletionOption completionOption,
            CancellationToken cancellationToken)
        {
            return client.HeadAsync(new Uri(requestUri), completionOption, cancellationToken);
        }

        public static Task<HttpResponseMessage> HeadAsync(
            this HttpClient client,
            Uri requestUri,
            HttpCompletionOption completionOption,
            CancellationToken cancellationToken)
        {
            return client.SendAsync(new HttpRequestMessage(HttpMethod.Head, requestUri), completionOption, cancellationToken);
        }

        #endregion - HeadAsync -

        #region - SendAsync with Content and Headers -

        public static Task<HttpResponseMessage> SendAsync(
            this HttpClient client,
            HttpMethod method,
            Uri requestUri,
            IEnumerable<KeyValuePair<string, IEnumerable<string>>> headers = null,
            HttpContent content = null)
        {
            var request = new HttpRequestMessage(method, requestUri)
            {
                Content = content
            };

            if (!(headers is null))
            {
                foreach (var (key, value) in headers)
                {
                    request.Headers.Add(key, value);
                }
            }

            return client.SendAsync(request);
        }

        #endregion - SendAsync with Content and Headers -

        /// <summary>
        /// This extension method for <see cref="HttpClient"/> provides a convenient overload that accepts
        /// a <see cref="string"/> accessToken to be used as Bearer authentication.
        /// </summary>
        /// <param name="httpClient">The <see cref="HttpClient"/> instance</param>
        /// <param name="method">The <see cref="HttpMethod"/></param>
        /// <param name="path">The path to the requested target</param>
        /// <param name="requestBody">The body of the request</param>
        /// <param name="accessToken">The access token to be used as Bearer authentication</param>
        /// <param name="ct">A <see cref="CancellationToken"/></param>
        /// <remarks>https://github.com/mspnp/multitenant-saas-guidance/blob/master/src/Tailspin.Surveys.Common/HttpClientExtensions.cs</remarks>
        public static async Task<HttpResponseMessage> SendRequestWithBearerTokenAsync(this HttpClient httpClient, HttpMethod method, string path, object requestBody, string accessToken, CancellationToken ct)
        {
            var request = new HttpRequestMessage(method, path);

            if (requestBody != null)
            {
                var json = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                request.Content = content;
            }

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await httpClient.SendAsync(request, ct);

            return response;
        }
    }
}