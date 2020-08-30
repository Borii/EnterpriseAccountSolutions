using AccountApi.Services.Requests;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;

namespace AccountApi.Services
{
    public class TransactionClient : ITransactionClient
    {
        private HttpClient httpClient;

        public TransactionClient(HttpClient httpClient)
        {
            httpClient.DefaultRequestHeaders.Add("Accept", "*/*");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory");
            this.httpClient = httpClient;
        }

        public async Task<string> Get(string baseUrl) =>
            await this.httpClient.GetStringAsync(baseUrl);

        public Task<HttpResponseMessage> Post(TransactionRequest request) =>
            this.httpClient.PostAsync(string.Empty, new ObjectContent<TransactionRequest>(request, new JsonMediaTypeFormatter()));
    }
}
