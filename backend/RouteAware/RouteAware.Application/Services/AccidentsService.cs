using RouteAware.Core.Models;
using RouteAware.DataAccess.Repositories;
using RouteAware.Moderation;
using System.Drawing;

namespace RouteAware.Application.Services
{
    public class AccidentsService : IAccidentsService
    {
        private readonly IAccidentsRepository accidentsRepository;
        private readonly IPhotosModerator photosModerator;

        public AccidentsService(IAccidentsRepository accidentsRepository, IPhotosModerator photosModerator)
        {
            this.accidentsRepository = accidentsRepository;
            this.photosModerator = photosModerator;
        }

        public async Task<List<Accident>> GetAllAccidents()
        {
            return await accidentsRepository.Get();
        }

        public async Task<List<Accident>> GetAccidentsByAuthor(Guid author)
        {
            return await accidentsRepository.GetByUserId(author);
        }

        public async Task<List<Accident>> GetAccidentsByRegion(string region)
        {
            return await accidentsRepository.GetByRegion(region);
        }

        public async Task<Guid> CreateAccident(Accident accident)
        {
            return await accidentsRepository.Create(accident);
        }

        public async Task<Guid> UpdateAccident(Guid id, string title, string description, string region, double latitude, double longitude, string imageURL, DateTime creationDateTime, string damageLevel)
        {
            return await accidentsRepository.Update(id, title, description, region, latitude, longitude, imageURL, creationDateTime, damageLevel);
        }

        public async Task<Guid> DeleteAccident(Guid id)
        {
            return await accidentsRepository.Delete(id);
        }

        public bool Moderate(MemoryStream memoryStream)
        {
            using var image = Image.FromStream(memoryStream);
            return photosModerator.Moderate(image);
        }
    }
}
