using FluentAssertions;
using MtnMomo.DotNet.Client.Collection.Client;
using MtnMomo.DotNet.Client.Collection.Models.Request;
using MtnMomo.DotNet.Client.Collection.Models.Shared;
using MtnMomo.DotNet.Client.Common;
using NUnit.Framework;
using System.Net;

namespace MtnMomo.DotNet.Client.Tests
{
    [TestFixture]
    public class CollectionClientTest
    {
        private ICollectionClient collectionClient;

        [SetUp]
        protected void SetUp()
        {
            collectionClient = TestConfig.GetCollectionClient();
        }

        [Test]
        public void Test_Mtn_Collection_Api_RequestToPay()
        {
            //make a transfer
            var model = new PostReqesutToPayRequest
            {
                Amount = "20",
                Currency = "EUR",
                ExternalId = "222",
                Payer = new Payer
                {
                    PartyId = "0557143133",
                    PartyIdType = PartyIdType.MSISDN
                },
                PayerMessage = "test",
                PayeeNote = "test"
            };

            var postrequestResult = collectionClient.PostRequestToPay(model).Result;
            postrequestResult.Data.Should().NotBeNull();
            postrequestResult.Status.Should().Be("Successful");
            postrequestResult.StatusCode.Should().Be(HttpStatusCode.Accepted);

            //get a transfer
            var getrequestResult = collectionClient.GetRequestToPay(postrequestResult.Data).Result;
            getrequestResult.Data.Amount.Should().Be("20");
            getrequestResult.Data.Currency.Should().Be("EUR");
            getrequestResult.Status.Should().Be("Successful");
            getrequestResult.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void Test_Mtn_Collection_Api_AccountBalance()
        {
            //check account balance
            var accountBalanceResult = collectionClient.AccountBalance().Result;

            if (accountBalanceResult.StatusCode == HttpStatusCode.OK)
            {
                accountBalanceResult.Status.Should().Be("Successful");
                accountBalanceResult.Data.Currency.Should().NotBeNull();
                accountBalanceResult.Data.AvailableBalance.Should().NotBeNull();
            }
            else
            {
                accountBalanceResult.Status.Should().Be("Failed");
            }
        }

        [Test]
        public void Test_Mtn_Collection_Api_AccountHolder()
        {
            //check valid number
            var accountHolderResult = collectionClient.AccountHolder(PartyIdType.MSISDN.ToString().ToLower(), "0557143133").Result;
            accountHolderResult.Status.Should().Be("Successful");
            accountHolderResult.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
