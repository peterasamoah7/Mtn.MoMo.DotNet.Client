using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MtnMomo.DotNet.Client.Collection.Client;
using MtnMomo.DotNet.Client.Collection.Models.Config;
using MtnMomo.DotNet.Client.Common;
using MtnMomo.DotNet.Client.Common.Client;
using MtnMomo.DotNet.Client.Common.Client.Interfaces;
using MtnMomo.DotNet.Client.Common.Http;
using MtnMomo.DotNet.Client.Disbursements.Client;
using MtnMomo.DotNet.Client.Disbursements.Models.Config;
using MtnMomo.DotNet.Client.Remittance.Client;
using MtnMomo.DotNet.Client.Remittance.Models.Config;

namespace MobileMoney
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
            services.AddHttpClient(Constants.MtnClient, c => {
                c.BaseAddress = new Uri(Constants.Sandbox);
                c.DefaultRequestHeaders.Add(Constants.EnvHeader, "sandbox");
            });

            var config = new CollectionConfig
            {
                ApiKey = "c0b63594ff1d4676b25d08d77216a400",
                SubscriptionKey = "dcaf84d5179f455dbec7ba52601c62d2",
                UserId = "7706a623-7c1c-4d25-9cea-06cf06fbda51",
                Environment = "sandbox",
                Sandbox = true
            };

            var dconfig = new DisbursementsConfig
            {
                ApiKey = "200a55998bba461099093c9f2b34a26a",
                SubscriptionKey = "20b05f1a1e48469993c68fc182ae6453",
                UserId = "7706a623-7c1c-4d25-9cea-06cf06fbda51",
                Environment = "sandbox",
                Sandbox = true
            };

            var rconfig = new RemittanceConfig
            {
                ApiKey = "0b34dd8e1616441b9cc44c41dff9fdcf",
                SubscriptionKey = "cf4f7a46c6804d69b7d605a3e304ffe2",
                UserId = "7706a623-7c1c-4d25-9cea-06cf06fbda51",
                Environment = "sandbox",
                Sandbox = true
            };

            services.AddSingleton(config);
            services.AddSingleton(dconfig);
            services.AddSingleton(rconfig);
            services.AddScoped<IBaseClient, BaseClient>();
            services.AddScoped<ITokenClient, TokenClient>();
            services.AddScoped<IAccountBalanceClient, AccountBalanceClient>();
            services.AddScoped<IAccountHolderClient, AccountHolderClient>();
            services.AddScoped<ICollectionClient, CollectionClient>();
            services.AddScoped<IDisbursementsClient, DisbursementsClient>();
            services.AddScoped<IRemittanceClient, RemittanceClient>();
            services.AddScoped<ITransferClient, TransferClient>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
