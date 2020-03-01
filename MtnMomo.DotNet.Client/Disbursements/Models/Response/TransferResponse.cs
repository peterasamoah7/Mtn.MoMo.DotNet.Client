using MtnMomo.DotNet.Client.Common.Models.Response;
using MtnMomo.DotNet.Client.Disbursements.Models.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MtnMomo.DotNet.Client.Disbursements.Models.Response
{
    public class TransferResponse
    {
        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("financialTransactionId")]
        public int FinancialTransactionId { get; set; }

        [JsonProperty("externalId")]
        public int ExternalId { get; set; }

        [JsonProperty("payee")]
        public Payee Payee { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("reason")]
        public Reason Reason { get; set; }
    }
}
