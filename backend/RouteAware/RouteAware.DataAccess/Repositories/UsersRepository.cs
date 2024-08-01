using Microsoft.EntityFrameworkCore;
using RouteAware.Core.Models;
using RouteAware.DataAccess.Entities;

namespace RouteAware.DataAccess.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly RouteAwareDbContext context;

        public UsersRepository(RouteAwareDbContext context)
        {
            this.context = context;
        }

        public async Task Add(User user)
        {
            var userEntity = new RouteAwareUserEntity()
            {
                Id = user.Id,
                UserName = user.UserName,
                PasswordHash = user.PasswordHash,
                Email = user.Email
            };

            await context.Users.AddAsync(userEntity);
            await context.SaveChangesAsync();
        }

        public async Task<User> GetByEmail(string email)
        {
            var userEntity = await context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email) ?? throw new Exception();

            return User.Create(userEntity.Id, userEntity.UserName, userEntity.PasswordHash, userEntity.Email);
        }
    }
}
