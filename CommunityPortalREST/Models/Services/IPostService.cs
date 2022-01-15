using CommunityPortalREST.Models.Domains;
using CommunityPortalREST.Models.ViewModels.Post;

namespace CommunityPortalREST.Models.Services
{
    public interface IPostService : IService<Post, CreatePostViewModel>
    {
        bool TrackCategory(int post, int category);
        bool SubTrackCategory(int post, int category);
    }
}
