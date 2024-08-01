using Microsoft.EntityFrameworkCore;
using RouteAware.Core.Models;
using RouteAware.DataAccess.Entities;

namespace RouteAware.DataAccess.Repositories
{
    public class AccidentsRepository : IAccidentsRepository
    {
        private readonly RouteAwareDbContext context;

        public AccidentsRepository(RouteAwareDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Accident>> Get()
        {
            var accidentEntities = await context.Accidents
                .AsNoTracking()
                .ToListAsync();

            var accidents = accidentEntities
                .Select(a => Accident.Create(a.Id, a.Title, a.Description, a.Region, a.UserId, a.Latitude, a.Longitude, a.ImageURL, a.CreationDateTime, a.DamageLevel).Accident)
                .ToList();

            return accidents;
        }

        public async Task<List<Accident>> GetByUserId(Guid userId)
        {
            var accidents = await context.Accidents
                .AsNoTracking()
                .Where(a => a.UserId == userId)
                .Select(a => Accident.Create(a.Id, a.Title, a.Description, a.Region, a.UserId, a.Latitude, a.Longitude, a.ImageURL, a.CreationDateTime, a.DamageLevel).Accident)
                .ToListAsync();

            return accidents;
        }

        public async Task<List<Accident>> GetByRegion(string region)
        {
            var accidents = await context.Accidents
                .AsNoTracking()
                .Where(a => a.Region == region)
                .Select(a => Accident.Create(a.Id, a.Title, a.Description, a.Region, a.UserId, a.Latitude, a.Longitude, a.ImageURL, a.CreationDateTime, a.DamageLevel).Accident)
                .ToListAsync();

            return accidents;
        }

        public async Task<Guid> Create(Accident accident)
        {
            var accidentEntity = new AccidentEntity
            {
                Id = accident.Id,
                Title = accident.Title,
                Description = accident.Description,
                Region = accident.Region,
                UserId = accident.UserId,
                Latitude = accident.Latitude,
                Longitude = accident.Longitude,
                ImageURL = accident.ImageURL,
                CreationDateTime = accident.CreationDateTime,
                DamageLevel = accident.DamageLevel
            };

            await context.Accidents.AddAsync(accidentEntity);
            await context.SaveChangesAsync();

            return accidentEntity.Id;
        }

        public async Task<Guid> Update(Guid id, string title, string description, string region, double latitude, double longitude, string imageURL, DateTime creationDateTime, string damageLevel)
        {
            await context.Accidents
                .Where(a => a.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(a => a.Title, a => title)
                    .SetProperty(a => a.Description, a => description)
                    .SetProperty(a => a.Region, a => region)
                    .SetProperty(a => a.Latitude, a => latitude)
                    .SetProperty(a => a.Longitude, a => longitude)
                    .SetProperty(a => a.ImageURL, a => imageURL)
                    .SetProperty(a => a.CreationDateTime, a => creationDateTime)
                    .SetProperty(a => a.DamageLevel, a => damageLevel));

            return id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await context.Accidents
                .Where(a => a.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }
    }
}
