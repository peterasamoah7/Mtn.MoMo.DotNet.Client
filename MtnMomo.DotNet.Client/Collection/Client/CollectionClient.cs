using MtnMomo.DotNet.Client.Collection.Models.Config;
using MtnMomo.DotNet.Client.Common.Http;
using MtnMomo.DotNet.Client.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MtnMomo.DotNet.Client.Collection.Client
{
    public class CollectionClient : ICollectionClient
    {
        private readonly IBaseClient baseClient;
        private readonly CollectionConfig collectionConfig; 

        public CollectionClient(
            IBaseClient baseClient,
            CollectionConfig collectionConfig)
        {
            this.baseClient = baseClient;
        }

        private async Task<TokenResponse> GetToken()
        {
            return null; 
        }
    }
}
