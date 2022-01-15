using System.ComponentModel.DataAnnotations;

namespace CommunityPortalREST.Models.ViewModels.Category
{
    public class CreateCategoryViewModel
    {
        [Required]
        public string Title { get; set; }
    }
}
