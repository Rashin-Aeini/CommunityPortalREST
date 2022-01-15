namespace CommunityPortalREST.Models.ViewModels.Post
{
    public class RequestFilterPostViewModel
    {
        public string Search { get; set; }
        public string Sort { get; set; }
        public string Type { get; set; }
        public int Page { get; set; }
    }
}
