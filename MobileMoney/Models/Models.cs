using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileMoney.Models
{
    public class PostReqesutToPayModel 
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

    public class Payer
    {
        [JsonProperty("partyIdType")]
        public PartyIdType PartyIdType { get; set; }

        [JsonProperty("partyId")]
        public string PartyId { get; set; }
    }

    public enum PartyIdType
    {
        MSISDN,
        EMAIL,
        PARTY_CODE
    }

    public class ServiceConfig
    {
        public string SubscriptionKey { get; set; }
        public string UserId { get; set; }
        public string ApiKey { get; set; }
        public string Environment { get; set; }
    }

    public class AccountBalance
    {
        [JsonProperty("availableBalance")]
        public string AvailableBalance { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }
    }
}
