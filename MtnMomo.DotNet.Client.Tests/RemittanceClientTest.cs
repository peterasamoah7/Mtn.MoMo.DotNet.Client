using MtnMomo.DotNet.Client.Common.Models;
using MtnMomo.DotNet.Client.Common.Models.Response;
using MtnMomo.DotNet.Client.Remittance.Client;
using NUnit.Framework;
using FluentAssertions;
using MtnMomo.DotNet.Client.Common;
using System.Net;

namespace MtnMomo.DotNet.Client.Tests
{
    [TestFixture]
    public class RemittanceClientTest
    {
        private IRemittanceClient remittanceClient;

        [SetUp]
        protected void SetUp()
        {
            remittanceClient = TestConfig.GetRemittanceClient(); 
        }

        [Test]
        public void Test_Mtn_Remittance_Api_Transfer()
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

            var postTransferResult = remittanceClient.PostTransfer(model).Result;
            postTransferResult.Data.Should().NotBeNull();
            postTransferResult.Status.Should().Be("Successful");
            postTransferResult.StatusCode.Should().Be(HttpStatusCode.Accepted);

            //get a transfer
            var getTranferResult = remittanceClient.GetTransfer(postTransferResult.Data).Result;
            getTranferResult.Data.Amount.Should().Be("20");
            getTranferResult.Data.Currency.Should().Be("EUR");
            getTranferResult.Status.Should().Be("Successful");
            getTranferResult.StatusCode.Should().Be(HttpStatusCode.OK);                   
        }

        [Test]
        public void Test_Mtn_Remittance_Api_AccountBalance()
        {
            //check account balance
            var accountBalanceResult = remittanceClient.AccountBalance().Result;

            if(accountBalanceResult.StatusCode == HttpStatusCode.OK)
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
        public void Test_Mtn_Remittance_Api_AccountHolder()
        {
            //check valid number
            var accountHolderResult = remittanceClient.AccountHolder(PartyIdType.MSISDN.ToString().ToLower(), "0557143133").Result;
            accountHolderResult.Status.Should().Be("Successful");
            accountHolderResult.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
