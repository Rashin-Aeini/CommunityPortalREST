using System.Collections.Generic;

namespace CommunityPortalREST.Models.ViewModels.Home
{
    public class PostViewModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Thumbnail { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
    }
}
