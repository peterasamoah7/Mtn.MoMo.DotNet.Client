namespace MtnMomo.DotNet.Client.Remittance.Models.Config
{
    public class RemittanceConfig
    {
        public string SubscriptionKey { get; set; }
        public string UserId { get; set; }
        public string ApiKey { get; set; }
        public string Environment { get; set; }
        public bool Sandbox { get; set; }
        public string BaseUrl { get; set; }
    }
}
