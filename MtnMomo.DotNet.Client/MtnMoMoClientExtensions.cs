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
        /// <summary>
        /// Mtn Collection Client
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static IServiceCollection AddMtnCollectionClient(this IServiceCollection services, CollectionConfig config)
        {
            services.AddHttpClient(Constants.MtnClient, c =>
            {
                c.BaseAddress = config.Sandbox ? new Uri(Constants.Sandbox) : new Uri(config.BaseUrl); 
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
            services.AddHttpClient(Constants.MtnClient, c =>
            {
                c.BaseAddress = config.Sandbox ? new Uri(Constants.Sandbox) : new Uri(config.BaseUrl);
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
            services.AddHttpClient(Constants.MtnClient, c =>
            {
                c.BaseAddress = config.Sandbox ? new Uri(Constants.Sandbox) : new Uri(config.BaseUrl);
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
