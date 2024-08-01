using RouteAware.Core.Models;

namespace RouteAware.Application.Services
{
    public interface IUsersService
    {
        Task<(string, User)> LoginUser(string email, string password);
        Task<Guid> RegisterUser(string userName, string password, string email);
    }
}