using System;
using System.Collections.Generic;

namespace CommunityPortalREST.Models.Domains
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Thumbnail { get; set; }
        public string Content { get; set; }
        public TimeSpan Created { get; set; }
        public List<CategoryPost> Categories { get; set; }
    }
}
