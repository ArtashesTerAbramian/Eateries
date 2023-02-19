using System.Security.Claims;

namespace Eateries.Application.Interfaces;

public interface IJwtTokenService
{
    public string GenerateToken(ClaimsIdentity claimsIdentity);
}