using MtnMomo.DotNet.Client.Collection.Models;
using MtnMomo.DotNet.Client.Collection.Models.Config;
using MtnMomo.DotNet.Client.Collection.Models.Reponse;
using MtnMomo.DotNet.Client.Collection.Models.Request;
using MtnMomo.DotNet.Client.Common;
using MtnMomo.DotNet.Client.Common.Client.Interfaces;
using MtnMomo.DotNet.Client.Common.Http;
using MtnMomo.DotNet.Client.Common.Models;
using MtnMomo.DotNet.Client.Common.Models.Request;
using MtnMomo.DotNet.Client.Common.Models.Response;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace MtnMomo.DotNet.Client.Collection.Client
{
    public class CollectionClient : ICollectionClient
    {
        private readonly IBaseClient baseClient;
        private readonly ITokenClient tokenClient;
        private readonly IAccountBalanceClient accountBalanceClient;
        private readonly IAccountHolderClient accountHolderClient;
        private readonly CollectionConfig collectionConfig;

        public CollectionClient(
            IBaseClient baseClient,
            CollectionConfig collectionConfig,
            ITokenClient tokenClient,
            IAccountBalanceClient accountBalanceClient,
            IAccountHolderClient accountHolderClient)
        {
            this.baseClient = baseClient;
            this.collectionConfig = collectionConfig;
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
                RequestUri = CollectionRequestUri.Token,
                ApiKey = collectionConfig.ApiKey,
                SubscriptionKey = collectionConfig.SubscriptionKey,
                UserId = collectionConfig.UserId
            };

            var token = await tokenClient.GetToken(tokenRequest);

            return token.Data;
        }

        /// <summary>
        /// Post a request to pay
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ClientResponse<string>> PostRequestToPay(PostReqesutToPayRequest request, string callbackUrl = null)
        {
            var token = await GetToken();

            if(string.IsNullOrEmpty(token?.AccessToken))
            {
                return new ClientResponse<string> { Status = Status.Failed.ToString(), StatusCode = HttpStatusCode.Unauthorized };
            }

            var paymentReference = Guid.NewGuid().ToString();

            var headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>(Constants.PaymentReferenceHeader, $"{paymentReference}"),
                new KeyValuePair<string, string>(Constants.AuthHeader, $"Bearer {token.AccessToken}"),
                new KeyValuePair<string, string>(Constants.SubKeyHeader, collectionConfig.SubscriptionKey)
            };

            if (!string.IsNullOrEmpty(callbackUrl))
            {
                headers.Add(new KeyValuePair<string, string>(Constants.CallbackUrlHeader, callbackUrl));
            }

            var response = await baseClient.PostAsync<string>(CollectionRequestUri.RequestToPay, Constants.MtnClient, request, headers);

            response.Data = response.StatusCode == HttpStatusCode.Accepted ? paymentReference : null;
            response.Status = response.StatusCode == HttpStatusCode.Accepted ? Status.Successful.ToString() : Status.Failed.ToString();

            return response;
        }

        /// <summary>
        /// Get status for reqyes to pay
        /// </summary>
        /// <param name="referenceId"></param>
        /// <returns></returns>
        public async Task<ClientResponse<GetReqesutToPayReponse>> GetRequestToPay(string referenceId)
        {
            var token = await GetToken();

            if (string.IsNullOrEmpty(token?.AccessToken))
            {
                return new ClientResponse<GetReqesutToPayReponse> { Status = Status.Failed.ToString(), StatusCode = HttpStatusCode.Unauthorized };
            }

            var headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>(Constants.SubKeyHeader, collectionConfig.SubscriptionKey),
                new KeyValuePair<string, string>(Constants.AuthHeader, $"Bearer {token.AccessToken}"),
            };

            var response = await baseClient.GetAsync<GetReqesutToPayReponse>($"{CollectionRequestUri.RequestToPay}/{referenceId}", Constants.MtnClient, headers);
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
                SubscriptionKey = collectionConfig.SubscriptionKey,
                RequestUri = CollectionRequestUri.AccountBalance,
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
                SubscriptionKey = collectionConfig.SubscriptionKey,
                AccountHolderId = accountHolderId,
                AccountHolderIdType = accountHolderIdType,
                RequestUri = CollectionRequestUri.AccountHolder,
                Token = token?.AccessToken
            };

            return await accountHolderClient.AccountHolder(accountHolderRequest);        
        }

    }
}
