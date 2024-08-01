namespace RouteAware.API.Contracts
{
    public record AccidentsResponse(
        Guid Id, 
        string Title,
        string Description,
        string Region,
        double Latitude,
        double Longitude,
        string ImageURL,
        DateTime CreationDateTime,
        string DamageLevel);
}
