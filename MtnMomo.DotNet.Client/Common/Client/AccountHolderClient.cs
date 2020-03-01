using MtnMomo.DotNet.Client.Common.Client.Interfaces;
using MtnMomo.DotNet.Client.Common.Http;
using MtnMomo.DotNet.Client.Common.Models.Request;
using MtnMomo.DotNet.Client.Common.Models.Response;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace MtnMomo.DotNet.Client.Common.Client
{
    public class AccountHolderClient : IAccountHolderClient
    {
        private readonly IBaseClient baseClient;

        public AccountHolderClient(IBaseClient baseClient)
        {
            this.baseClient = baseClient;
        }

        /// <summary>
        /// Validate account holder for a Product
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ClientResponse> AccountHolder(AccountHolderRequest request)
        {
            var headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>(Constants.SubKeyHeader, request.SubscriptionKey),
                new KeyValuePair<string, string>(Constants.AuthHeader, $"Bearer {request.Token}"),
            };

            var response = await baseClient.GetAsync($"{request.RequestUri}/{request.AccountHolderIdType}/{request.AccountHolderId}/active", Constants.MtnClient, headers);
            response.Status = response.StatusCode == HttpStatusCode.OK ? Status.Successful.ToString() : Status.Failed.ToString();

            return response;
        }
    }
}
