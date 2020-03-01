using MtnMomo.DotNet.Client.Common.Models.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MtnMomo.DotNet.Client.Common.Http
{
    public interface IBaseClient
    {
        Task<ClientResponse<T>> GetAsync<T>(string requestUri, string clientName, IEnumerable<KeyValuePair<string, string>> headers = null) where T : class;
        Task<ClientResponse<T>> PostAsync<T>(string requestUri, string clientName, object value, IEnumerable<KeyValuePair<string, string>> headers = null) where T : class;
        Task<ClientResponse> PostAsync(string requestUri, string clientName, object value, IEnumerable<KeyValuePair<string, string>> headers = null);
        Task<ClientResponse> GetAsync(string requestUri, string clientName, IEnumerable<KeyValuePair<string, string>> headers = null);
    }
}
