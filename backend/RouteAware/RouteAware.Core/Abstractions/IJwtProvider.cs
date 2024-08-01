using RouteAware.Core.Models;

namespace RouteAware.Infrastructure
{
    public interface IJwtProvider
    {
        string GenerateToken(User user);
    }
}