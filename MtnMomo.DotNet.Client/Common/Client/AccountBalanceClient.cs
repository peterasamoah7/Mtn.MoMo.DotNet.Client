using MtnMomo.DotNet.Client.Common.Client.Interfaces;
using MtnMomo.DotNet.Client.Common.Http;
using MtnMomo.DotNet.Client.Common.Models;
using MtnMomo.DotNet.Client.Common.Models.Request;
using MtnMomo.DotNet.Client.Common.Models.Response;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace MtnMomo.DotNet.Client.Common.Client
{
    public class AccountBalanceClient : IAccountBalanceClient
    {
        private readonly IBaseClient baseClient;

        public AccountBalanceClient(IBaseClient baseClient)
        {
            this.baseClient = baseClient;
        }

        /// <summary>
        /// Get account balance for Product
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ClientResponse<AccountBalanceResponse>> AccountBalance(AccountBalanceRequest request)
        {
            if (string.IsNullOrEmpty(request.Token))
            {
                return new ClientResponse<AccountBalanceResponse> { Status = Status.Failed.ToString(), StatusCode = HttpStatusCode.Unauthorized };
            }

            var headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>(Constants.SubKeyHeader, request.SubscriptionKey),
                new KeyValuePair<string, string>(Constants.AuthHeader, $"Bearer {request.Token}"),
            };

            var response = await baseClient.GetAsync<AccountBalanceResponse>(request.RequestUri, Constants.MtnClient, headers);
            response.Status = response.StatusCode == HttpStatusCode.OK ? Status.Successful.ToString() : Status.Failed.ToString();

            return response;
        }
    }
}
