using Newtonsoft.Json;

namespace MtnMomo.DotNet.Client.Common.Models.Response
{
    public class Payee
    {
        [JsonProperty("partyIdType")]
        public PartyIdType PartyIdType { get; set; }

        [JsonProperty("partyId")]
        public string PartyId { get; set; }
    }
}
