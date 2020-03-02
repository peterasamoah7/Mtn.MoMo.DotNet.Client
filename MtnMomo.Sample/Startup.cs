using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MtnMomo.DotNet.Client;
using MtnMomo.DotNet.Client.Collection.Models.Config;
using MtnMomo.DotNet.Client.Disbursements.Models.Config;
using MtnMomo.DotNet.Client.Remittance.Models.Config;

namespace MtnMomo.Sample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //create config for client type
            //collection client config
            //use credentials from Mtn Sandbox Profile : https://momodeveloper.mtn.com/developer
            var cconfig = new CollectionConfig
            {
                ApiKey = "ad8a5dbe6a754a108d0b122bfe8c26bb",
                SubscriptionKey = "4ae1cbdd3f744fc283b9a3339a444003",
                UserId = "1bfc600a-a6e1-4c1b-ba66-5fee8038c056",
                Environment = "sandbox",
                Sandbox = false, //no need to provide base url if sandbox only set this to true, this will default to sandbox url
                BaseUrl = "<your_production_url>"
            };
            //disbursement client config
            //var dconfig = new DisbursementsConfig
            //{
            //    ApiKey = "0ef2baa628e04a419ee7d4bf17839c20",
            //    SubscriptionKey = "f1d94c7aabd64fc0af6bfd0a2353c020",
            //    UserId = "1bfc600a-a6e1-4c1b-ba66-5fee8038c056",
            //    Environment = "sandbox",
            //    Sandbox = false, //no need to provide base url if sandbox only set this to true, this will default to sandbox url
            //    BaseUrl = "<your_production_url>"
            //};
            //remittace client config
            //var rconfig = new RemittanceConfig
            //{
            //    ApiKey = "a4b41504bff44def992f439fa97a6e41",
            //    SubscriptionKey = "64fe810144b240298a83bc73ddf85d67",
            //    UserId = "1bfc600a-a6e1-4c1b-ba66-5fee8038c056",
            //    Environment = "sandbox",
            //    Sandbox = false, //no need to provide base url if sandbox only set this to true, this will default to sandbox url
            //    BaseUrl = "<your_product_url>"
            //};


            //register mtn momo client
            services
                .AddMtnCollectionClient(cconfig);
                //.AddMtnDisbursementsClient(dconfig)
                //.AddMtnRemittanceClient(rconfig);

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
