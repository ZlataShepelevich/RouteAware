namespace RouteAware.API.Contracts
{
    public record AccidentsRequest(
        string Title,
        string Description,
        string Region,
        Guid Author,
        double Latitude,
        double Longitude,
        string ImageURL, 
        DateTime CreationDateTime,
        string DamageLevel);
}
