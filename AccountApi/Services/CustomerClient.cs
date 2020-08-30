using System;
using System.Net.Http;

namespace AccountApi
{
    public class CustomerClient
    {
        public CustomerClient(HttpClient httpClient)
        {
            httpClient.DefaultRequestHeaders.Add("Accept", "*/*");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory");
            this.HttpClient = httpClient;
        }

        public HttpClient HttpClient { get; }

        //public async Task<string> Get(Guid id)
        //{
        //    var content = await response.Content.ReadAsStringAsync();

        //    //var catalog = JsonConvert.DeserializeObject<Customer>(response);

        //    return content;
        //}

        //public async Task<string> Get()
        //{
        //    var content = await response.Content.ReadAsStringAsync();

        //    //var catalog = JsonConvert.DeserializeObject<Customer>(response);

        //    return content;
        //}
    }
}
