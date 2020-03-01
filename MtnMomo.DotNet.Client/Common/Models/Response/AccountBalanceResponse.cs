using Newtonsoft.Json;

namespace MtnMomo.DotNet.Client.Common.Models.Response
{
    public class AccountBalanceResponse
    {
        [JsonProperty("availableBalance")]
        public string AvailableBalance { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }
    }
}
