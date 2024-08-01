using RouteAware.Core.Models;

namespace RouteAware.DataAccess.Repositories
{
    public interface IUsersRepository
    {
        Task Add(User user);
        Task<User> GetByEmail(string email);
    }
}