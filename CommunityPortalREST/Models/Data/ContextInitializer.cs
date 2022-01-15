using Microsoft.EntityFrameworkCore;

namespace CommunityPortalREST.Models.Data
{
    public static class ContextInitializer
    {
        public static void Initialize(DbContext context)
        {
            context.Database.Migrate();
        }
    }
}
