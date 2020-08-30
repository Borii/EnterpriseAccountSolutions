using System.Net.Http;

namespace AccountApi.Services
{
    public class TransactionClient
    {
        public TransactionClient(HttpClient httpClient)
        {
            httpClient.DefaultRequestHeaders.Add("Accept", "*/*");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory");
            this.HttpClient = httpClient;
        }

        public HttpClient HttpClient { get; }
    }
}
