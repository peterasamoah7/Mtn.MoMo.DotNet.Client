using MtnMomo.DotNet.Client.Common.Client.Interfaces;
using MtnMomo.DotNet.Client.Common.Config;
using MtnMomo.DotNet.Client.Common.Http;
using MtnMomo.DotNet.Client.Common.Models;
using MtnMomo.DotNet.Client.Common.Models.Response;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace MtnMomo.DotNet.Client.Common.Client
{
    public class TransferClient : ITransferClient
    {
        private readonly IBaseClient baseClient;

        public TransferClient(IBaseClient baseClient)
        {
            this.baseClient = baseClient;
        }

        /// <summary>
        /// Make a transfer
        /// </summary>
        /// <param name="request"></param>
        /// <param name="config"></param>
        /// <param name="callbackUrl"></param>
        /// <returns></returns>
        public async Task<ClientResponse<string>> PostTransfer(TransferRequest request, TransferConfig config, string callbackUrl = null)
        {
            if (string.IsNullOrEmpty(config.Token))
            {
                return new ClientResponse<string> { Status = Status.Failed.ToString(), StatusCode = HttpStatusCode.Unauthorized };
            }

            var paymentReference = Guid.NewGuid().ToString();

            var headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>(Constants.PaymentReferenceHeader, $"{paymentReference}"),
                new KeyValuePair<string, string>(Constants.AuthHeader, $"Bearer {config.Token}"),
                new KeyValuePair<string, string>(Constants.SubKeyHeader, config.SubscriptionKey)
            };

            if (!string.IsNullOrEmpty(callbackUrl))
            {
                headers.Add(new KeyValuePair<string, string>(Constants.CallbackUrlHeader, callbackUrl));
            }

            var response = await baseClient.PostAsync<string>(config.RequestUri, Constants.MtnClient, request, headers);

            response.Data = response.StatusCode == HttpStatusCode.Accepted ? paymentReference : null;
            response.Status = response.StatusCode == HttpStatusCode.Accepted ? Status.Successful.ToString() : Status.Failed.ToString();

            return response;
        }

        /// <summary>
        /// Get a transfer
        /// </summary>
        /// <param name="referenceId"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public async Task<ClientResponse<TransferResponse>> GetTransfer(string referenceId, TransferConfig config)
        {
            if (string.IsNullOrEmpty(config.Token))
            {
                return new ClientResponse<TransferResponse> { Status = Status.Failed.ToString(), StatusCode = HttpStatusCode.Unauthorized };
            }

            var headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>(Constants.SubKeyHeader, config.SubscriptionKey),
                new KeyValuePair<string, string>(Constants.AuthHeader, $"Bearer {config.Token}"),
            };

            var response = await baseClient.GetAsync<TransferResponse>($"{config.RequestUri}/{referenceId}", Constants.MtnClient, headers);
            response.Status = response.StatusCode == HttpStatusCode.OK ? Status.Successful.ToString() : Status.Failed.ToString();

            return response;
        }
    }
}
