namespace RouteAware.API.Contracts
{    
    public record LoginUserRequest(
        string Password,
        string Email);
}
