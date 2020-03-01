using Newtonsoft.Json;

namespace MtnMomo.DotNet.Client.Common.Models.Response
{
    public class Reason
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
