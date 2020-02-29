using MtnMomo.DotNet.Client.Common.Http;
using MtnMomo.DotNet.Client.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MtnMomo.DotNet.Client.Common.Client
{
    public class TokenClient : ITokenClient
    {
        private readonly IBaseClient baseClient; 

        public TokenClient(IBaseClient baseClient)
        {
            this.baseClient = baseClient;
        }

       
    }
}
