using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RouteAware.Core.Models;
using RouteAware.DataAccess.Entities;

namespace RouteAware.DataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<RouteAwareUserEntity>
    {
        public void Configure(EntityTypeBuilder<RouteAwareUserEntity> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.UserName)                
                .IsRequired(); //.HasMaxLength(User.MAX_USERNAME_LENGTH)

            builder.Property(a => a.PasswordHash)
               .IsRequired();

            builder.Property(a => a.Email)
               .IsRequired();
        }
    }
}
