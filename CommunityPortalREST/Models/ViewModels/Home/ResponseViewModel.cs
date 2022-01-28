using System.Collections.Generic;

namespace CommunityPortalREST.Models.ViewModels.Home
{
    public class ResponseViewModel
    {
        public int Pages { get; set; }
        public List<PostViewModel> Posts { get; set; }
    }
}
