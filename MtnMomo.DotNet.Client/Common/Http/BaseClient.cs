using MtnMomo.DotNet.Client.Common.Models.Response;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MtnMomo.DotNet.Client.Common.Http
{
    public class BaseClient : IBaseClient
    {
        /// <summary>
        /// HttpClient factory
        /// </summary>
        private readonly IHttpClientFactory httpClientFactory;

        public BaseClient(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// Generic GET request wrapper
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestUri"></param>
        /// <param name="clientName"></param>
        /// <returns></returns>
        public async Task<ClientResponse<T>> GetAsync<T>(string requestUri, string clientName, IEnumerable<KeyValuePair<string, string>> headers = null) where T : class
        {
            var request = GetRequestMessage(HttpMethod.Get, requestUri);

            var response = await SendAsync(clientName, request, headers);

            var data = await response.Content.ReadAsStringAsync();

            return new ClientResponse<T>
            {
                Data = response.IsSuccessStatusCode && !string.IsNullOrEmpty(data) ? Utils.Deserialize<T>(data) : null,
                StatusCode = response.StatusCode
            };
        }

        /// <summary>
        /// Generic POST request wrapper
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestUri"></param>
        /// <param name="clientName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<ClientResponse<T>> PostAsync<T>(string requestUri, string clientName, object value, IEnumerable<KeyValuePair<string, string>> headers = null) where T : class
        {
            var request = GetRequestMessage(HttpMethod.Post, requestUri, value);

            var response = await SendAsync(clientName, request, headers);

            var data = await response.Content.ReadAsStringAsync();

            return new ClientResponse<T>
            {
                Data = response.IsSuccessStatusCode && !string.IsNullOrEmpty(data) ? Utils.Deserialize<T>(data) : null,
                StatusCode = response.StatusCode
            };
        }

        /// <summary>
        /// Generic POST request wrapper
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="clientName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<ClientResponse> PostAsync(string requestUri, string clientName, object value, IEnumerable<KeyValuePair<string, string>> headers = null)
        {
            var request = GetRequestMessage(HttpMethod.Post, requestUri, value);

            var response = await SendAsync(clientName, request, headers);

            return new ClientResponse
            {
                StatusCode = response.StatusCode
            };
        }

        /// <summary>
        /// Generic GET request wrapper
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="clientName"></param>
        /// <returns></returns>
        public async Task<ClientResponse> GetAsync(string requestUri, string clientName, IEnumerable<KeyValuePair<string, string>> headers = null)
        {
            var request = GetRequestMessage(HttpMethod.Get, requestUri);

            var response = await SendAsync(clientName, request, headers);

            return new ClientResponse
            {
                StatusCode = response.StatusCode
            };
        }

        /// <summary>
        /// Get Request Message
        /// </summary>
        /// <param name="method"></param>
        /// <param name="value"></param>
        /// <param name="requestUri"></param>
        /// <returns></returns>
        private HttpRequestMessage GetRequestMessage(HttpMethod method, string requestUri, object value = null)
        {
            if (method == HttpMethod.Get)
            {
                return new HttpRequestMessage(HttpMethod.Get, requestUri);
            }

            var content = value != null ? new StringContent(Utils.Serialize(value), Encoding.UTF8, "application/json") : null;

            return new HttpRequestMessage(HttpMethod.Post, requestUri) { Content = content };
        }

        /// <summary>
        /// Send request
        /// </summary>
        /// <param name="clientName"></param>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        private async Task<HttpResponseMessage> SendAsync(string clientName, HttpRequestMessage requestMessage, IEnumerable<KeyValuePair<string, string>> headers = null)
        {
            var client = httpClientFactory.CreateClient(clientName);

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }

            return await client.SendAsync(requestMessage);
        }
    }
}
