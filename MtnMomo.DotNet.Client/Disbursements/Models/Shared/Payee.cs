using MtnMomo.DotNet.Client.Common;
using Newtonsoft.Json;

namespace MtnMomo.DotNet.Client.Disbursements.Models.Shared
{
    public class Payee
    {
        [JsonProperty("partyIdType")]
        public PartyIdType PartyIdType { get; set; }

        [JsonProperty("partyId")]
        public string PartyId { get; set; }
    }
}
