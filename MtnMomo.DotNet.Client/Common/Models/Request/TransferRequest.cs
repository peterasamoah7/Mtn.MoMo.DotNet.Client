using MtnMomo.DotNet.Client.Common.Models.Response;
using Newtonsoft.Json;

namespace MtnMomo.DotNet.Client.Common.Models
{
    public class TransferRequest
    {
        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("externalId")]
        public string ExternalId { get; set; }

        [JsonProperty("payee")]
        public Payee Payee { get; set; }

        [JsonProperty("payerMessage")]
        public string PayerMessage { get; set; }

        [JsonProperty("payeeNote")]
        public string PayeeNote { get; set; }
    }
}
