using System.Collections.Generic;

namespace CommunityPortalREST.Models.ViewModels.Category
{
    public class ResponseCategoryViewModel
    {
        public int Pages { get; set; }
        public List<IndexCategoryViewModel> Result { get; set; }
    }
}
