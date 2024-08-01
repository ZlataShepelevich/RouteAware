namespace RouteAware.API.Contracts
{
    public record RegisterUserRequest(
        string UserName,
        string Password,
        string Email);
}
