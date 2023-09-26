using PortFolio.Models.DTO;
using System.Security.Claims;

namespace PortFolio.Repositories.Abstract
{
    public interface ITokenService
    {

        TokenResponse GetToken(IEnumerable<Claim> claims);

        string GetRefreshToken();

        ClaimsPrincipal GetClaimsPrincipal(string Token);
      
    }
}
