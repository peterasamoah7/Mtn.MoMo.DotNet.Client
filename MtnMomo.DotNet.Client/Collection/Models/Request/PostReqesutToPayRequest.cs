using MtnMomo.DotNet.Client.Collection.Models.Shared;
using Newtonsoft.Json;

namespace MtnMomo.DotNet.Client.Collection.Models.Request
{
    /// <summary>
    /// Ref : https://momodeveloper.mtn.com/docs/services/collection/operations/requesttopay-POST?
    /// </summary>
    public class PostReqesutToPayRequest
    {
        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("externalId")]
        public string ExternalId { get; set; }

        [JsonProperty("payer")]
        public Payer Payer { get; set; }

        [JsonProperty("payerMessage")]
        public string PayerMessage { get; set; }

        [JsonProperty("payeeNote")]
        public string PayeeNote { get; set; }
    }
}
