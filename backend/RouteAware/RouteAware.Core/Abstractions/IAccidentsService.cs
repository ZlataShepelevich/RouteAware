using RouteAware.Core.Models;

namespace RouteAware.Application.Services
{
    public interface IAccidentsService
    {
        Task<Guid> CreateAccident(Accident accident);
        Task<Guid> DeleteAccident(Guid id);
        Task<List<Accident>> GetAllAccidents();
        Task<List<Accident>> GetAccidentsByAuthor(Guid author);
        Task<List<Accident>> GetAccidentsByRegion(string region);
        Task<Guid> UpdateAccident(Guid id, string title, string description, string region, double latitude, double longitude, string imageURL, DateTime creationDateTime, string damageLevel);
        bool Moderate(MemoryStream memoryStream);
    }
}