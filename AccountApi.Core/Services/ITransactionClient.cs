using AccountApi.Services.Requests;
using AccountApi.Services.Responses;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AccountApi.Services
{
    public interface ITransactionClient
    {
        Task<string> Get(string baseUrl);
        Task<HttpResponseMessage> Post(TransactionRequest request);
    }
}