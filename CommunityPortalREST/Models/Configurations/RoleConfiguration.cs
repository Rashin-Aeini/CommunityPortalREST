using CommunityPortalREST.Models.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommunityPortalREST.Models.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasIndex(role => role.Id);

            builder.Property(role => role.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(role => role.Name)
                .IsUnique();

            builder.HasMany(role => role.Accounts)
                .WithOne(account => account.Role)
                .HasForeignKey(account => account.RoleId);
        }
    }
}
