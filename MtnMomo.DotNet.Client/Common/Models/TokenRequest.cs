using System;
using System.Collections.Generic;
using System.Text;

namespace MtnMomo.DotNet.Client.Common.Models
{
    public class TokenRequest
    {
        public string RequestUri { get; set; }

        public string SubscriptionKey { get; set; }

        public string ApiKey { get; set; }

        public string UserId { get; set; }
    }
}
