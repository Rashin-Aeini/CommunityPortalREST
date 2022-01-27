using CommunityPortalREST.Models.Domains;

namespace CommunityPortalREST.Models.Repositories
{
    public interface ITokenRepository : IRepository<Token>
    {
        bool Valid(string entry);
    }
}
