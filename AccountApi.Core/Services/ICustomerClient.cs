using System.Net.Http;

namespace AccountApi
{
    public interface ICustomerClient
    {
        HttpClient HttpClient { get; }
    }
}