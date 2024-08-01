using RouteAware.Core.Models;
using RouteAware.DataAccess.Repositories;
using RouteAware.Infrastructure;

namespace RouteAware.Application.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository usersRepository;
        private readonly IPasswordHasher passwordHasher;
        private readonly IJwtProvider jwtProvider;

        public UsersService(
            IUsersRepository usersRepository,
            IPasswordHasher passwordHasher,
            IJwtProvider jwtProvider)
        {
            this.usersRepository = usersRepository;
            this.passwordHasher = passwordHasher;
            this.jwtProvider = jwtProvider;
        }

        public async Task<Guid> RegisterUser(string userName, string password, string email)
        {
            var hashedPassword = passwordHasher.Generate(password);

            var userId = Guid.NewGuid();

            var user = User.Create(userId, userName, hashedPassword, email);

            await usersRepository.Add(user);

            return userId;
        }

        public async Task<(string, User)> LoginUser(string email, string password)
        {
            var user = await usersRepository.GetByEmail(email);

            var result = passwordHasher.Verify(password, user.PasswordHash);

            if (!result)
            {
                throw new Exception("Failed to login");
            }

            var token = jwtProvider.GenerateToken(user);

            return (token, user);
        }
    }
}
