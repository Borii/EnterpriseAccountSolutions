using System.Net.Http;
using System.Threading.Tasks;
using AccountApi.Core.Services.Responses;

namespace AccountApi.Core.Services
{
    public interface ICustomerClient
    {
        Task<CustomerResponse> GetById(string Id);
    }
}