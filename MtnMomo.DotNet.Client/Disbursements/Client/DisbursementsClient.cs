using MtnMomo.DotNet.Client.Common;
using MtnMomo.DotNet.Client.Common.Client.Interfaces;
using MtnMomo.DotNet.Client.Common.Config;
using MtnMomo.DotNet.Client.Common.Models;
using MtnMomo.DotNet.Client.Common.Models.Request;
using MtnMomo.DotNet.Client.Common.Models.Response;
using MtnMomo.DotNet.Client.Disbursements.Models;
using MtnMomo.DotNet.Client.Disbursements.Models.Config;
using System.Net;
using System.Threading.Tasks;

namespace MtnMomo.DotNet.Client.Disbursements.Client
{
    public class DisbursementsClient : IDisbursementsClient
    {
        private readonly ITokenClient tokenClient;
        private readonly IAccountBalanceClient accountBalanceClient;
        private readonly IAccountHolderClient accountHolderClient;
        private readonly ITransferClient transferClient; 
        private readonly DisbursementsConfig disbursementConfig;
         
        public DisbursementsClient(
            DisbursementsConfig disbursementConfig,
            ITokenClient tokenClient,
            IAccountBalanceClient accountBalanceClient,
            IAccountHolderClient accountHolderClient,
            ITransferClient transferClient)
        {
            this.disbursementConfig = disbursementConfig;
            this.tokenClient = tokenClient;
            this.accountBalanceClient = accountBalanceClient;
            this.accountHolderClient = accountHolderClient;
            this.transferClient = transferClient;
        }

        /// <summary>
        /// Get an auth token
        /// </summary>
        /// <returns></returns>
        private async Task<TokenResponse> GetToken()
        {
            var tokenRequest = new TokenRequest
            {
                RequestUri = DisbursementsRequestUri.Token,
                ApiKey = disbursementConfig.ApiKey,
                SubscriptionKey = disbursementConfig.SubscriptionKey,
                UserId = disbursementConfig.UserId
            };

            var token = await tokenClient.GetToken(tokenRequest);

            return token.Data;
        }

        /// <summary>
        /// Transfer from account to payee account
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ClientResponse<string>> PostTransfer(TransferRequest request, string callbackUrl = null)
        {
            var token = await GetToken();     

            var transferConfig = new TransferConfig
            {
                SubscriptionKey = disbursementConfig.SubscriptionKey,
                RequestUri = DisbursementsRequestUri.Transfer,
                Token = token?.AccessToken
            };

            return await transferClient.PostTransfer(request, transferConfig, callbackUrl);             
        }

        /// <summary>
        /// Get a transfer
        /// </summary>
        /// <param name="referenceId"></param>
        /// <returns></returns>
        public async Task<ClientResponse<TransferResponse>> GetTransfer(string referenceId)
        {
            var token = await GetToken();        

            var transferConfig = new TransferConfig
            {
                SubscriptionKey = disbursementConfig.SubscriptionKey,
                RequestUri = DisbursementsRequestUri.Transfer,
                Token = token?.AccessToken
            };

            return await transferClient.GetTransfer(referenceId, transferConfig);          
        }

        /// <summary>
        /// Get Account Balance
        /// </summary>
        /// <returns></returns>
        public async Task<ClientResponse<AccountBalanceResponse>> AccountBalance()
        {
            var token = await GetToken();

            var accountBalanceRquest = new AccountBalanceRequest
            {
                SubscriptionKey = disbursementConfig.SubscriptionKey,
                RequestUri = DisbursementsRequestUri.AccountBalance,
                Token = token?.AccessToken
            };

            return await accountBalanceClient.AccountBalance(accountBalanceRquest);
        }

        /// <summary>
        /// Validate account holder
        /// </summary>
        /// <param name="accountHolderIdType"></param>
        /// <param name="accountHolderId"></param>
        /// <returns></returns>
        public async Task<ClientResponse> AccountHolder(string accountHolderIdType, string accountHolderId)
        {
            var token = await GetToken();

            var accountHolderRequest = new AccountHolderRequest
            {
                SubscriptionKey = disbursementConfig.SubscriptionKey,
                AccountHolderId = accountHolderId,
                AccountHolderIdType = accountHolderIdType,
                RequestUri = DisbursementsRequestUri.AccountHolder,
                Token = token?.AccessToken
            };

            return await accountHolderClient.AccountHolder(accountHolderRequest);
        }
    }
}
