using CommunityPortalREST.Models.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommunityPortalREST.Models.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(account => account.Id);

            builder.Property(account => account.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(account => account.Family)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(account => account.Username)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(account => account.Password)
                .IsRequired()
                .HasMaxLength(250);

            builder.HasIndex(account => account.Username)
                .IsUnique();

            builder.HasMany(account => account.Tokens)
                .WithOne(token => token.Account)
                .HasForeignKey(token => token.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(account => account.Roles)
                .WithOne(role => role.Account)
                .HasForeignKey(role => role.AccountId);
        }
    }
}
