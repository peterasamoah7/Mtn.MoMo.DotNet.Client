using System;
using System.Collections.Generic;
using System.Text;

namespace MtnMomo.DotNet.Client.Disbursements.Models.Config
{
    public class DisbursementsConfig
    {
        public string SubscriptionKey { get; set; }
        public string UserId { get; set; }
        public string ApiKey { get; set; }
        public string Environment { get; set; }
        public bool Sandbox { get; set; }
    }
}
