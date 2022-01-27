using System.Collections.Generic;

namespace CommunityPortalREST.Models.Domains
{
    public class Account
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public List<Token> Tokens { get; set; }
        public List<AccountRole> Roles { get; set; }
        
    }
}
