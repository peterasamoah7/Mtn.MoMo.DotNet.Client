using MtnMomo.DotNet.Client.Common.Models.Response;
using MtnMomo.DotNet.Client.Disbursements.Models.Request;
using MtnMomo.DotNet.Client.Disbursements.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MtnMomo.DotNet.Client.Disbursements.Client
{
    public interface IDisbursementsClient
    {
        Task<ClientResponse<AccountBalanceResponse>> AccountBalance();
        Task<ClientResponse> AccountHolder(string accountHolderIdType, string accountHolderId);
        Task<ClientResponse<TransferResponse>> GetTransfer(string referenceId);
        Task<ClientResponse<string>> PostTransfer(TransferRequest request, string callbackUrl = null);
    }
}
