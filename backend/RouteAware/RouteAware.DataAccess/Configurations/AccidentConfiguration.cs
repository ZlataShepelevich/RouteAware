using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RouteAware.Core.Models;
using RouteAware.DataAccess.Entities;

namespace RouteAware.DataAccess.Configurations
{
    public class AccidentConfiguration : IEntityTypeConfiguration<AccidentEntity>
    {
        public void Configure(EntityTypeBuilder<AccidentEntity> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Title)
                .HasMaxLength(Accident.MAX_TITLE_LENGTH)
                .IsRequired();

            builder.Property(a => a.Description)
               .IsRequired();

            builder.Property(a => a.Region)
               .IsRequired();

            builder.HasOne(a => a.User)
                  .WithMany() 
                  .HasForeignKey(a => a.UserId) 
                  .IsRequired();

            builder.Property(a => a.Latitude)
               .IsRequired();

            builder.Property(a => a.Longitude)
               .IsRequired();

            builder.Property(a => a.ImageURL)
               .IsRequired();

            builder.Property(a => a.CreationDateTime)
               .IsRequired();

            builder.Property(a => a.DamageLevel)
               .IsRequired();
        }
    }
}
