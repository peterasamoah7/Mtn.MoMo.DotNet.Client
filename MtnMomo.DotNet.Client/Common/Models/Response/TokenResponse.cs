using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MtnMomo.DotNet.Client.Common.Models.Response
{
    /// <summary>
    /// Ref : https://momodeveloper.mtn.com/docs/services/collection/operations/token-POST?
    /// </summary>
    public class TokenResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public int ExpireIn { get; set; }
    }
}
