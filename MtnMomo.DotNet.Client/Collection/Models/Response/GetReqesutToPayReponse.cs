using MtnMomo.DotNet.Client.Collection.Models.Shared;
using Newtonsoft.Json;

namespace MtnMomo.DotNet.Client.Collection.Models.Reponse
{
    /// <summary>
    /// Ref : https://momodeveloper.mtn.com/docs/services/collection/operations/requesttopay-referenceId-GET?
    /// </summary>
    public class GetReqesutToPayReponse
    {
        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("financialTransactionId")]
        public int FinancialTransactionId { get; set; }

        [JsonProperty("externalId")]
        public int ExternalId { get; set; }

        [JsonProperty("payer")]
        public Payer Payer { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
