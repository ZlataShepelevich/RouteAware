namespace RouteAware.API.Contracts
{
    public record UsersResponse(
        Guid Id,
        string UserName,
        string Email);
}
