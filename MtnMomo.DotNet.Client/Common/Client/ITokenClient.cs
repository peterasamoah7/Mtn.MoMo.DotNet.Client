using MtnMomo.DotNet.Client.Common.Models;
using System.Threading.Tasks;

namespace MtnMomo.DotNet.Client.Common.Client
{
    public interface ITokenClient
    {
        Task<ClientResponse<TokenResponse>> GetToken(TokenRequest tokenRequest);
    }
}
