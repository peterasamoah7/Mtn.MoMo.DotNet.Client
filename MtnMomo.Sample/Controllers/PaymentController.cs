using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MtnMomo.DotNet.Client.Collection.Client;
using MtnMomo.DotNet.Client.Collection.Models.Request;
using MtnMomo.DotNet.Client.Collection.Models.Shared;
using MtnMomo.DotNet.Client.Common;
using MtnMomo.DotNet.Client.Common.Models;
using MtnMomo.DotNet.Client.Common.Models.Response;
using MtnMomo.DotNet.Client.Disbursements.Client;
using MtnMomo.DotNet.Client.Remittance.Client;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MtnMomo.Sample.Controllers
{
    [Route("api/[controller]/[action]")]
    public class PaymentController : Controller
    {
        private readonly ICollectionClient collectionClient;
        //private readonly IDisbursementsClient disbursementClient;
        //private readonly IRemittanceClient remittanceClient; 

        public PaymentController(
            ICollectionClient collectionClient
            //IDisbursementsClient disbursementClient,
            //IRemittanceClient remittanceClient
            )
        {
            this.collectionClient = collectionClient;
            //this.disbursementClient = disbursementClient;
            //this.remittanceClient = remittanceClient;
        }

        /// <summary>
        /// Sent a payment
        /// Get payment status
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Collection()
        {
            /**
             * Sample test data when Collection client used
             */

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

            var postrequestResult = await collectionClient.PostRequestToPay(model);

            var getrequestResult = await collectionClient.GetRequestToPay(postrequestResult.Data);

            return Ok(new { post = postrequestResult, get = getrequestResult }); 
        }

        /// <summary>
        /// Create a disbursement 
        /// Get a disbursement
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Disbursement()
        {
            /**
             * Sample test data when Disbursement client used
             */

            //var model = new TransferRequest
            //{
            //    Amount = "20",
            //    Currency = "EUR",
            //    ExternalId = "222",
            //    Payee = new Payee
            //    {
            //        PartyId = "0557143134",
            //        PartyIdType = PartyIdType.MSISDN
            //    },
            //    PayerMessage = "test",
            //    PayeeNote = "test"
            //};

            //var postTransferResult = await disbursementClient.PostTransfer(model);

            //var getTranferResult = await disbursementClient.GetTransfer(postTransferResult.Data);

            //return Ok(new { post = postTransferResult, get = getTranferResult }); 

            return Ok(); 
        }

        /// <summary>
        /// Send a remittance transfer
        /// Get a remittance transfer
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Remittance()
        {
            /**
             * Sample test data when Remittance client used
             */

            //var model = new TransferRequest
            //{
            //    Amount = "20",
            //    Currency = "EUR",
            //    ExternalId = "222",
            //    Payee = new Payee
            //    {
            //        PartyId = "0557143134",
            //        PartyIdType = PartyIdType.MSISDN
            //    },
            //    PayerMessage = "test",
            //    PayeeNote = "test"
            //};

            //var postTransferResult = await remittanceClient.PostTransfer(model);

            //var getTranferResult = await remittanceClient.GetTransfer(postTransferResult.Data);

            //return Ok(new { post = postTransferResult, get = getTranferResult });

            return Ok();
        }
    }
}
