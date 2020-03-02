namespace MtnMomo.DotNet.Client.Common.Models.Request
{
    public class AccountHolderRequest
    {
        public string SubscriptionKey { get; set; }

        public string Token { get; set; }

        public string RequestUri { get; set; }

        public string AccountHolderIdType { get; set; }

        public string AccountHolderId { get; set; }
    }
}
