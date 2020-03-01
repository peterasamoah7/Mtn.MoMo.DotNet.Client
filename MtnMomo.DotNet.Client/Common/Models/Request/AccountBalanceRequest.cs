using System;
using System.Collections.Generic;
using System.Text;

namespace MtnMomo.DotNet.Client.Common.Models.Request
{
    public class AccountBalanceRequest
    {
        public string SubscriptionKey { get; set; }

        public string Token { get; set; }

        public string RequestUri { get; set; }
    }
}
