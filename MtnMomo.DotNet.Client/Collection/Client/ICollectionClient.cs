using MtnMomo.DotNet.Client.Collection.Models.Reponse;
using MtnMomo.DotNet.Client.Collection.Models.Request;
using MtnMomo.DotNet.Client.Common.Models.Response;
using System.Threading.Tasks;

namespace MtnMomo.DotNet.Client.Collection.Client
{
    public interface ICollectionClient
    {
        Task<ClientResponse<string>> PostRequestToPay(PostReqesutToPayRequest request, string callbackUrl = null);
        Task<ClientResponse<GetReqesutToPayReponse>> GetRequestToPay(string referenceId);
        Task<ClientResponse<AccountBalanceResponse>> AccountBalance();
        Task<ClientResponse> AccountHolder(string accountHolderIdType, string accountHolderId);
    }
}
