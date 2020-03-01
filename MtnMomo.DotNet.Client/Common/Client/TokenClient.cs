using MtnMomo.DotNet.Client.Common.Http;
using MtnMomo.DotNet.Client.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MtnMomo.DotNet.Client.Common.Client
{
    public class TokenClient : ITokenClient
    {
        private readonly IBaseClient baseClient; 

        public TokenClient(IBaseClient baseClient)
        {
            this.baseClient = baseClient;
        }

        /// <summary>
        /// Get an auth token
        /// </summary>
        /// <param name="requestUri"></param>
        /// <returns></returns>
        public async Task<ClientResponse<TokenResponse>> GetToken(TokenRequest tokenRequest)
        {
            var headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>(Constants.AuthHeader, $"Basic {Utils.GetEncodedBasicAuth(tokenRequest.UserId, tokenRequest.ApiKey)}"),
                new KeyValuePair<string, string>(Constants.SubKeyHeader, tokenRequest.SubscriptionKey)
            };

            var response = await baseClient.PostAsync<TokenResponse>(tokenRequest.RequestUri, Constants.MtnClient, null, headers); 

            return response; 
        }
    }
}
