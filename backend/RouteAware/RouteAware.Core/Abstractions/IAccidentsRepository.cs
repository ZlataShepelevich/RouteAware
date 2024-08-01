using RouteAware.Core.Models;

namespace RouteAware.DataAccess.Repositories
{
    public interface IAccidentsRepository
    {
        Task<Guid> Create(Accident accident);
        Task<Guid> Delete(Guid id);
        Task<List<Accident>> Get();
        Task<List<Accident>> GetByRegion(string region);
        Task<List<Accident>> GetByUserId(Guid userId);
        Task<Guid> Update(Guid id, string title, string description, string region, double latitude, double longitude, string imageURL, DateTime creationDateTime, string damageLevel);
    }
}