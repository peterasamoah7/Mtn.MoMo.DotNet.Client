using MobileMoney.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MobileMoney.Services
{
    public class Collection
    {
        public Collection() { }

        /// <summary>
        /// Get auth token
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="apiKey"></param>
        /// <param name="subKey"></param>
        /// <returns></returns>
        private async Task<TokenModel> GetAuthToken(string userId, string apiKey, string subKey)
        {
            var auth = Utils.GetEncodedBasicAuth(userId, apiKey);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://sandbox.momodeveloper.mtn.com/");
                client.DefaultRequestHeaders.Add("Authorization", $"Basic {auth}");
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subKey);

                var response = await client.PostAsync("collection/token/", null);

                if(response.IsSuccessStatusCode)
                {
                    return Utils.Deserialize<TokenModel>(await response.Content.ReadAsStringAsync());
                }
            }

            return null; 
        }

        /// <summary>
        /// Request to pay Post
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> RequestToPay(PostReqesutToPayModel model) 
        {
            var config = new ServiceConfig
            {
                ApiKey = "c0b63594ff1d4676b25d08d77216a400",
                SubscriptionKey = "dcaf84d5179f455dbec7ba52601c62d2",
                UserId = "7706a623-7c1c-4d25-9cea-06cf06fbda51",
                Environment = "sandbox"
            };

            var token = await GetAuthToken(config.UserId, config.ApiKey, config.SubscriptionKey);

            var reference = Guid.NewGuid().ToString();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://sandbox.momodeveloper.mtn.com/");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.AccessToken}");
                client.DefaultRequestHeaders.Add("X-Reference-Id", reference);
                client.DefaultRequestHeaders.Add("X-Target-Environment", config.Environment);
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", config.SubscriptionKey);

                var request = new StringContent(Utils.Serialize(model), Encoding.UTF8, "application/json");

                var response = await client.PostAsync("collection/v1_0/requesttopay", request);

                if (response.IsSuccessStatusCode)
                {
                    return true; 
                }

            }

            return false; 
        }

        /// <summary>
        /// Request to pay Get
        /// </summary>
        /// <param name="referenceId"></param>
        /// <returns></returns>
        public async Task<GetReqesutToPayReponse> RequestToPay(string referenceId)
        {
            var config = new ServiceConfig
            {
                ApiKey = "c0b63594ff1d4676b25d08d77216a400",
                SubscriptionKey = "dcaf84d5179f455dbec7ba52601c62d2",
                UserId = "7706a623-7c1c-4d25-9cea-06cf06fbda51",
                Environment = "sandbox"
            };

            var token = await GetAuthToken(config.UserId, config.ApiKey, config.SubscriptionKey);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://sandbox.momodeveloper.mtn.com/");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.AccessToken}");
                client.DefaultRequestHeaders.Add("X-Target-Environment", config.Environment);
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", config.SubscriptionKey);

                var response = await client.GetAsync($"collection/v1_0/requesttopay/{referenceId}");

                if (response.IsSuccessStatusCode)
                {
                    return Utils.Deserialize<GetReqesutToPayReponse>(await response.Content.ReadAsStringAsync());
                }

            }

            return null;
        }

        /// <summary>
        /// Get Account Balance
        /// </summary>
        /// <returns></returns>
        public async Task<AccountBalance> AccountBalance()
        {
            var config = new ServiceConfig
            {
                ApiKey = "c0b63594ff1d4676b25d08d77216a400",
                SubscriptionKey = "dcaf84d5179f455dbec7ba52601c62d2",
                UserId = "7706a623-7c1c-4d25-9cea-06cf06fbda51",
                Environment = "sandbox"
            };

            var token = await GetAuthToken(config.UserId, config.ApiKey, config.SubscriptionKey);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://sandbox.momodeveloper.mtn.com/");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.AccessToken}");
                client.DefaultRequestHeaders.Add("X-Target-Environment", config.Environment);
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", config.SubscriptionKey);

                var response = await client.GetAsync($"collection/v1_0/account/balance");

                if (response.IsSuccessStatusCode)
                {
                    return Utils.Deserialize<AccountBalance>(await response.Content.ReadAsStringAsync());
                }

            }

            return null;
        }

        /// <summary>
        /// Account Holder Check
        /// </summary>
        /// <param name="accountHolderIdType"></param>
        /// <param name="accountHolderId"></param>
        /// <returns></returns>
        public async Task<bool> AccountHolder(string accountHolderIdType, string accountHolderId)
        {
            var config = new ServiceConfig
            {
                ApiKey = "c0b63594ff1d4676b25d08d77216a400",
                SubscriptionKey = "dcaf84d5179f455dbec7ba52601c62d2",
                UserId = "7706a623-7c1c-4d25-9cea-06cf06fbda51",
                Environment = "sandbox"
            };

            var token = await GetAuthToken(config.UserId, config.ApiKey, config.SubscriptionKey);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://sandbox.momodeveloper.mtn.com/");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.AccessToken}");
                client.DefaultRequestHeaders.Add("X-Target-Environment", config.Environment);
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", config.SubscriptionKey);

                var response = await client.GetAsync($"collection/v1_0/accountholder/{accountHolderIdType}/{accountHolderId}/active");

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

            }

            return false;
        }
    }
}
