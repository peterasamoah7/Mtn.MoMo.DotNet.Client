using MtnMomo.DotNet.Client.Common;
using MtnMomo.DotNet.Client.Common.Client.Interfaces;
using MtnMomo.DotNet.Client.Common.Http;
using MtnMomo.DotNet.Client.Common.Models;
using MtnMomo.DotNet.Client.Common.Models.Request;
using MtnMomo.DotNet.Client.Common.Models.Response;
using MtnMomo.DotNet.Client.Disbursements.Models;
using MtnMomo.DotNet.Client.Disbursements.Models.Config;
using MtnMomo.DotNet.Client.Disbursements.Models.Request;
using MtnMomo.DotNet.Client.Disbursements.Models.Response;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MtnMomo.DotNet.Client.Disbursements.Client
{
    public class DisbursementsClient : IDisbursementsClient
    {
        private readonly IBaseClient baseClient;
        private readonly ITokenClient tokenClient;
        private readonly IAccountBalanceClient accountBalanceClient;
        private readonly IAccountHolderClient accountHolderClient;
        private readonly DisbursementsConfig disbursementConfig;
         
        public DisbursementsClient(
            IBaseClient baseClient,
            DisbursementsConfig disbursementConfig,
            ITokenClient tokenClient,
            IAccountBalanceClient accountBalanceClient,
            IAccountHolderClient accountHolderClient)
        {
            this.baseClient = baseClient;
            this.disbursementConfig = disbursementConfig;
            this.tokenClient = tokenClient;
            this.accountBalanceClient = accountBalanceClient;
            this.accountHolderClient = accountHolderClient;
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

            var paymentReference = Guid.NewGuid().ToString();

            var headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>(Constants.PaymentReferenceHeader, $"{paymentReference}"),
                new KeyValuePair<string, string>(Constants.AuthHeader, $"Bearer {token.AccessToken}"),
                new KeyValuePair<string, string>(Constants.SubKeyHeader, disbursementConfig.SubscriptionKey)
            };

            if(!string.IsNullOrEmpty(callbackUrl))
            {
                headers.Add(new KeyValuePair<string, string>(Constants.CallbackUrlHeader, callbackUrl));
            }

            var response = await baseClient.PostAsync<string>(DisbursementsRequestUri.Transfer, Constants.MtnClient, request, headers);

            response.Data = response.StatusCode == HttpStatusCode.Accepted ? paymentReference : null;
            response.Status = response.StatusCode == HttpStatusCode.Accepted ? Status.Successful.ToString() : Status.Failed.ToString();

            return response;
        }

        /// <summary>
        /// Get a transfer
        /// </summary>
        /// <param name="referenceId"></param>
        /// <returns></returns>
        public async Task<ClientResponse<TransferResponse>> GetTransfer(string referenceId)
        {
            var token = await GetToken();

            var headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>(Constants.SubKeyHeader, disbursementConfig.SubscriptionKey),
                new KeyValuePair<string, string>(Constants.AuthHeader, $"Bearer {token.AccessToken}"),
            };

            var response = await baseClient.GetAsync<TransferResponse>($"{DisbursementsRequestUri.Transfer}/{referenceId}", Constants.MtnClient, headers);
            response.Status = response.StatusCode == HttpStatusCode.OK ? Status.Successful.ToString() : Status.Failed.ToString();

            return response;
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
                Token = token.AccessToken
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
                Token = token.AccessToken
            };

            return await accountHolderClient.AccountHolder(accountHolderRequest);
        }
    }
}
