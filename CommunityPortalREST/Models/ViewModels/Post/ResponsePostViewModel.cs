using System.Collections.Generic;

namespace CommunityPortalREST.Models.ViewModels.Post
{
    public class ResponsePostViewModel
    {
        public int Pages { get; set; }
        public List<IndexPostViewModel> Result { get; set; }
    }
}
