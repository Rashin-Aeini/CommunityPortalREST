namespace CommunityPortalREST.Models.ViewModels.Category
{
    public class RequestFilterCategoryViewModel
    {
        public RequestFilterCategoryViewModel()
        {
            Search = ""; //string.Empty;
            Size = 20;
        }
        
        public string Search { get; set; }
        public int Size { get; set; }
        public int Page { get; set; }
        

        
    }
}
