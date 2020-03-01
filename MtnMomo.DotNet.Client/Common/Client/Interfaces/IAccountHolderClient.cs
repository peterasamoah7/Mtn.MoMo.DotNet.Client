using MtnMomo.DotNet.Client.Common.Models.Request;
using MtnMomo.DotNet.Client.Common.Models.Response;
using System.Threading.Tasks;

namespace MtnMomo.DotNet.Client.Common.Client.Interfaces
{
    public interface IAccountHolderClient
    {
        Task<ClientResponse> AccountHolder(AccountHolderRequest request);
    }
}
