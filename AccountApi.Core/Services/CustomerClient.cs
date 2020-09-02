using System.Net.Http;
using System.Threading.Tasks;
using AccountApi.Core.Services.Responses;
using Newtonsoft.Json;

namespace AccountApi.Core.Services
{
    public class CustomerClient : ICustomerClient
    {
        public CustomerClient(HttpClient httpClient)
        {
            httpClient.DefaultRequestHeaders.Add("Accept", "*/*");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory");
            this.HttpClient = httpClient;
        }

        private HttpClient HttpClient { get; }

        public async Task<CustomerResponse> GetById(string Id)
        {
            var customerResponse = await this.HttpClient.GetStringAsync(Id);
            return JsonConvert.DeserializeObject<CustomerResponse>(customerResponse);
        }
    }
}
