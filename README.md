# Mtn Mobile Money DotNet Client

A client library for using the Mtn Mobile Money API : https://momodeveloper.mtn.com/

## Getting Started

```
Install-Package MtnMobileMoney.DotNet.Client -Version 1.0.0
```

### Usage

Set up client library using depency injection
The client Mtn Mobile Money Collection, Disbusrements and Remittance APIs

Register only Collection CLient
```
var config = new CollectionConfig
{
    ApiKey = "ad8a5dbe6a754a108d0b122bfe8c26bb",
    SubscriptionKey = "4ae1cbdd3f744fc283b9a3339a444003",
    UserId = "1bfc600a-a6e1-4c1b-ba66-5fee8038c056",
    Environment = "sandbox",
    Sandbox = false, //no need to provide base url if sandbox only set this to true, this will default to sandbox url
    BaseUrl = "<your_production_url>"
};

services.AddMtnCollectionClient(config);

```
Register all clients. Client configuration is required for each client.

```
var cconfig = new CollectionConfig
{
    ApiKey = "e531c7f22df84e91844cbf849970566d",
    SubscriptionKey = "4e0dda8bfe554146aae09d5ab8b112a8",
    UserId = "9d5a2b6b-a164-443c-ac22-27404d86a2f4",
    Environment = "sandbox",
    Sandbox = true, //no need to provide base url if sandbox only set this to true, this will default to sandbox url
    BaseUrl = "<your_production_url>"
};

//disbursement client config
var dconfig = new DisbursementsConfig
{
    ApiKey = "0ef2baa628e04a419ee7d4bf17839c20",
    SubscriptionKey = "f1d94c7aabd64fc0af6bfd0a2353c020",
    UserId = "1bfc600a-a6e1-4c1b-ba66-5fee8038c056",
    Environment = "sandbox",
    Sandbox = true, //no need to provide base url if sandbox only set this to true, this will default to sandbox url
    BaseUrl = "<your_production_url>"
};

//remittace client config
var rconfig = new RemittanceConfig
{
    ApiKey = "a4b41504bff44def992f439fa97a6e41",
    SubscriptionKey = "64fe810144b240298a83bc73ddf85d67",
    UserId = "1bfc600a-a6e1-4c1b-ba66-5fee8038c056",
    Environment = "sandbox",
    Sandbox = true, //no need to provide base url if sandbox only set this to true, this will default to sandbox url
    BaseUrl = "<your_product_url>"
};

 services
    .AddMtnCollectionClient(cconfig);
    .AddMtnDisbursementsClient(dconfig)
    .AddMtnRemittanceClient(rconfig);

```

Inject registered client into class

```
public class PaymentController : Controller
{
    private readonly ICollectionClient collectionClient;

    public PaymentController(ICollectionClient collectionClient)
    {
        this.collectionClient = collectionClient;
    }
}

```

Supported Methods

Collection Methods
1. PostRequestToPay 
2. GetRequestToPay
3. AccountBalance
4. AccountHolder

Disbusrements Methods
1. PostTransfer 
2. GetTransfer 
3. AccountBalance
4. AccountHolder

Remittance Methods
1. PostTransfer 
2. GetTransfer 
3. AccountBalance
4. AccountHolder

Request Models
All methods use custom models with the same structure as specified in [MTN Sandbox](https://momodeveloper.mtn.com/docs/services/collection/operations/requesttopay-POST)


Please see [Test Project](https://github.com/peterasamoah7/Mtn.MoMo.DotNet.Client/tree/master/MtnMomo.DotNet.Client.Tests) for specific models


Response Models

The Data Model is a defined in [MTN Sandbox](https://momodeveloper.mtn.com/docs/services/collection/operations/requesttopay-POST)
```
"statusCode": 200, //Http Status code
"data": {   //data from MTN API
        
    },
    "status": "SUCCESSFUL", 
    "reason": null
},
"status": "Successful"

```

## Sample Usage
See sample project included [Sample Project](https://github.com/peterasamoah7/Mtn.MoMo.DotNet.Client/tree/master/MtnMomo.Sample)

See test project for all methods usage [Test Project](https://github.com/peterasamoah7/Mtn.MoMo.DotNet.Client/tree/master/MtnMomo.DotNet.Client.Tests)

## Contributing

Please feel free to contact me if you want to contribute.

## Authors

* **Peter Asamoah** 

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

