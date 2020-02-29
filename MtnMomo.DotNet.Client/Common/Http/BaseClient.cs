using Newtonsoft.Json;
using System;
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
        public async Task<T> GetAsync<T>(string requestUri, string clientName) where T : class
        {
            var request = GetRequestMessage(HttpMethod.Get, requestUri);

            var response = await SendAsync(clientName, request);

            return response.IsSuccessStatusCode ? JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync()) : null;
        }

        /// <summary>
        /// Generic POST request wrapper
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestUri"></param>
        /// <param name="clientName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<T> PostAsync<T>(string requestUri, string clientName, object value) where T : class
        {
            var request = GetRequestMessage(HttpMethod.Post, requestUri, value);

            var response = await SendAsync(clientName, request);

            return response.IsSuccessStatusCode ? JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync()) : null;
        }

        /// <summary>
        /// Generic POST request wrapper
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="clientName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PostAsync(string requestUri, string clientName, object value)
        {
            var request = GetRequestMessage(HttpMethod.Post, requestUri, value);

            return await SendAsync(clientName, request);
        }

        /// <summary>
        /// Generic GET request wrapper
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="clientName"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> GetAsync(string requestUri, string clientName)
        {
            var request = GetRequestMessage(HttpMethod.Get, requestUri);

            return await SendAsync(clientName, request);
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
            if(method == HttpMethod.Get)
            {
                return new HttpRequestMessage(HttpMethod.Get, requestUri);
            }

            var content = value != null  ? new StringContent(Utils.Serialize(value), Encoding.UTF8, "application/json") : null;

            return new HttpRequestMessage(HttpMethod.Post, requestUri) { Content = content };
        }

        /// <summary>
        /// Send request
        /// </summary>
        /// <param name="clientName"></param>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        private async Task<HttpResponseMessage> SendAsync(string clientName, HttpRequestMessage requestMessage)
        {
            var client = httpClientFactory.CreateClient(clientName);

            return await client.SendAsync(requestMessage);
        }
    }
}
