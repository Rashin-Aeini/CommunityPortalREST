using CommunityPortalREST.Models.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommunityPortalREST.Models.Configurations
{
    public class TokenConfiguration : IEntityTypeConfiguration<Token>
    {
        public void Configure(EntityTypeBuilder<Token> builder)
        {
            builder.HasKey(token => token.Id);

            builder.Property(token => token.Number)
                .IsRequired();

            builder.Property(token => token.Expire)
                .IsRequired();

            builder.HasIndex(token => token.Number)
                .IsUnique();
        }
    }
}
