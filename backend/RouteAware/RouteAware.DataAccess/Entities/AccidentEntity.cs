using System.ComponentModel.DataAnnotations.Schema;

namespace RouteAware.DataAccess.Entities
{
    public class AccidentEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;

        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public RouteAwareUserEntity User { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string ImageURL { get; set; } = string.Empty;
        public DateTime CreationDateTime { get; set; }
        public string DamageLevel { get; set; } = string.Empty;
    }
}
