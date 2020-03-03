# Mtn Mobile Money DotNet Client

A client library for using the Mtn Mobile Money API : https://momodeveloper.mtn.com/

## Getting Started

```
Install-Package MtnMobileMoney.DotNet.Client -Version 1.0.0
```

### Usage

Set up client library using depency injection

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

services.AddMtnCollectionClient(cconfig);

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

## Sample Usage
See sample project included https://github.com/peterasamoah7/Mtn.MoMo.DotNet.Client/tree/master/MtnMomo.Sample

## Contributing

Please feel free to contact me if you want to contribute.

## Authors

* **Peter Asamoah** 

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

