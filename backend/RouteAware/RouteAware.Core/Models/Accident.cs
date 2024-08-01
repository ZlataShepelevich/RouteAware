using System;

namespace RouteAware.Core.Models
{
    public class Accident
    {
        public const int MAX_TITLE_LENGTH = 250;

        private Accident(Guid id, string title, string description, string region, Guid userId, double latitude, double longitude, string imageURL, DateTime creationDateTime, string damageLevel)
        {
            Id = id;
            Title = title;
            Description = description;
            Region = region;
            UserId = userId;
            Latitude = latitude;
            Longitude = longitude;
            ImageURL = imageURL;
            CreationDateTime = creationDateTime;
            DamageLevel = damageLevel;
        }

        public Guid Id {  get; }
        public string Title { get; } = string.Empty;
        public string Description { get; } = string.Empty;
        public string Region { get; } = string.Empty;
        public Guid UserId { get; }
        public double Latitude { get; }
        public double Longitude { get; }        
        public string ImageURL { get; } = string.Empty;  
        public DateTime CreationDateTime { get; }
        public string DamageLevel { get; } = string.Empty;

        public static (Accident Accident, string Error) Create(Guid id, string title, string description, string region, Guid userId, double latitude, double longitude, string imageURL, DateTime creationDateTime, string damageLevel)
        {
            var error = string.Empty;

            if (string.IsNullOrEmpty(title) || title.Length > MAX_TITLE_LENGTH)
            {
                error = "Title can not be empty or longer then 250 symbols";
            }

            var accident = new Accident(id, title, description, region, userId, latitude, longitude, imageURL, creationDateTime, damageLevel);

            return (accident, error);
        }
    }
}
