using System.Security.Claims;

namespace EdTech.Interfaces
{
    public interface ITokenService
    {
        string GenerateJWTToken(ClaimsIdentity claimsIdentity);
    }
}