using Microsoft.Extensions.DependencyInjection;
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
using System;

namespace MtnMomo.DotNet.Client
{
    public static class MtnMoMoClientExtensions
    {
        public static IServiceCollection AddMtnSandbox(this IServiceCollection services)
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

            return services;
        }

        /// <summary>
        /// Mtn Collection Client
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static IServiceCollection AdMtnCollectionClient(this IServiceCollection services, CollectionConfig config)
        {
            services.AddHttpClient(Constants.MtnClient, c => {
                c.BaseAddress = new Uri(Constants.Sandbox);
                c.DefaultRequestHeaders.Add(Constants.EnvHeader, "sandbox");
            });

            return services.AddScoped<IBaseClient, BaseClient>()
                    .AddScoped<ITokenClient, TokenClient>()
                    .AddSingleton(config)
                    .AddScoped<ICollectionClient, CollectionClient>()
                    .AddScoped<IAccountBalanceClient, AccountBalanceClient>()
                    .AddScoped<IAccountHolderClient, AccountHolderClient>();
        }

        /// <summary>
        /// Mtn Disbursement Client
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static IServiceCollection AddMtnDisbursementsClient(this IServiceCollection services, DisbursementsConfig config)
        {
            services.AddHttpClient(Constants.MtnClient, c => {
                c.BaseAddress = new Uri(Constants.Sandbox);
                c.DefaultRequestHeaders.Add(Constants.EnvHeader, "sandbox");
            });

            return services.AddScoped<IBaseClient, BaseClient>()
                    .AddScoped<ITokenClient, TokenClient>()
                    .AddSingleton(config)
                    .AddScoped<IDisbursementsClient, DisbursementsClient>()
                    .AddScoped<IAccountBalanceClient, AccountBalanceClient>()
                    .AddScoped<ITransferClient, TransferClient>()
                    .AddScoped<IAccountHolderClient, AccountHolderClient>();
        }

        /// <summary>
        /// Mtn Remittance Client
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static IServiceCollection AddMtnRemittanceClient(this IServiceCollection services, RemittanceConfig config)
        {
            services.AddHttpClient(Constants.MtnClient, c => {
                c.BaseAddress = new Uri(Constants.Sandbox);
                c.DefaultRequestHeaders.Add(Constants.EnvHeader, "sandbox");
            });

            return services.AddScoped<IBaseClient, BaseClient>()
                    .AddScoped<ITokenClient, TokenClient>()
                    .AddSingleton(config)
                    .AddScoped<IRemittanceClient, RemittanceClient>()
                    .AddScoped<IAccountBalanceClient, AccountBalanceClient>()
                    .AddScoped<ITransferClient, TransferClient>()
                    .AddScoped<IAccountHolderClient, AccountHolderClient>();
        }
    }
}
