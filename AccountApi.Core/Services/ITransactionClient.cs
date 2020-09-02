using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AccountApi.Core.Services.Requests;
using AccountApi.Core.Services.Responses;

namespace AccountApi.Core.Services
{
    public interface ITransactionClient
    {
        Task<IEnumerable<TransactionResponse>> Get(string baseUrl);
        Task<HttpResponseMessage> Post(TransactionRequest request);
    }
}