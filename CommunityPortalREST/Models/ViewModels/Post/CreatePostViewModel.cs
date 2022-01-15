using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CommunityPortalREST.Models.ViewModels.Post
{
    public class CreatePostViewModel
    {
        public CreatePostViewModel()
        {
            Categories = new List<int>();
        }

        [Required]
        public string Title { get; set; }

        public string Thumbnail { get; set; }

        [Required]
        public string Content { get; set; }

        public TimeSpan Created { get; set; }

        public List<int> Categories { get; set; }
    }
}
