using MtnMomo.DotNet.Client.Common.Models;
using MtnMomo.DotNet.Client.Common.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MtnMomo.DotNet.Client.Remittance.Client
{
    public interface IRemittanceClient
    {
        Task<ClientResponse<AccountBalanceResponse>> AccountBalance();
        Task<ClientResponse> AccountHolder(string accountHolderIdType, string accountHolderId);
        Task<ClientResponse<TransferResponse>> GetTransfer(string referenceId);
        Task<ClientResponse<string>> PostTransfer(TransferRequest request, string callbackUrl = null);
    }
}
