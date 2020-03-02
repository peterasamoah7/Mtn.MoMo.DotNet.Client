using FluentAssertions;
using MtnMomo.DotNet.Client.Common;
using MtnMomo.DotNet.Client.Common.Models;
using MtnMomo.DotNet.Client.Common.Models.Response;
using MtnMomo.DotNet.Client.Disbursements.Client;
using NUnit.Framework;
using System.Net;

namespace MtnMomo.DotNet.Client.Tests
{
    [TestFixture]
    public class DisbursementsClientTest
    {
        private IDisbursementsClient disbursementsClient;

        [SetUp]
        protected void SetUp()
        {
            disbursementsClient = TestConfig.GetDisbursementsClient();
        }

        [Test]
        public void Test_Mtn_Disbursements_Api_Transfer()
        {
            //make a transfer
            var model = new TransferRequest
            {
                Amount = "20",
                Currency = "EUR",
                ExternalId = "222",
                Payee = new Payee
                {
                    PartyId = "0557143133",
                    PartyIdType = PartyIdType.MSISDN
                },
                PayerMessage = "test",
                PayeeNote = "test"
            };

            var postTransferResult = disbursementsClient.PostTransfer(model).Result;
            postTransferResult.Data.Should().NotBeNull();
            postTransferResult.Status.Should().Be("Successful");
            postTransferResult.StatusCode.Should().Be(HttpStatusCode.Accepted);

            //get a transfer
            var getTranferResult = disbursementsClient.GetTransfer(postTransferResult.Data).Result;
            getTranferResult.Data.Amount.Should().Be("20");
            getTranferResult.Data.Currency.Should().Be("EUR");
            getTranferResult.Status.Should().Be("Successful");
            getTranferResult.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void Test_Mtn_Disbursements_Api_AccountBalance()
        {
            //check account balance
            var accountBalanceResult = disbursementsClient.AccountBalance().Result;

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
        public void Test_Mtn_Disbursements_Api_AccountHolder()
        {
            //check valid number
            var accountHolderResult = disbursementsClient.AccountHolder(PartyIdType.MSISDN.ToString().ToLower(), "0557143133").Result;
            accountHolderResult.Status.Should().Be("Successful");
            accountHolderResult.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
