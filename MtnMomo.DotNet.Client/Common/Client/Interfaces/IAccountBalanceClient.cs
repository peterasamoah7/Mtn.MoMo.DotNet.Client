using MtnMomo.DotNet.Client.Common.Models.Request;
using MtnMomo.DotNet.Client.Common.Models.Response;
using System.Threading.Tasks;

namespace MtnMomo.DotNet.Client.Common.Client.Interfaces
{
    public interface IAccountBalanceClient
    {
        Task<ClientResponse<AccountBalanceResponse>> AccountBalance(AccountBalanceRequest request);
    }
}
