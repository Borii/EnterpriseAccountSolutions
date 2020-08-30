using System.Net.Http;

namespace AccountApi
{
    public class CustomerClient : ICustomerClient
    {
        public CustomerClient(HttpClient httpClient)
        {
            httpClient.DefaultRequestHeaders.Add("Accept", "*/*");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory");
            this.HttpClient = httpClient;
        }

        public HttpClient HttpClient { get; }
    }
}
