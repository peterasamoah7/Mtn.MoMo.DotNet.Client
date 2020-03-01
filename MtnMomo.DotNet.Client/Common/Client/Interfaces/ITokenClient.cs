using MtnMomo.DotNet.Client.Common.Models;
using MtnMomo.DotNet.Client.Common.Models.Response;
using System.Threading.Tasks;

namespace MtnMomo.DotNet.Client.Common.Client.Interfaces
{
    public interface ITokenClient
    {
        Task<ClientResponse<TokenResponse>> GetToken(TokenRequest tokenRequest);
    }
}
