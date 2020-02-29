namespace MtnMomo.DotNet.Client.Collection.Models.Config
{
    /// <summary>
    /// Ref : https://momodeveloper.mtn.com/api-documentation/api-description/
    /// </summary>
    public class CollectionConfig
    {
        public string SubscriptionKey { get; set; }
        public string UserId { get; set; }
        public string ApiKey { get; set; }
        public string Environment { get; set; }
        public bool Sandbox { get; set; } 
    }
}
