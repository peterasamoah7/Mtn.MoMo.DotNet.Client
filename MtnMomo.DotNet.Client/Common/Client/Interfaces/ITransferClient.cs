using MtnMomo.DotNet.Client.Common.Config;
using MtnMomo.DotNet.Client.Common.Models;
using MtnMomo.DotNet.Client.Common.Models.Response;
using System.Threading.Tasks;

namespace MtnMomo.DotNet.Client.Common.Client.Interfaces
{
    public interface ITransferClient
    {
        Task<ClientResponse<string>> PostTransfer(TransferRequest request, TransferConfig config, string callbackUrl = null);
        Task<ClientResponse<TransferResponse>> GetTransfer(string referenceId, TransferConfig config);
    }
}
