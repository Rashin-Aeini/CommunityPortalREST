namespace CommunityPortalREST.Models.ViewModels.Home
{
    public class RequestViewModel
    {
        public RequestViewModel()
        {
            Page = 1;
            Size = 15;
            Category = -1;
        }
        public int Page { get; set; }
        public int Size { get; set; }
        public int Category { get; set; }
    }
}
