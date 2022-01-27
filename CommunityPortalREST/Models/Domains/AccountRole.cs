namespace CommunityPortalREST.Models.Domains
{
    public class AccountRole
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
