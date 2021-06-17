using System;
using System.Net.Http;

namespace Hedy.Core.Infrastructure.Rest
{
    public sealed class HttpClientSingleton
    {
        private static HttpClientSingleton instance = null;
        private HttpClient httpClient = null;
        private TimeSpan timeout = new TimeSpan(0, 0, 30);

        public HttpClient Client
        {
            get
            {
                return httpClient;
            }
        }

        private HttpClientSingleton()
        {
            CreateHttpCliente();
        }

        public static HttpClientSingleton Instance
        {
            get
            {
                // No Thread Safe Singleton
                if (instance == null)
                    instance = new HttpClientSingleton();

                return instance;
            }
        }

        public void CreateHttpCliente(bool allowAutoRedirect = false)
        {
            var handler = new HttpClientHandler
            {
                AllowAutoRedirect = allowAutoRedirect
            };

            httpClient = new HttpClient(handler)
            {
                Timeout = this.timeout
            };
        }
    }
}