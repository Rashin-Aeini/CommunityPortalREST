using CommunityPortalREST.Models.Configurations;
using CommunityPortalREST.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace CommunityPortalREST.Models.Data
{
    public class PortalContext : DbContext
    {
        public PortalContext(DbContextOptions<PortalContext> options) : base(options)
        {
            
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryPost> CategoryPosts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new PostConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            base.OnModelCreating(builder);
        }
    }
}
