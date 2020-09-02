using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using AccountApi.Core.Services.Requests;
using AccountApi.Core.Services.Responses;
using Newtonsoft.Json;

namespace AccountApi.Core.Services
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

        public async Task<IEnumerable<TransactionResponse>> Get(string baseUrl)
        {
            var resultAsString = await this.httpClient.GetStringAsync(baseUrl);
            return JsonConvert.DeserializeObject<List<TransactionResponse>>(resultAsString);
        }

        public Task<HttpResponseMessage> Post(TransactionRequest request) =>
            this.httpClient.PostAsync(string.Empty, new ObjectContent<TransactionRequest>(request, new JsonMediaTypeFormatter()));
    }
}
