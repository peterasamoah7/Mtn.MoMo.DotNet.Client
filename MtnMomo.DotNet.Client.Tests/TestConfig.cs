using Microsoft.Extensions.DependencyInjection;
using MtnMomo.DotNet.Client.Collection.Client;
using MtnMomo.DotNet.Client.Collection.Models.Config;
using MtnMomo.DotNet.Client.Common.Client;
using MtnMomo.DotNet.Client.Common.Client.Interfaces;
using MtnMomo.DotNet.Client.Common.Http;
using MtnMomo.DotNet.Client.Disbursements.Client;
using MtnMomo.DotNet.Client.Disbursements.Models.Config;
using MtnMomo.DotNet.Client.Remittance.Client;
using MtnMomo.DotNet.Client.Remittance.Models.Config;
using System;

namespace MtnMomo.DotNet.Client.Tests
{
    public static class TestConfig
    {
        /// <summary>
        /// Get collection client
        /// </summary>
        /// <returns></returns>
        public static ICollectionClient GetCollectionClient()
        {
            var services = new ServiceCollection();

            var config = new CollectionConfig
            {
                ApiKey = "c0b63594ff1d4676b25d08d77216a400",
                SubscriptionKey = "dcaf84d5179f455dbec7ba52601c62d2",
                UserId = "7706a623-7c1c-4d25-9cea-06cf06fbda51",
                Environment = "sandbox",
                Sandbox = true
            };

            services.AddHttpClient(Common.Constants.MtnClient, c => {
                c.BaseAddress = new Uri(Common.Constants.Sandbox);
                c.DefaultRequestHeaders.Add(Common.Constants.EnvHeader, "sandbox");
            });

            services.AddScoped<IBaseClient, BaseClient>()
                    .AddScoped<ITokenClient, TokenClient>()
                    .AddSingleton(config)
                    .AddScoped<ICollectionClient, CollectionClient>()
                    .AddScoped<IAccountBalanceClient, AccountBalanceClient>()
                    .AddScoped<IAccountHolderClient, AccountHolderClient>();

            var serviceProvider = services.BuildServiceProvider();

            var client = serviceProvider.GetService<ICollectionClient>(); 

            return client; 
        }

        /// <summary>
        /// Get Disbursements client
        /// </summary>
        /// <returns></returns>
        public static IDisbursementsClient GetDisbursementsClient()
        {
            var services = new ServiceCollection();

            var config = new DisbursementsConfig
            {
                ApiKey = "200a55998bba461099093c9f2b34a26a",
                SubscriptionKey = "20b05f1a1e48469993c68fc182ae6453",
                UserId = "7706a623-7c1c-4d25-9cea-06cf06fbda51",
                Environment = "sandbox",
                Sandbox = true
            };

            services.AddHttpClient(Common.Constants.MtnClient, c => {
                c.BaseAddress = new Uri(Common.Constants.Sandbox);
                c.DefaultRequestHeaders.Add(Common.Constants.EnvHeader, "sandbox");
            });

            services.AddScoped<IBaseClient, BaseClient>()
                    .AddScoped<ITokenClient, TokenClient>()
                    .AddSingleton(config)
                    .AddScoped<IDisbursementsClient, DisbursementsClient>()
                    .AddScoped<IAccountBalanceClient, AccountBalanceClient>()
                    .AddScoped<ITransferClient, TransferClient>()
                    .AddScoped<IAccountHolderClient, AccountHolderClient>();

            var serviceProvider = services.BuildServiceProvider();

            var client = serviceProvider.GetService<IDisbursementsClient>();

            return client;
        }

        /// <summary>
        /// Get Remittance client
        /// </summary>
        /// <returns></returns>
        public static IRemittanceClient GetRemittanceClient()
        {
            var services = new ServiceCollection();

            var config = new RemittanceConfig
            {
                ApiKey = "0b34dd8e1616441b9cc44c41dff9fdcf",
                SubscriptionKey = "cf4f7a46c6804d69b7d605a3e304ffe2",
                UserId = "7706a623-7c1c-4d25-9cea-06cf06fbda51",
                Environment = "sandbox",
                Sandbox = true
            };

            services.AddHttpClient(Common.Constants.MtnClient, c => {
                c.BaseAddress = new Uri(Common.Constants.Sandbox);
                c.DefaultRequestHeaders.Add(Common.Constants.EnvHeader, "sandbox");
            });

            services.AddScoped<IBaseClient, BaseClient>()
                    .AddScoped<ITokenClient, TokenClient>()
                    .AddSingleton(config)
                    .AddScoped<IRemittanceClient, RemittanceClient>()
                    .AddScoped<IAccountBalanceClient, AccountBalanceClient>()
                    .AddScoped<ITransferClient, TransferClient>()
                    .AddScoped<IAccountHolderClient, AccountHolderClient>();

            var serviceProvider = services.BuildServiceProvider();

            var client = serviceProvider.GetService<IRemittanceClient>();

            return client;
        }
    }
}
