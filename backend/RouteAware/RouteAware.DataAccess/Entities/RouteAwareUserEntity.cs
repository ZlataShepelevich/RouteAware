namespace RouteAware.DataAccess.Entities
{
    public class RouteAwareUserEntity
    {
        public Guid Id { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
    }
}
