using System;
using System.Threading.Tasks;
using System.Net.Http;

namespace LogExpertSharp
{
    public sealed class Connection : IDisposable
    {
#if DEBUG
        private const string DOMAIN = "alpha";
#else
        private const string DOMAIN = "gis";
#endif

        private const string BASE_URL = "https://" + DOMAIN + ".logexpert.ru";

        private readonly string token;

        public readonly HttpClient HttpClient;

        public Connection(string token)
        {
            this.token = token;
            HttpClient = new HttpClient { BaseAddress = new Uri(BASE_URL) };
        }

        public void Dispose() => HttpClient?.Dispose();

        public MultipartFormDataContent GetAuthenticatedHttpContent()
        {
            var content = new MultipartFormDataContent();
            content.Add(new StringContent(token), nameof(token));
            return content;
        }

        public MultipartFormDataContent GetAuthenticatedHttpContent(params (string key, string value)[] valuePairs)
        {
            var content = GetAuthenticatedHttpContent();

            foreach(var item in valuePairs)
            {
                content.Add( new StringContent(item.value), item.key );
            }

            return content;
        }

        public Task<HttpResponseMessage> Post(string method, params (string key, string value)[] valuePairs)
        {
            return HttpClient.PostAsync(method, GetAuthenticatedHttpContent(valuePairs));
        }
    }
}