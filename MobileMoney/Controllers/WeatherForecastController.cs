using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MobileMoney.Models;
using MtnMomo.DotNet.Client.Collection.Client;
using MtnMomo.DotNet.Client.Common.Models;
using MtnMomo.DotNet.Client.Disbursements.Client;
using MtnMomo.DotNet.Client.Remittance.Client;

namespace MobileMoney.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ICollectionClient collectionClient;
        private readonly IDisbursementsClient disbursementsClient;
        private readonly IRemittanceClient remittanceClient;

        public WeatherForecastController(ICollectionClient collectionClient, IDisbursementsClient disbursementsClient, IRemittanceClient remittanceClient)
        {
            this.collectionClient = collectionClient;
            this.disbursementsClient = disbursementsClient;
            this.remittanceClient = remittanceClient; 
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //var collection = new Collection();

            var model = new TransferRequest
            {
                //Amount = "20",
                //Currency = "EUR",
                //ExternalId = "222",
                //Payee = new MtnMomo.DotNet.Client.Common.Models.Response.Payee
                //{
                //    PartyId = "0557143132",
                //    PartyIdType = MtnMomo.DotNet.Client.Common.PartyIdType.MSISDN
                //},
                //PayerMessage = "test",
                //PayeeNote = "test"
            };

            var response = await remittanceClient.PostTransfer(model);

            //var response = await collectionClient.AccountHolder(PartyIdType.MSISDN.ToString().ToLower(), "0545555555");

            //var response = await collectionClient.AccountBalance(); 

            //var response = await collectionClient.GetRequestToPay("c67c4c47-f896-48d5-aeb3-643e2564e283");

            return Ok(response);
        }
    }
}
