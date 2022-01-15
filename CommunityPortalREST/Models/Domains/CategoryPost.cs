namespace CommunityPortalREST.Models.Domains
{
    public class CategoryPost
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
