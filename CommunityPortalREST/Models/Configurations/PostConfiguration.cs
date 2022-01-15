using System.ComponentModel.DataAnnotations;
using CommunityPortalREST.Models.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommunityPortalREST.Models.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            //[Key]
            builder.HasKey(post => post.Id);

            //[Required]
            builder.Property(post => post.Title)
                .IsRequired();

            builder.Property(post => post.Content)
                .IsRequired();

            //[DataType(DataType.Text)]
            builder.Property(post => post.Content)
                .HasColumnType(nameof(DataType.Text).ToUpper());

            //[MaxLength(250)]
            builder.Property(post => post.Title)
                .HasMaxLength(250);

            builder.HasMany(post => post.Categories)
                .WithOne(category => category.Post)
                .HasForeignKey(category => category.PostId);
        }
    }
}
