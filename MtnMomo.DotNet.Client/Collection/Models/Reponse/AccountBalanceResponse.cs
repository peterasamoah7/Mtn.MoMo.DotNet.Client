using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MtnMomo.DotNet.Client.Collection.Models.Reponse
{
    public class AccountBalanceResponse
    {
        [JsonProperty("availableBalance")]
        public string AvailableBalance { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }
    }
}
