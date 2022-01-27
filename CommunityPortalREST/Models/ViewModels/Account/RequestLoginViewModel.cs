using System.ComponentModel.DataAnnotations;

namespace CommunityPortalREST.Models.ViewModels.Account
{
    public class RequestLoginViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
