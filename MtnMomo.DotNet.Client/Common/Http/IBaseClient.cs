using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MtnMomo.DotNet.Client.Common.Http
{
    public interface IBaseClient
    {
        Task<T> GetAsync<T>(string requestUri, string clientName) where T : class;
        Task<T> PostAsync<T>(string requestUri, string clientName, object value) where T : class;
        Task<HttpResponseMessage> PostAsync(string requestUri, string clientName, object value);
        Task<HttpResponseMessage> GetAsync(string requestUri, string clientName);
    }
}
