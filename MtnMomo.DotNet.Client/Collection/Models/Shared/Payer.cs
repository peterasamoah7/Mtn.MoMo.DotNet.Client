using Newtonsoft.Json;

namespace MtnMomo.DotNet.Client.Collection.Models.Shared
{
    public class Payer
    {
        [JsonProperty("partyIdType")]
        public PartyIdType PartyIdType { get; set; }

        [JsonProperty("partyId")]
        public string PartyId { get; set; }
    }
}
